using System;

namespace RiggVar.Rgg
{
    public class TFachwerk
    {
        const int fw1 = 0;
        const int fw2 = 1;
        const int fw3 = 2;
        const int fw4 = 3;
        const int fw5 = 4;
        const int fw6 = 5;
        const int fw7 = 6;
        const int fw8 = 7;
        const int fw9 = 8;

        const int TStabVektor = 9;
        const int TKnotenVektor = 6;
        const int TAuflager = 4;

        const int AX = 0;
        const int AY = 1;
        const int BX = 2;
        const int BY = 3;

        // Geometriematrix, Stab in Spalte verbindet die beiden Knoten
        private readonly int[,] constantG = {{ 1, 1, 2, 2, 3, 3, 4, 4, 5 },
                                             { 4, 5, 6, 3, 5, 4, 6, 5, 6 }};

        // EA in N
        private readonly double[] constantEA = { 1E6, 1E6, 2E6, 2E6, 2E6, 2E6, 10E6, 10E6, 10E6 };

        // Anfangswerte für Knotenpunkt-Koordinaten in mm
        private readonly double[] InitX = { 2870, 2300, 2800, 4140, 2870, 2560 };
        private readonly double[] InitY = { 400, 2600, 4600, 340, -100, 430 };

        // Anfangswerte für äußere Belastung in N
        private readonly double[] BelastungX = { 0.00, 0.00, 0.00, 0, 0.00, 0 };
        private readonly double[] BelastungY = { 0.00, 0.00, 0.00, 0, 0.00, 0 };

        // Richtungen der Kraft "1" für Ermittlung der Knotenverschiebungen in Grad
        private readonly double[] PO1 = { 0, 0, 0, 0, 0, 0 };
        private readonly double[] PO2 = { -90, -90, -90, -90, -90, -90 };

        // Vektoren zum bequemen Löschen der Arrays
        public double[] ClearVektorK = { 0, 0, 0, 0, 0, 0 };
        public double[] ClearVektorS = { 0, 0, 0, 0, 0, 0, 0, 0, 0 };

        public int K; // Anzahl der Knoten
        public int K1; // KnotenNr. des Festlagers A
        public int K2; // KnotenNr. des Loslagers B
        public double Phi; // Winkel von FB in Grad
        public int S; // Anzahl der Stäbe
        public int[,] G = new int[2, 9]; // TGeometrie; Geometriematrix
        public double[] vektorEA = new double[TStabVektor]; // Vektor EA
        public double[] KX = new double[TKnotenVektor]; // Koordinaten
        public double[] KY = new double[TKnotenVektor]; // Koordinaten
        public double[] FX = new double[TKnotenVektor]; // Belastung
        public double[] FY = new double[TKnotenVektor]; // Belastung
        public double[] FXsaved = new double[TKnotenVektor]; // Kopie des Belastungsvektors
        public double[] FYsaved = new double[TKnotenVektor]; // Kopie des Belastungsvektors
        public double[] FO = new double[TKnotenVektor]; // Verschiebungen
        public double[] FO1 = new double[TKnotenVektor]; // Verschiebungen
        public double[] FO2 = new double[TKnotenVektor]; // Verschiebungen
        public double[] Lager = new double[TAuflager]; // Auflagerreaktionen
        public double P; // Winkel der Auflagekraft im Loslager B in Rad
        public double[] Q = new double[TStabVektor]; // l/EA Vektor
        public double[] FS1 = new double[TStabVektor]; // Puffer für Stabkräfte
        public double[] FS = new double[TStabVektor]; // Speicher-Vektor der Stabkräfte bei äußerer Last
        public double SummeFX, SummeFY, SummeMO; //summierte Belastungen
        public double DX1, DY1, W1;
        public double DX2, DY2, W2;
        public double DX3, DY3, W3;
        public double DX4, DY4, W4;
        public double D, D1, D2, W;
        public double BekanntFX, BekanntFY;
        public double WantenPower;
        public double MastDruck;
        public TSalingTyp SalingTyp;
        public bool BerechneVerschiebungen;

