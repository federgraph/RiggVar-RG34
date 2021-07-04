using System;
using System.Globalization;

namespace RiggVar.Rgg
{

    public class TGetriebeFS : TGetriebe
    {
        public bool WantToPlayWithExtendedSearchRange;

        public TGetriebeFS() : base()
        {
            // BerechneWinkel();
            // wird jetzt in constructor von TMast aufgerufen
        }
        protected virtual void BerechneF()
        {
            // Berechnung Punkt F - Masttop
            FrEpsilon = Math.PI / 2 - SKK.AngleXZ(rP.C, rP.D);
            rP.F = SKK.AnglePointXZ(rP.D, FiMastL - FiMastUnten, FrEpsilon);
        }
        protected void BerechneM()
        {
            double a = rP.F0.Distance(rP.F);
            double t = (a - MastfallVorlauf) / a;
            TRealPoint ooTemp = rP.F - rP.F0;
            ooTemp = rP.F0 + ooTemp * t;
            rP.M = ooTemp;
        }
        protected virtual void KorrekturF(
            double tempH, double k1, double k2,
            ref double k3, ref double Beta, ref double Gamma)
        {
            // wird später überschrieben,
            // wenn die Mastbiegung verwendet werden kann,
            // um k3 und tempBeta neu zu bestimmen.
        }
        public void ResetStatus()
        {
            FGetriebeOK = true;
            FGetriebeStatus.Exclude(Rigg.gsWanteZukurz);
            FGetriebeStatus.Exclude(Rigg.gsWanteZulang);
        }
        public void UpdateGetriebe()
        {
            _ = LogList.Remove(0, LogList.Length);
            switch (SalingTyp)
            {
                case TSalingTyp.stOhneStarr:
                    UpdateGetriebeOS();
                    break;

                case TSalingTyp.stOhneBiegt:
                    UpdateGetriebeOS_2();
                    break;

                case TSalingTyp.stDrehbar:
                    UpdateGetriebeDS();
                    break;

                case TSalingTyp.stFest:
                    if (ManipulatorMode)
                    {
                        UpdateGetriebeFS();
                    }
                    else
                    {
                        BerechneWinkel();
                    }

                    break;
            }
        }
        public void UpdateGetriebeFS()
        {
            // FrWinkel gegeben, FrVorstag ergibt sich

            bool svar = false;

            ResetStatus();
            FrPhi = FrWinkel + FrAlpha;
            Wanten3dTo2d();

            // Berechnung der Punkte A, B, P und D
            FrPsi = RggCalc.PsiVonPhi(FrPhi, FrBasis, FrWunten2D, FrSalingH, FrMastUnten, ref svar);
            if (svar == false)
            {
                FGetriebeOK = false;
                FGetriebeStatus.Include(Rigg.gsErrorPsivonPhi);
                _ = LogList.AppendLine("TGetriebeFS.UpdateGetriebeFS:");
                _ = LogList.AppendLine("  svar False in PsivonPhi");
                return;
            };

            rP.A = SKK.AnglePointXZ(rP.A0, FrWunten2D, FrPhi - FrAlpha);
            rP.A.Y = -FrSalingA / 2;

            rP.B = rP.A;
            rP.B.Y = -rP.A.Y;

            rP.P = rP.A;
            rP.P.Y = 0;

            rP.D = SKK.AnglePointXZ(rP.D0, FrMastUnten, FrPsi - FrAlpha);

            rP.C = SKK.IntersectionXZ1(rP.A, rP.D, FrWoben2D, FrMastOben);

            FrVorstag = rP.C0.Distance(rP.C);
            FrSalingL = Math.Sqrt(RggCalc.Sqr(FrSalingH) + RggCalc.Sqr(FrSalingA / 2));
            Rest();
        }

        private class WobenIstVonPsiData
        {
            public TRealPoint TempA, TempC, TempD;
            public double WStrich, Basis, Skalar;
        }

        private double WobenIstVonPsi(double psi, WobenIstVonPsiData nd)
        {

            // Berechnungen im Vierelenk D0 D C C0
            rP.D = SKK.AnglePointXZ(rP.D0, FrMastUnten, psi - FrAlpha);
            rP.C = SKK.IntersectionXZ1(rP.D, rP.C0, FrMastOben, FrVorstag);

            nd.WStrich = rP.A0.Distance(rP.C);
            nd.Basis = rP.A0.Distance(rP.D);

            // weiter mit Koordinatentransformation, ebenes Trapez A0, A, C, D
            nd.TempD = Vcalc.Null;
            nd.TempD.X = nd.Basis;
            nd.TempC = SKK.IntersectionXZ1(TRealPoint.Zero, nd.TempD, nd.WStrich, FrMastOben);
            nd.TempA = SKK.IntersectionXZ1(TRealPoint.Zero, nd.TempD, FrWunten3D, FrSalingL);

            return nd.TempA.Distance(nd.TempC);
        }