        public TFachwerk()
        {
            BerechneVerschiebungen = false;
            SalingTyp = TSalingTyp.stFest;
            K = 6; // Anzahl der Knoten
            K1 = fw6; // KnotenNr. des Festlagers A
            K2 = fw5; // KnotenNr. des Loslagers B
            Phi = 30; // Winkel von FB in Grad
            S = (2 * K) - 3; // Anzahl der Stäbe
            G = constantG;
            vektorEA = constantEA;
            KX = InitX;
            KY = InitY;
            FX = BelastungX;
            FY = BelastungY;
            P = Phi * Math.PI / 180;
            FS1 = ClearVektorS;
        }

        private double Sqr(double a)
        {
            return a * a;
        }
        public void KG22(int l, int l1, int l2, int l3, int l4, int i1, int i2, int i3, int i4)
        {
            // unbekannte Kraft Nr.1
            DX1 = KX[l1] - KX[l]; // delta x
            DY1 = KY[l1] - KY[l]; // delta y
            W1 = Math.Sqrt(Sqr(DX1) + Sqr(DY1)); // Stablänge
            // unbekannte Kraft Nr.2
            DX2 = KX[l2] - KX[l]; // delta x 
            DY2 = KY[l2] - KY[l]; // delta y 
            W2 = Math.Sqrt(Sqr(DX2) + Sqr(DY2)); // Stablänge 
            // bekannte Kraft Nr.3 
            DX3 = KX[l3] - KX[l]; //delta x 
            DY3 = KY[l3] - KY[l]; //delta y 
            W3 = Math.Sqrt(Sqr(DX3) + Sqr(DY3)); //Stablänge 
            // bekannte Kraft Nr.4 
            DX4 = KX[l4] - KX[l]; // delta x 
            DY4 = KY[l4] - KY[l]; // delta y 
            W4 = Math.Sqrt(Sqr(DX4) + Sqr(DY4)); // Stablänge 
            // Summe der bekannten Kräfte 
            // mit DX/W = cos alpha,
            // und DY/W = sin alpha 
            BekanntFX = -FX[l] - (FS1[i3] * DX3 / W3) - (FS1[i4] * DX4 / W4);
            BekanntFY = -FY[l] - (FS1[i3] * DY3 / W3) - (FS1[i4] * DY4 / W4);
            // Ausrechnen der Stabkräfte 
            D = (DX1 * DY2) - (DX2 * DY1);
            D1 = (BekanntFX * DY2) - (BekanntFY * DX2);
            D2 = (BekanntFY * DX1) - (BekanntFX * DY1);
            FS1[i1] = D1 / D * W1; // 1. neu ermittelte Stabkraft 
            FS1[i2] = D2 / D * W2; // 2. neu ermittelte Stabkraft 
        }
        public void KG20(int l, int l1, int l2, int i1, int i2)
        {
            // unbekannte Kraft Nr.1 
            DX1 = KX[l1] - KX[l]; // delta x 
            DY1 = KY[l1] - KY[l]; // delta y 
            W1 = Math.Sqrt(Sqr(DX1) + Sqr(DY1)); // Stablänge 
            // unbekannte Kraft Nr.2 
            DX2 = KX[l2] - KX[l]; // delta x 
            DY2 = KY[l2] - KY[l]; // delta y 
            W2 = Math.Sqrt(Sqr(DX2) + Sqr(DY2)); // Stablänge 
            // Summe der bekannten Kräfte 
            BekanntFX = -FX[l];
            BekanntFY = -FY[l];
            // Ausrechnen der Stabkräfte 
            D = (DX1 * DY2) - (DX2 * DY1);
            D1 = (BekanntFX * DY2) - (BekanntFY * DX2);
            D2 = (BekanntFY * DX1) - (BekanntFX * DY1);
            FS1[i1] = D1 / D * W1; // 1. neu ermittelte Stabkraft 
            FS1[i2] = D2 / D * W2; // 2. neu ermittelte Stabkraft 
        }
        public void KG21(int l, int l1, int l2, int l3, int i1, int i2, int i3)
        {
            // unbekannte Kraft Nr.1 
            DX1 = KX[l1] - KX[l]; // delta x 
            DY1 = KY[l1] - KY[l]; // delta y 
            W1 = Math.Sqrt(Sqr(DX1) + Sqr(DY1)); // Stablänge 
            //unbekannte Kraft Nr.2 
            DX2 = KX[l2] - KX[l]; // delta x 
            DY2 = KY[l2] - KY[l]; // delta y 
            W2 = Math.Sqrt(Sqr(DX2) + Sqr(DY2)); // Stablänge 
            // bekannte Kraft Nr.3 
            DX3 = KX[l3] - KX[l]; //delta x 
            DY3 = KY[l3] - KY[l]; //delta y 
            W3 = Math.Sqrt(Sqr(DX3) + Sqr(DY3)); // Stablänge 
            // Summe der bekannten Kräfte 
            // mit DX/W = cos alpha,
            // und DY/W = sin alpha 
            BekanntFX = -FX[l] - (FS1[i3] * DX3 / W3);
            BekanntFY = -FY[l] - (FS1[i3] * DY3 / W3);
            // Ausrechnen der Stabkräfte 
            D = (DX1 * DY2) - (DX2 * DY1);
            D1 = (BekanntFX * DY2) - (BekanntFY * DX2);
            D2 = (BekanntFY * DX1) - (BekanntFX * DY1);
            FS1[i1] = D1 / D * W1; // 1. neu ermittelte Stabkraft 
            FS1[i2] = D2 / D * W2; // 2. neu ermittelte Stabkraft 
        }
        public void Stabkraefte()
        {
            // Puffer der ermittelten Stabkräfte ist Vektor FS1
            KG20(fw1, fw5, fw4, fw2, fw1);

            if (SalingTyp == TSalingTyp.stOhneStarr)
            {
                // Eine Kraft vorgeben!
                FS1[fw4] = WantenPower;
                FS1[fw3] = WantenPower;
            }
            else
            {
                KG20(fw2, fw3, fw6, fw4, fw3);
            }

            KG21(fw3, fw4, fw5, fw2, fw6, fw5, fw4);
            KG22(fw4, fw5, fw6, fw3, fw1, fw8, fw7, fw6, fw1);
            KG21(fw6, fw5, fw4, fw2, fw9, fw7, fw3);
        }
        public void Stabkraefte_2()
        {
            // bei stOhne_2, Controllerzweischlag
            KG20(fw1, fw5, fw4, fw2, fw1);

            // Punkt C, MastDruck vorgegeben 
            FS1[fw5] = MastDruck;
            KG21(fw3, fw4, fw2, fw5, fw6, fw4, fw5);

            // Punkt P, weil Ohne Saling:
            FS1[fw3] = FS1[fw4];

            KG22(fw4, fw5, fw6, fw3, fw1, fw8, fw7, fw6, fw1);
            KG21(fw6, fw5, fw4, fw2, fw9, fw7, fw3);
        }
        public void Auflagerkraefte(double SumFX, double SumFY, double SumMO, double[] Lager)
        {
            double FB = -SumMO / (((KX[K2] - KX[K1]) * Math.Sin(P)) - ((KY[K2] - KY[K1]) * Math.Cos(P)));
            Lager[BX] = FB * Math.Cos(P);
            Lager[BY] = FB * Math.Sin(P);
            Lager[AX] = -SumFX - Lager[BX];
            Lager[AY] = -SumFY - Lager[BY];
        }
        public void Verschiebungen()
        {
            double Sum1FX, Sum1FY, Sum1MO;
            double[] Lager1 = new double[4];
            double PORad;
            double F;

            PORad = 0;
            for (int l = 0; l < K; l++)
            {
                for (int j = 0; j <= 1; j++)
                {
                    // 1. neue Richtung aus Vektor holen.
                    if (j == 0)
                    {
                        PORad = PO1[l] * Math.PI / 180; // Richtung in Rad 
                    }
                    else if (j == 1)
                    {
                        PORad = PO2[l] * Math.PI / 180; // Richtung in Rad 
                    }

                    // 2. FW nur mit Kraft "1" auf Knoten l belasten.  
                    Sum1FX = Math.Cos(PORad);  // x-Komponenten der Last "1" 
                    Sum1FY = Math.Sin(PORad);  // y-Komponenten der Last "1" 
                    Sum1MO = (Sum1FY * (KX[l] - KX[K1])) - (Sum1FX * (KY[l] - KY[K1]));

                    // 3. Auflagerkräfte für Belatung mit "1" ausrechnen. 
                    Auflagerkraefte(Sum1FX, Sum1FY, Sum1MO, Lager1);

                    // 4. Belastungs-Vektor für "1" - Belastung aktualisieren.  
                    FX = ClearVektorK;
                    FY = ClearVektorK;
                    FX[l] = Sum1FX;
                    FY[l] = Sum1FY;
                    FX[K1] = FX[K1] + Lager1[AX];
                    FY[K1] = FY[K1] + Lager1[AY];
                    FX[K2] = FX[K2] + Lager1[BX];
                    FY[K2] = FY[K2] + Lager1[BY];

                    // 5. Stabkräfte bei "1" - Belastung berechnen.  
                    Stabkraefte();

                    // 6. Verschiebungsanteile summieren.  
                    F = 0;
                    for (int i = 0; i < S; i++)
                    {
                        Q[i] = Math.Sqrt(Sqr(KX[G[0, i]] - KX[G[1, i]]) + Sqr(KY[G[0, i]] - KY[G[1, i]]));
                        Q[i] = Q[i] / vektorEA[i];
                        F += (FS[i] * FS1[i] * Q[i]);
                    }

                    // 7. Verschiebungskomponente des Knotens k im Vektor speichern.  
                    if (j == 0)
                    {
                        FO1[l] = F;
                    }
                    else if (j == 1)
                    {
                        FO2[l] = F;
                    }
                }

                // Speichern des absoluten Verschiebungsbetrages des Knotens l 
                FO[l] = Math.Sqrt(Sqr(FO1[l]) + Sqr(FO2[l]));
            }
        }
        public void ActionF()
        {
            // Belastungsvektor für die Ausgabe sichern;
            // FX und FY werden bei der Berechnung der Verschiebungen überschrieben
            FX.CopyTo(FXsaved, 0);
            FY.CopyTo(FYsaved, 0);

            // Aufsummieren der äußeren Belastung
            SummeFX = 0;
            SummeFY = 0;
            SummeMO = 0;
            for (int l = 0; l < K; l++)
            {
                SummeFX += FX[l];
                SummeFY += FY[l];
                SummeMO = SummeMO + (FY[l] * (KX[l] - KX[K1])) - (FX[l] * (KY[l] - KY[K1]));
            }

            // Auflagerkräfte ausrechnen
            Auflagerkraefte(SummeFX, SummeFY, SummeMO, Lager);

            // Die Auflagerkräfte werden zu den Knotenlasten addiert:
            FX[K1] = FX[K1] + Lager[AX];
            FY[K1] = FY[K1] + Lager[AY];
            FX[K2] = FX[K2] + Lager[BX];
            FY[K2] = FY[K2] + Lager[BY];

            // Stabkräfte ausrechnen
            if (SalingTyp == TSalingTyp.stOhneBiegt)
            {
                Stabkraefte_2();
            }
            else
            {
                Stabkraefte();
            }

            // Stabkräfte von FS1 nach FS kopieren.
            // Die Prozedur Stabkräfte speichert die ermittelten Stabkräfte immer in FS1!
            for (int i = 0; i < S; i++)
            {
                FS[i] = FS1[i];
            }

            if ((SalingTyp == TSalingTyp.stOhneStarr)
                || (SalingTyp == TSalingTyp.stOhneBiegt)
                && (BerechneVerschiebungen == true))
            {
                Verschiebungen(); // Verschiebungen ausrechnen
            }
            // Jetzt sind die Stabkräfte unter Last "1" im Vektor FS1 gespeichert,
            // die Stabkräfte unter der wirklichen Last immer noch in Vektor FS.
        }

    }

}