        public void UpdateGetriebeDS()
        {
            int Counter;

            // gegeben: Woben3d, Wunten3d, Mastunten, Mastoben, SalingL,
            // Vorstag, Rumpfkoordinaten.
            // gesucht: Riggkoordinaten ooA, ooB, ooC, ooD, ooP, ooF
            double psiStart, psiEnde, psiEnde2, psiA, psiB;
            double WobenMin, WobenMax, WobenIst, Diff;
            double Saling1L, W1Strich;
            //double WStrich, Basis, Skalar;
            TRealPoint Temp;
            //TRealPoint TempA, TempC, TempD;
            //TRealPoint [] oooTemp = new TRealPoint [Rigg.TRiggPoints];
            WobenIstVonPsiData nd = new WobenIstVonPsiData();

            ResetStatus();

            // Vorstag gegeben, Winkel numerisch ermitteln!
            // Startwinkel
            SKK.SchnittEbene = TSchnittEbene.seXZ;
            SKK.Radius1 = FrMastUnten;
            SKK.Radius2 = FrVorstag - FrMastOben;
            SKK.MittelPunkt1 = rP.D0;
            SKK.MittelPunkt2 = rP.C0;
            Temp = SKK.SchnittPunkt1;

            psiStart = Math.Atan((rP.D0.X - Temp.X) / (Temp.Z - rP.D0.Z));
            psiStart = psiStart + (Math.PI / 2) + FrAlpha + (0.1 * Math.PI / 180);

            // Endwinkel
            SKK.SchnittEbene = TSchnittEbene.seXZ;
            SKK.Radius1 = FrMastUnten + FrMastOben;
            SKK.Radius2 = FrVorstag;
            SKK.MittelPunkt1 = rP.D0;
            SKK.MittelPunkt2 = rP.C0;
            Temp = SKK.SchnittPunkt1;

            psiEnde = Math.Atan((rP.D0.X - Temp.X) / (Temp.Z - rP.D0.Z));
            psiEnde = psiEnde + (Math.PI / 2) + FrAlpha;
            psiEnde2 = psiEnde + (50 * Math.PI / 180);

            WobenMin = WobenIstVonPsi(psiStart, nd);
            WobenMax = WobenIstVonPsi(psiEnde, nd);

            psiA = 0;
            psiB = 0;
            if (FrWoben3D < WobenMin)
            {
                psiA = psiEnde2;
                psiB = psiEnde;
                WobenMin = WobenIstVonPsi(psiEnde2, nd);
                if (FrWoben3D < WobenMin)
                {
                    FGetriebeOK = false;
                    FGetriebeStatus.Include(Rigg.gsWanteZukurz);
                }
            }
            else if (FrWoben3D > WobenMax)
            {
                FrWanteZulang = FrWoben3D - WobenMax;
                FGetriebeOK = false;
                FGetriebeStatus.Include(Rigg.gsWanteZulang);
            }
            else
            {
                psiA = psiStart;
                psiB = psiEnde;
            }

            // FrPsi ermitteln
            // Mast gerade zeichnen, wenn Wante zu lang oder zu kurz ist
            if (FGetriebeStatus.IsMember(Rigg.gsWanteZulang))
            {
                FrPsi = psiEnde;
            }
            else if (FGetriebeStatus.IsMember(Rigg.gsWanteZukurz))
            {
                _ = WobenIstVonPsi(psiEnde, nd);
                FrPsi = psiEnde;
            }
            //if GetriebeOK then : den richtigen Winkel FrPsi numerisch ermitteln
            else
            {
                Counter = 0;
                do
                {
                    Counter++;
                    FrPsi = (psiA + psiB) / 2;
                    WobenIst = WobenIstVonPsi(FrPsi, nd);
                    Diff = WobenIst - FrWoben3D;
                    if (Diff > 0)
                    {
                        psiB = FrPsi;
                    }
                    else
                    {
                        psiA = FrPsi;
                    }
                }
                while
                    ((Math.Abs(Diff) >= 0.01) && (Counter < 200));
            }

            // weiter im ebenen Trapez
            RggCalc.SchnittGG(Vcalc.Null, nd.TempC, nd.TempD, nd.TempA, ref Temp);
            //Temp enthält jetzt den Schnittpunkt der Diagonalen
            W1Strich = Temp.Length();
            Saling1L = nd.TempD.Distance(Temp);

            // weiter räumlich
            nd.Skalar = W1Strich / nd.WStrich;
            Temp = rP.C - rP.A0;
            Temp *= nd.Skalar;
            Temp = rP.A0 + Temp;
            // Temp enthält jetzt den räumlichen Schnittpunkt der Diagonalen

            // Berechnung Punkt ooA
            nd.Skalar = FrSalingL / Saling1L;
            Temp -= rP.D;
            Temp *= nd.Skalar;
            rP.A = rP.D + Temp;

            // aktualisieren
            rP.P = rP.A;
            rP.P.Y = 0;
            rP.B = rP.A;
            rP.B.Y = -rP.A.Y;
            FrSalingA = 2 * rP.A.Y;
            FrSalingH = rP.P.Distance(rP.D);
            FrPhi = Math.Atan((rP.P0.X - rP.P.X) / (rP.P.Z - rP.P0.Z));
            FrPhi = FrPhi + (Math.PI / 2) + FrAlpha;
            FrWinkel = FrPhi - FrAlpha;
            Rest();
        }
        public void UpdateGetriebeOS()
        {
            // FrVorstag und FrWoben2d gegeben
            TRealPoint Temp;
            double Skalar;

            ResetStatus();
            // Berechnung Punkt C
            SKK.SchnittEbene = TSchnittEbene.seXZ;
            SKK.Radius1 = FrMastUnten + FrMastOben;
            SKK.Radius2 = FrVorstag;
            SKK.MittelPunkt1 = rP.D0;
            SKK.MittelPunkt2 = rP.C0;
            rP.C = SKK.SchnittPunkt1;

            FrWunten2D = rP.P0.Distance(rP.C) - FrWoben2D;
            // Punkt P
            Skalar = FrWoben2D / (FrWunten2D + FrWoben2D);
            rP.P.X = rP.C.X - (Skalar * (rP.C.X - rP.P0.X));
            rP.P.Y = 0;
            rP.P.Z = rP.C.Z - (Skalar * (rP.C.Z - rP.P0.Z));
            // Punkte A, B
            rP.A = rP.P;
            rP.A.Y = Skalar * rP.A0.Y;
            rP.B = rP.A;
            rP.B.Y = -rP.A.Y;
            // Punkt D
            Temp = rP.C - rP.D0;
            Skalar = FrMastUnten / (FrMastUnten + FrMastOben);
            Temp.X = Skalar * Temp.X;
            //Temp.y = 0;
            Temp.Z = Skalar * Temp.Z;
            rP.D = rP.D0 + Temp;
            // aktualisieren
            FrSalingH = rP.P.Distance(rP.D);
            FrSalingA = 2 * rP.A.Y;
            FrSalingL = rP.A.Distance(rP.D);
            FrPhi = Math.Atan((rP.P0.X - rP.P.X) / (rP.P.Z - rP.P0.Z));
            FrPhi = FrPhi + (Math.PI / 2) + FrAlpha;
            FrWinkel = FrPhi - FrAlpha;
            FrPsi = Math.Atan((rP.D0.X - rP.D.X) / (rP.D.Z - rP.D0.Z));
            FrPsi = FrPsi + (Math.PI / 2) + FrAlpha;
            Wanten2dTo3d();
            Rest();
        }
        public void UpdateGetriebeOS_2()
        {
            // FrVorstag und FrWoben3d und FrWunten3d gegeben
            double TempW, Skalar, TempWunten2d, TempWoben2d;
            TRealPoint Temp;

            ResetStatus();
            // Wanten3dto2d
            TempW = Math.Sqrt(RggCalc.Sqr(FrWunten3D + FrWoben3D) - RggCalc.Sqr(FrPuettingA / 2));
            Skalar = FrWunten3D / (FrWoben3D + FrWunten3D);
            TempWunten2d = TempW * Skalar;
            TempWoben2d = TempW * (1 - Skalar);
            // Berechnung Punkt C
            SKK.SchnittEbene = TSchnittEbene.seXZ;
            SKK.Radius1 = TempWunten2d + TempWoben2d;
            SKK.Radius2 = FrVorstag;
            SKK.MittelPunkt1 = rP.P0;
            SKK.MittelPunkt2 = rP.C0;
            rP.C = SKK.SchnittPunkt1;

            // wenn die Wanten nicht straff sind:
            if (rP.D0.Distance(rP.C) > FrMastUnten + FrMastOben)
            {
                // Punkt C
                SKK.SchnittEbene = TSchnittEbene.seXZ;
                SKK.Radius1 = FrMastUnten + FrMastOben;
                SKK.Radius2 = FrVorstag;
                SKK.MittelPunkt1 = rP.D0;
                SKK.MittelPunkt2 = rP.C0;
                rP.C = SKK.SchnittPunkt1;
                // Punkt D
                Temp = rP.C - rP.D0;
                Skalar = FrMastUnten / (FrMastUnten + FrMastOben);
                Temp.X = Skalar * Temp.X;
                //Temp.y = 0;
                Temp.Z = Skalar * Temp.Z;
                rP.D = rP.D0 + Temp;
                // Wantenlängen
                /*
                FrWoben2d := TempWoben2d;
                FrWunten2d := rP.P0.Distance(rP.C) - FrWoben2d;
                */
                FrWanteZulang = FrWunten3D + FrWoben3D - rP.C.Distance(rP.A0);
                FGetriebeOK = false;
                FGetriebeStatus.Include(Rigg.gsWanteZulang);
            }

            // wenn die Wanten straff sind:
            else
            {
                // Punkt C oben schon berechnet
                // Punkt D
                SKK.SchnittEbene = TSchnittEbene.seXZ;
                SKK.Radius1 = FrMastOben;
                SKK.Radius2 = FrMastUnten;
                SKK.MittelPunkt1 = rP.C;
                SKK.MittelPunkt2 = rP.D0;
                rP.D = SKK.SchnittPunkt1;
                //Wantenlängen
                /*
                FrWunten2d := TempWunten2d;
                FrWoben2d := TempWoben2d;
                */
            }

            // Punkt P
            Skalar = FrWoben2D / (FrWunten2D + FrWoben2D);
            rP.P.X = rP.C.X - (Skalar * (rP.C.X - rP.P0.X));
            rP.P.Y = 0;
            rP.P.Z = rP.C.Z - (Skalar * (rP.C.Z - rP.P0.Z));
            // Punkte A, B
            rP.A = rP.P;
            rP.A.Y = Skalar * rP.A0.Y;
            rP.B = rP.A;
            rP.B.Y = -rP.A.Y;
            // aktualisieren
            FrSalingH = rP.P.Distance(rP.D);
            FrSalingA = 2 * rP.A.Y;
            FrSalingL = rP.A.Distance(rP.D);
            FrPhi = Math.Atan((rP.P0.X - rP.P.X) / (rP.P.Z - rP.P0.Z));
            FrPhi = FrPhi + (Math.PI / 2) + FrAlpha;
            FrWinkel = FrPhi - FrAlpha;
            FrPsi = Math.Atan((rP.D0.X - rP.D.X) / (rP.D.Z - rP.D0.Z));
            FrPsi = FrPsi + (Math.PI / 2) + FrAlpha;
            //Wanten2dTo3d; Wantenlängen3d bleiben unverändert
            Rest();
        }
        public void Rest()
        {
            // Berechnung Punkt ooE
            rP.E.X = rP.E0.X - FrController;
            rP.E.Y = 0;
            rP.E.Z = rP.E0.Z;

            // Berechnung Punkt ooF
            BerechneF();
            BerechneM();

            // zweite Hälfte von iP füllen
            for (int i = Rigg.ooA; i <= Rigg.ooP; i++)
            {
                iP[i].x = Round(rP[i].X);
                iP[i].y = Round(rP[i].Y);
                iP[i].z = Round(rP[i].Z);
            }
        }
        private double VorstagLaenge(double psi)
        {
            // Viergelenk P0 P D D0, Koppelpunkt C	
            rP.D = SKK.AnglePointXZ(rP.D0, FrMastUnten, psi - FrAlpha);
            rP.P = SKK.IntersectionXZ1(rP.P0, rP.D, FrWunten2D, FrSalingH);
            rP.C = SKK.IntersectionXZ1(rP.P, rP.D, FrWoben2D, FrMastOben);
            return  rP.C0.Distance(rP.C);
        }
        public void BerechneWinkel()
        {
            // FrVorstag gegeben, FrWinkel gesucht
            int Counter;
            bool svar = false;
            double VorstagIst, Diff;
            double psiStart, psiEnde, psiA, psiB;
            TRealPoint localC, ooTemp1, ooTemp2;

            ResetStatus();
            Wanten3dTo2d();

            // 1. Startwinkel ermitteln
            // Durchbiegung Null, Mast gerade
            // linke Totlage für Winkel psi im Viergelenk D0 D C C0
            localC = SKK.IntersectionXZ1(rP.D0, rP.C0, FrMastUnten + FrMastOben, FrVorstag);

            psiStart = SKK.AngleXZM(rP.D0, localC);
            psiStart = (Math.PI / 2) + psiStart + FrAlpha;

            // Test, ob Wante locker bei Mast gerade und Vorstaglänge = FrVorstag.
            // Ermittlung der Koordinaten für diesen Fall.
            FrPsi = psiStart;

            rP.C = localC;
            rP.D = SKK.AnglePointXZ(rP.D0, FrMastUnten, FrPsi - FrAlpha);
            rP.P = SKK.IntersectionXZ1(rP.D, rP.C, FrSalingH, FrWoben2D);

            FrWanteZulang = rP.P0.Distance(rP.P) + rP.P.Distance(rP.C) - FrWunten2D - FrWoben2D;
            if (FrWanteZulang < 0)
            {
                FGetriebeOK = false;
                FGetriebeStatus.Include(Rigg.gsWanteZulang);
            }
            else
            {
                // wenn Wante nicht zu locker dann Suche fortsetzen

                // may jump to 'other' intersection of circle C0C and KoppelKurve
                if (WantToPlayWithExtendedSearchRange)
                {
                    // Suchbereich erweitern, wenn Durchbiegung negativ
                    // für psi = psiStart im VierGelenk P0 P D D0
                    ooTemp1 = rP.D - rP.D0;
                    ooTemp2 = rP.C - rP.D;
                    localC = ooTemp1.CrossProduct(ooTemp2);
                    if (localC.Y > 0)
                    {
                        psiStart += (45 * Math.PI / 180);
                    }
                }

                // 2. Endwinkel ermitteln - Mastoben parallel zu Vorstag
                // rechte Totlage für Winkel psi im Viergelenk D0 D C C0
                localC = SKK.IntersectionXZ1(rP.D0, rP.C0, FrMastUnten, FrVorstag - FrMastOben);
                psiEnde = SKK.AngleXZM(rP.D0, localC);
                psiEnde = Math.PI / 2 + psiEnde + FrAlpha;

                // 3. Winkel ermitteln, für den gilt: VorstagIst gleich FrVorstag
                // Viergelenk P0 P D D0, Koppelpunkt C
                psiB = psiStart;
                psiA = psiEnde + (0.01 * Math.PI / 180);
                Counter = 0;
                do
                {
                    Counter++;
                    FrPsi = (psiA + psiB) / 2;
                    VorstagIst = VorstagLaenge(FrPsi);
                    Diff = VorstagIst - FrVorstag;
                    if (Diff > 0)
                    {
                        psiB = FrPsi;
                    }
                    else
                    {
                        psiA = FrPsi;
                    }
                }
                while
                    ((Math.Abs(Diff) >= 0.01) && (Counter < 200));
            }

            // aktualisieren
            rP.A = rP.P;
            rP.A.Y = FrSalingA / 2;
            rP.B = rP.P;
            rP.B.Y = -rP.A.Y;
            FrPhi = Math.PI - RggCalc.PsiVonPhi(Math.PI - FrPsi, FrBasis, FrMastUnten, FrSalingH, FrWunten2D, ref svar);

            if (svar == false)
            {
                FGetriebeOK = false;
                FGetriebeStatus.Include(Rigg.gsErrorPsivonPhi);
                _ = LogList.AppendLine("TGetriebeFS.BerechneWinkel:");
                _ = LogList.AppendLine("  svar False in PsivonPhi");
                //ExitCounter2++;
                return;
            }
            FrWinkel = FrPhi - FrAlpha;
            FrSalingL = Math.Sqrt(RggCalc.Sqr(FrSalingH) + RggCalc.Sqr(FrSalingA / 2));
            Rest(); // think: refactored away with 'extract method refactoring'
        }
        public TRealPoint[] Koppelkurve()
        {
            // Koppelkurve Viergelenk P0, P, D, D0
            // Wanten2d neu bereitgestellt,
            // sonst interne Felder nicht verändert!

            bool svar = false;
            double phiA, phiE, phiM, psiM, WinkelStep;
            TRealPoint ooTemp;
            TRiggPoints oooTemp = new TRiggPoints();
            TRealPoint[] result = new TRealPoint[101];

            rP.CopyTo(oooTemp); // aktuelle Koordinaten sichern
            Wanten3dTo2d();

            // 1. Startwinkel
            ooTemp = SKK.IntersectionXZ1(rP.P0, rP.D0, FrWunten2D + FrSalingH, FrMastUnten);
            phiA = SKK.AngleXZM(rP.P0, ooTemp);
            phiA = phiA + Math.PI / 2 + FrAlpha;

            // 2. Endwinkel
            ooTemp = SKK.IntersectionXZ1(rP.P0, rP.D0, FrWunten2D, FrSalingH + FrMastUnten);
            if (base.SKK.Status == TBemerkungKK.bmK1inK2)
            {
                phiE = FrAlpha + (130 * Math.PI / 180);
            }
            else
            {
                phiE = SKK.AngleXZM(rP.P0, ooTemp);
                phiE = phiE + (Math.PI / 2) + FrAlpha;
            }

            // 3. Koppelkurve
            phiA += Math.PI / 180;
            phiE -= Math.PI / 180;
            WinkelStep = (phiE - phiA) / 100;
            phiM = phiA;
            for (int i = 0; i <= 100; i++)
            {
                psiM = RggCalc.PsiVonPhi(phiM, FrBasis, FrWunten2D, FrSalingH, FrMastUnten, ref svar);
                rP.P.X = rP.P0.X + (FrWunten2D * Math.Cos(phiM - FrAlpha));
                rP.P.Z = rP.P0.Z + (FrWunten2D * Math.Sin(phiM - FrAlpha));
                rP.D.X = rP.D0.X + (FrMastUnten * Math.Cos(psiM - FrAlpha));
                rP.D.Z = rP.D0.Z + (FrMastUnten * Math.Sin(psiM - FrAlpha));
                // Berechnung Punkt C
                rP.C = SKK.IntersectionXZ1(rP.P, rP.D, FrWoben2D, FrMastOben);
                result[i].X = rP.C.X;
                result[i].Y = 0;
                result[i].Z = rP.C.Z;
                phiM += WinkelStep;
            }

            oooTemp.CopyTo(rP); // aktuelle Koordinaten wiederherstellen
            return result;
        }
        public void BiegeUndNeigeF1(double Mastfall, double Biegung)
        {
            double k1, k2, k3, k4, k5, k6, k7;
            double tempAlpha, tempBeta, tempGamma;

            ResetStatus();
            if (SalingTyp == TSalingTyp.stDrehbar)
            {
                Wanten3dTo2d();
            }

            // 1. Berechnung Länge D0F aus Durchbiegung
            k1 = Math.Sqrt(RggCalc.Sqr(FrMastUnten) - RggCalc.Sqr(Biegung));
            k2 = Math.Sqrt(RggCalc.Sqr(FrMastOben) - RggCalc.Sqr(Biegung));
            tempAlpha = Math.Atan2(Biegung , k1);
            k4 = (k1 + k2) * Math.Sin(tempAlpha);
            k6 = (k1 + k2) * Math.Cos(tempAlpha);
            tempGamma = Math.Atan2(k4, k6 - FrMastUnten);
            k5 = (FrMastOben + FrMastEnde) * Math.Sin(tempGamma);
            k7 = (FrMastOben + FrMastEnde) * Math.Cos(tempGamma);
            tempBeta = Math.Atan2(k5, FrMastUnten + k7);
            k3 = Math.Sqrt(RggCalc.Sqr(k5) + RggCalc.Sqr(FrMastUnten + k7)); // oder k3 = k5/Math.Sin(tempBeta)
            // k3 = Abstand D0F

            // Bessere Werte für k3 und tempBeta bestimmen
            KorrekturF(Biegung, k1, k2, ref k3, ref tempBeta, ref tempGamma); // virtuelle Methode

            // 2. Berechnung Punkt F mit Mastfall
            rP.F = SKK.IntersectionXZ1(rP.F0, rP.D0, Mastfall + FiMastfallVorlauf, k3);

            // 3. psi, D, und C ermitteln
            FrPsi = SKK.AngleXZM(rP.D0, rP.F);
            FrPsi = FrPsi + (Math.PI / 2) + FrAlpha - tempBeta;

            rP.D = SKK.AnglePointXZ(rP.D0, FrMastUnten, FrPsi - FrAlpha);
            rP.C = SKK.AnglePointXZ(rP.D, FrMastOben, FrPsi - FrAlpha + tempGamma);

            FrVorstag = rP.C0.Distance(rP.C);

            // 4. restliche Aktualisierungen vornehmen
            switch (SalingTyp)
            {
                case TSalingTyp.stFest:
                    MakeSalingHBiggerFS(FrSalingH);
                    break;
                case TSalingTyp.stDrehbar:
                    MakeSalingLBiggerDS(FrSalingL);
                    break;
            }
        }
        public void BiegeUndNeigeFS(TTrimm TrimmSoll, ref double SalingHStart)
        {
            // var Parameter SalingHStart wird vom Regler benötigt
            TRealPoint ooTemp;

            BiegeUndNeigeF1(TrimmSoll.Mastfall, TrimmSoll.BiegungS);

            // 4. Startwert für FrSalingH ermitteln
            ooTemp = (rP.C - rP.P0).Normalize();
            ooTemp *= FrWoben2D;
            rP.P = rP.C + ooTemp;
            SalingHStart = rP.P.Distance(rP.D);
            FrSalingH = Math.Truncate(SalingHStart) + 1; // FiSalingH garantiert größer

            // 5. restliche Aktualisierungen in MakeSalingHBiggerFS vornehmen!
            MakeSalingHBiggerFS(SalingHStart);
        }
        public void BiegeUndNeigeDS(TTrimm TrimmSoll, ref double SalingLStart)
        {
            // var Parameter SalingLStart wird vom Regler benötigt
            TRealPoint ooTemp;

            BiegeUndNeigeF1(TrimmSoll.Mastfall, TrimmSoll.BiegungS);

            // Startwert für SalingL ermitteln
            ooTemp = (rP.C - rP.A0).Normalize();
            ooTemp *= FrWoben3D;
            rP.A = rP.C + ooTemp;
            SalingLStart = rP.A.Distance(rP.D);
            FrSalingL = Math.Truncate(SalingLStart) + 1; // FiSalingL dann garantiert größer!

            // restliche Aktualisierungen in MakeSalingLBiggerDS vornehmen
            MakeSalingLBiggerDS(SalingLStart);
        }
        public void BiegeUndNeigeC(double MastfallC, double Biegung)
        {
            double k1, k2;
            double tempAlpha;

            ResetStatus();
            if (SalingTyp == TSalingTyp.stDrehbar)
            {
                Wanten3dTo2d();
            }

            // Zweischlag ausgehend von Durchbiegung
            k1 = Math.Sqrt((FrMastUnten * FrMastUnten) - (Biegung * Biegung));
            k2 = Math.Sqrt((FrMastOben * FrMastOben) - (Biegung * Biegung));
            tempAlpha = Math.Atan2(Biegung, k1);

            // Punkt C
            rP.C = SKK.IntersectionXZ1(rP.F0, rP.D0, MastfallC, k1 + k2);

            // psi und Punkt D
            FrPsi = SKK.AngleXZM(rP.D0, rP.C);
            FrPsi = FrPsi + (Math.PI / 2) + FrAlpha - tempAlpha;

            rP.D = SKK.AnglePointXZ(rP.D0, FrMastUnten, FrPsi - FrAlpha);

            // Vorstag
            FrVorstag = rP.C0.Distance(rP.C);

            // Punkt F, M
            BerechneF();
            BerechneM();

            // restliche Aktualisierungen
            switch (SalingTyp)
            {
                case TSalingTyp.stFest:
                    MakeSalingHBiggerFS(FrSalingH);
                    break;
                case TSalingTyp.stDrehbar:
                    MakeSalingLBiggerDS(FrSalingL);
                    break;
            }
        }
        public void NeigeF(double Mastfall)
        {

            TRealPoint D0;

            TRealPoint oldF;
            TRealPoint oldC;
            TRealPoint oldD;

            TRealPoint newF;
            TRealPoint newC;
            TRealPoint newD;

            double D0F; // k3
            double D0C; // k1 + k2
            double D0D; // l4 (FrMastUnten)

            double newF0F;

            double oldPsi;
            double newPsi;
            double delta;
            double w;

            oldF = rP.F;
            oldC = rP.C;
            oldD = rP.D;
            D0 = rP.D0;
            D0F = rP.D0.Distance(rP.F);
            D0C = rP.D0.Distance(rP.C);
            D0D = FrMastUnten;

            // compute new Point F

            newF0F = Mastfall + MastfallVorlauf;
            SKK.SchnittEbene = TSchnittEbene.seXZ;
            SKK.Radius1 = newF0F;
            SKK.Radius2 = D0F; // unchanged
            SKK.MittelPunkt1 = rP.F0;
            SKK.MittelPunkt2 = rP.D0;
            rP.F = SKK.SchnittPunkt1;

            // compute new Points C and D

            newF = rP.F;
            oldPsi = Math.PI / 2 - SKK.AngleXZ(oldF, D0);
            newPsi = Math.PI / 2 - SKK.AngleXZ(newF, D0);
            delta = newPsi - oldPsi;

            w = Math.PI / 2 - SKK.AngleXZ(oldC, D0);
            w += delta;
            newC.X = D0.X + D0C * Math.Cos(w);
            newC.Y = 0;
            newC.Z = D0.Z + D0C * Math.Sin(w);

            w = Math.PI / 2 - SKK.AngleXZ(oldD, D0);
            w += delta;
            newD.X = D0.X + D0D * Math.Cos(w);
            newD.Y = 0;
            newD.Z = D0.Z + D0D * Math.Sin(w);

            rP.C = newC;
            rP.D = newD;

            // continue as in original BiegeUndNeigeF

            FrVorstag = rP.C0.Distance(rP.C);

            switch (SalingTyp)
            {
                case TSalingTyp.stFest:
                    MakeSalingHBiggerFS(FrSalingH);
                    break;
                case TSalingTyp.stDrehbar:
                    MakeSalingLBiggerDS(FrSalingL);
                    break;
            }

            BerechneM();
        }

        public void MakeSalingHBiggerFS(double SalingHplus)
        {
            // FrSalingH größer machen, FrWoben2d, Neigung und Biegung beibehalten;
            // FrWunten2d neu berechnen

            FrSalingH = SalingHplus;

            rP.P = SKK.IntersectionXZ1(rP.D, rP.C, FrSalingH, FrWoben2D);
            FrWunten2D = rP.P.Distance(rP.P0);

            // aktualisieren
            rP.A = rP.P;
            rP.A.Y = FrSalingA / 2;
            rP.B = rP.P;
            rP.B.Y = -FrSalingA / 2;
            FrPhi = SKK.AngleXZM(rP.A0, rP.A);
            FrPhi = FrAlpha + (Math.PI / 2) + FrPhi;
            FrWinkel = FrPhi - FrAlpha;
            FrSalingL = Math.Sqrt(RggCalc.Sqr(FrSalingH) + RggCalc.Sqr(FrSalingA / 2));
            FrController = FiControllerAnschlag;
            Wanten2dTo3d();
        }
        public void MakeSalingLBiggerDS(double SalingLplus)
        {
            TRealPoint TempA, TempC, TempD, Temp;
            double Basis, Skalar, WStrich, W1Strich, Saling1L;

            // Punkte D, C und F schon bekannt, FrWoben3d bleibt erhalten
            FrSalingL = SalingLplus;

            WStrich = rP.A0.Distance(rP.C);
            Basis = rP.A0.Distance(rP.D);
            // weiter mit Koordinatentransformation,
            // ebenes Trapez A0, A, C, D
            // Berechnung TempD
            TempD = Vcalc.Null;
            TempD.X = Basis;
            // Berechnung TempC  
            SKK.SchnittEbene = TSchnittEbene.seXZ;
            SKK.Radius1 = WStrich;
            SKK.Radius2 = FrMastOben;
            SKK.MittelPunkt1 = Vcalc.Null;
            SKK.MittelPunkt2 = TempD;
            TempC = SKK.SchnittPunkt1; // bleibt beim Regeln unverändert

            // Berechnung TempA    
            SKK.SchnittEbene = TSchnittEbene.seXZ;
            SKK.Radius1 = FrSalingL; // verändert sich beim Regeln
            SKK.Radius2 = FrWoben3D; // bleibt gleich beim Regeln
            SKK.MittelPunkt1 = TempD;
            SKK.MittelPunkt2 = TempC;
            TempA = SKK.SchnittPunkt1; // verändert sich beim Regeln

            Temp = Vcalc.Null;
            RggCalc.SchnittGG(Vcalc.Null, TempC, TempD, TempA, ref Temp);
            // Temp enthält jetzt den Schnittpunkt der Diagonalen
            W1Strich = Temp.Length();
            Saling1L = TempD.Distance(Temp);

            // weiter r äumlich:
            Skalar = W1Strich / WStrich;
            Temp = rP.C - rP.A0;
            Temp *= Skalar;
            Temp = rP.A0 + Temp;
            // Temp enthält jetzt den räumlichen Schnittpunkt der Diagonalen

            // Berechnung Punkt ooA
            Skalar = FrSalingL / Saling1L;
            Temp -= rP.D;
            Temp *= Skalar;
            rP.A = rP.D + Temp;

            // FrWunten3d ermitteln und aktualisieren
            FrWunten3D = TempA.Length();
            rP.P = rP.A;
            rP.P.Y = 0;
            rP.B = rP.A;
            rP.B.Y = -rP.A.Y;
            FrPhi = SKK.AngleXZM(rP.A0, rP.A);
            FrPhi = FrPhi + (Math.PI / 2) + FrAlpha;
            FrWinkel = FrPhi - FrAlpha;
            FrSalingA = 2 * rP.B.Y;
            FrSalingH = rP.P.Distance(rP.D);
            FrController = FiControllerAnschlag;
        }
        public void GetWantenSpannung()
        {
            double VorstagNull;
            VorstagNull = GetVorstagNull();
            VorstagDiff = VorstagNull - FrVorstag;
            SpannungW = VorstagDiff < 0 ? 0 : WantenKraftvonVorstag(VorstagDiff);
            WantenSpannung = Round(SpannungW);
        }
        public double WantenKraftvonVorstag(double WegSoll)
        {
            // liefert Wantenspannung 3D in Abh�ngigkeit von der Auslenkung des Vorstags
            return TrimmTab.EvalX(WegSoll);
        }
        public double GetVorstagNull()
        {
            TRealPoint Temp, TempP, TempD, TempC;
            string s;
            double WStrich, WStrich2d, result;

            result = 0;
            try
            {
                base.SKK.SchnittEbene = TSchnittEbene.seXZ;

                switch (SalingTyp)
                {
                    case TSalingTyp.stFest:
                        // 1. Aufruf SchnittKK: Saling2d und WanteOben2d;
                        // Schnittpunkt Temp wird im 2. Aufruf benötigt
                        SKK.Radius1 = FrSalingH;
                        SKK.Radius2 = FrWoben2D;
                        Temp = SKK.Null;
                        Temp.X = FrMastUnten;
                        SKK.MittelPunkt1 = Temp;
                        //Temp = Null;
                        Temp.X = FrMastUnten + FrMastOben;
                        SKK.MittelPunkt2 = Temp;
                        Temp = SKK.SchnittPunkt1;
                        s = SKK.Bemerkung;
                        s = string.Format(CultureInfo.InvariantCulture, "GetVorstagNull, stFest/1: {0}", s);
                        _ = LogList.AppendLine(s);

                        // 2. Aufruf SchnittKK: TempP ermitteln
                        SKK.Radius1 = FrWunten2D;
                        SKK.Radius2 = Temp.Length(); // Temp unter 1. ermittelt
                        SKK.MittelPunkt1 = rP.P0;
                        SKK.MittelPunkt2 = rP.D0;
                        TempP = SKK.SchnittPunkt1;
                        TempP.Y = 0;
                        s = SKK.Bemerkung;
                        s = string.Format(CultureInfo.InvariantCulture, "GetVorstagNull, stFest/2: {0}", s);
                        _ = LogList.AppendLine(s);

                        // 3. Aufruf SchnittKK: Saling2d und MastUnten; TempD ermitteln
                        SKK.Radius1 = FrSalingH;
                        SKK.Radius2 = FrMastUnten;
                        SKK.MittelPunkt1 = TempP;
                        SKK.MittelPunkt2 = rP.D0;
                        TempD = SKK.SchnittPunkt1;
                        TempD.Y = 0;
                        s = SKK.Bemerkung;
                        s = string.Format(CultureInfo.InvariantCulture, "GetVorstagNull, stFest/3: {0}", s);
                        _ = LogList.AppendLine(s);

                        // 4. Aufruf SchnittKK: WanteOben2d und MastOben; TempC ermitteln
                        SKK.Radius1 = FrWoben2D;
                        SKK.Radius2 = FrMastOben;
                        SKK.MittelPunkt1 = TempP;
                        SKK.MittelPunkt2 = TempD;
                        TempC = SKK.SchnittPunkt1;
                        TempC.Y = 0;
                        s = SKK.Bemerkung;
                        s = string.Format(CultureInfo.InvariantCulture, "GetVorstagNull, stFest/4: {0}", s);
                        _ = LogList.AppendLine(s);

                        result = rP.C0.Distance(TempC);
                        break;

                    case TSalingTyp.stDrehbar:
                        SKK.Radius1 = FrSalingL;
                        SKK.Radius2 = FrWoben3D;
                        TempD = SKK.Null;
                        TempD.X = FrMastUnten;
                        SKK.MittelPunkt1 = TempD;
                        TempC = SKK.Null;
                        TempC.X = FrMastUnten + FrMastOben;
                        SKK.MittelPunkt2 = TempC;
                        TempP = SKK.SchnittPunkt1;
                        TempP.Y = 0;
                        s = SKK.Bemerkung;
                        s = string.Format(CultureInfo.InvariantCulture, "GetVorstagNull, stDrehbar/1: {0}", s);
                        _ = LogList.AppendLine(s);

                        SKK.Radius1 = rP.D0.Distance(rP.A0);
                        SKK.Radius2 = FrWunten3D;
                        SKK.MittelPunkt1 = SKK.Null;
                        SKK.MittelPunkt2 = TempP;
                        Temp = SKK.SchnittPunkt1;
                        Temp.Y = 0;
                        s = base.SKK.Bemerkung;
                        s = string.Format(CultureInfo.InvariantCulture, "GetVorstagNull, stDrehbar/2: {0}", s);
                        _ = LogList.AppendLine(s);

                        WStrich = Temp.Distance(TempC);
                        WStrich2d = Math.Sqrt(RggCalc.Sqr(WStrich) - RggCalc.Sqr(rP.A0.Y));

                        SKK.Radius1 = WStrich2d;
                        SKK.Radius2 = FrMastUnten + FrMastOben;
                        SKK.MittelPunkt1 = rP.P0;
                        SKK.MittelPunkt2 = rP.D0;
                        TempC = SKK.SchnittPunkt1;
                        TempC.Y = 0;
                        s = SKK.Bemerkung;
                        s = string.Format(CultureInfo.InvariantCulture, "GetVorstagNull, stDrehbar/3: {0}", s);
                        _ = LogList.AppendLine(s);

                        result = rP.C0.Distance(TempC);
                        break;

                    default:
                        // case TSalingTyp.stOhne, TSalingTyp.stOhne_2:          
                        // 1. Aufruf SchnittKK: Wante2d und Mast; TempC ermitteln
                        SKK.Radius1 = FrWunten2D + FrWoben2D;
                        SKK.Radius2 = FrMastUnten + FrMastOben;
                        SKK.MittelPunkt1 = rP.P0;
                        SKK.MittelPunkt2 = rP.D0;
                        TempC = SKK.SchnittPunkt1;
                        TempC.Y = 0;
                        s = SKK.Bemerkung;
                        s = string.Format(CultureInfo.InvariantCulture, "GetVorstagNull, stOhne/1: {0}", s);
                        _ = LogList.AppendLine(s);
                        result = rP.C0.Distance(TempC);
                        break;
                }
            }
            catch (Exception ex) // on EMathError do
            {
                _ = LogList.AppendLine("TGetriebeFS.GetVorstagNull:  " + ex.Message);
            }
            return result;
        }
        public double VorstagDiff { get; private set; }
        public double SpannungW { get; private set; }
    }
}
