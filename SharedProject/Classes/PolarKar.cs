using System;
using System.Diagnostics;

namespace RiggVar.Rgg
{
    public class TPolarKar
    {
        public Vec3 NullVec = new Vec3(0, 0, 0);
        public Vec3 xVec = new Vec3(1, 0, 0);
        public Vec3 yVec = new Vec3(0, 1, 0);
        public Vec3 zVec = new Vec3(0, 0, 1);

        public int OnCalcAngle = 0;

        private double FPhi, FTheta, FGamma, FXrot, FYrot, FZrot;
        private bool FValid;
        private bool FMode;
        protected Vec3 p1, p2;
        protected double angle;
        protected TMatrix4x4 tmat;
        public TMatrix4x4 mat;

        public TPolarKar()
        {
            mat = new TMatrix4x4();
            tmat = new TMatrix4x4();
            Reset();
        }

        protected void GetMat()
        {
            if (Mode)
            {
                FillMatrix();
            }
            else
            {
                FillMatrixInc();
            }
        }
        protected void FillMatrix()
        {
            mat.Identity();
            // 1. Rotation um globale y-Achse, gleichzeitig lokale y-Achse  
            p1.x = 0.0; p1.y = 0.0; p1.z = 0.0;
            p2.x = 0.0; p2.y = 1.0; p2.z = 0.0;
            angle = FTheta;
            mat.Rotate(p1, p2, angle);
            // 2. Rotation um globale z-Achse  
            p1.x = 0.0; p1.y = 0.0; p1.z = 0.0;
            p2.x = 0.0; p2.y = 0.0; p2.z = 1.0;
            angle = FPhi;
            mat.Rotate(p1, p2, angle);
            // 3. Rotation um locale x-Achse  
            p1.x = 0.0; p1.y = 0.0; p1.z = 0.0;
            p2.x = mat.mat.m[0, 0];
            p2.y = mat.mat.m[1, 0];
            p2.z = mat.mat.m[2, 0];
            angle = FGamma;
            mat.Rotate(p1, p2, angle);
            FValid = true;
        }
        protected void FillMatrixInc()
        {
            tmat.Identity();
            p1 = NullVec;
            if (FTheta != 0)
            {
                p2.x = mat.mat.m[0, 1];
                p2.y = mat.mat.m[1, 1];
                p2.z = mat.mat.m[2, 1];
                angle = FTheta;
                tmat.Rotate(p1, p2, angle);
                FTheta = 0;
            }
            if (FPhi != 0)
            {
                p2.x = mat.mat.m[0, 0];
                p2.y = mat.mat.m[1, 0];
                p2.z = mat.mat.m[2, 0];
                angle = -FPhi;
                tmat.Rotate(p1, p2, angle);
                FPhi = 0;
            }
            if (FGamma != 0)
            {
                p2.x = mat.mat.m[0, 2];
                p2.y = mat.mat.m[1, 2];
                p2.z = mat.mat.m[2, 2];
                angle = FGamma;
                tmat.Rotate(p1, p2, angle);
                FGamma = 0;
            }
            if (FZrot != 0)
            {
                p2 = yVec;
                angle = FZrot;
                tmat.Rotate(p1, p2, angle);
                FZrot = 0;
            }
            if (FYrot != 0)
            {
                p2 = xVec;
                angle = FYrot;
                tmat.Rotate(p1, p2, angle);
                FYrot = 0;
            }
            if (FXrot != 0)
            {
                p2 = zVec;
                angle = FXrot;
                tmat.Rotate(p1, p2, angle);
                FXrot = 0;
            }
            FValid = true;
            mat.PreMultiply(tmat.mat);
        }
        public TRealPoint Rotiere(TRealPoint Punkt)
        {
            Vec3 temp;

            if (FValid == false)
            {
                GetMat();
            }

            temp.x = Punkt.X;
            temp.y = Punkt.Y;
            temp.z = Punkt.Z;
            mat.TransformPoint(ref temp);
            TRealPoint result;
            result.X = temp.x;
            result.Y = temp.y;
            result.Z = temp.z;
            return result;
        }
        public void Reset()
        {
            mat.Identity();
            FPhi = 0; FTheta = 0; FGamma = 0;
            FXrot = 0; FYrot = 0; FZrot = 0;
            FValid = true;
        }
        public void GetAngle(ref double wx, ref double wy, ref double wz)
        {
            wx = 0; wy = 0; wz = 0;
            switch (OnCalcAngle)
            {
                case 2:
                    GetAngle2(this, ref wx, ref wy, ref wz);
                    break;
            }
        }
        private static double Angle(Vec3 a, Vec3 b)
        {
            double temp;
            temp = Vec3.Dot(a, b);
            if (temp > 1)
            {
                temp = 1;
            }

            if (temp < -1)
            {
                temp = -1;
            }

            return Math.Acos(temp) * 180 / Math.PI;
        }
        public void GetAngle1(object Sender, ref double wx, ref double wy, ref double wz)
        {
            Vec3 FLocalX = new Vec3();
            Vec3 FLocalY = new Vec3();
            Vec3 FLocalZ = new Vec3();
            mat.GetLocals(ref FLocalX, ref FLocalY, ref FLocalZ);
            wx = Angle(FLocalX, xVec);
            wy = Angle(FLocalY, yVec);
            wz = Angle(FLocalZ, zVec);
        }
        private static void AssertFailed(string s)
        {
            Debug.WriteLine(s);
        }
        private static double CheckSinCos(double c)
        {
            if (!(c <= 1))
            {
                AssertFailed(string.Format("sincos > 1 ({0})", c)); // %6.5f
            }

            if (!(c >= -1))
            {
                AssertFailed(string.Format("sincos < -1 ({0})", c)); // %6.5f
            }

            if (c > 1)
            {
                c = 1;
            }

            if (c < -1)
            {
                c = -1;
            }

            return c;
        }
        public void GetAngle2(object? Sender, ref double wp, ref double wt, ref double wg)
        {
            if (Sender == null) return;
            if (!(Sender is TPolarKar)) return;

            double tempcos, tempsin;
            Vec3 ux, uy, uz, tempVec, tempY, tempZ;
            TMatrix4x4 tempmat;
            bool Theta90;
            tempmat = new TMatrix4x4();

            tempmat.CopyFrom(((TPolarKar)Sender).mat);
            ux = new Vec3();
            uy = new Vec3();
            uz = new Vec3();
            tempmat.GetLocals(ref ux, ref uy, ref uz);

            // Winkel Theta ermitteln im Bereich -90..90 Grad  
            tempsin = -ux.z;
            //tempcos = Dot(ux,zVec); //nicht verwendet
            wt = Math.Asin(CheckSinCos(tempsin));
            Theta90 = Math.Abs(tempsin * 180 / Math.PI) > 89.9; // Theta90 = abs(tempsin) > 0.99;

            // Winkel Gamma ermitteln im Bereich -180..180 Grad  
            if (Theta90)
            {
                // Winkel Gamma immer Null setzen, wenn lokale x-Achse senkrecht!
                //tempcos = 1;
                //tempsin = 0;
                wg = 0;
            }
            else
            {
                tempY = Vec3.Cross(zVec, ux);
                Vec3.Normalize(ref tempY);
                tempZ = Vec3.Cross(ux, tempY);
                Vec3.Normalize(ref tempZ);
                tempcos = Vec3.Dot(uz, tempZ);
                tempsin = -Vec3.Dot(uz, tempY);
                wg = Math.Asin(CheckSinCos(tempcos));
                if (tempsin < 0)
                {
                    wg = -wg;
                }
            }

            // Winkel Phi ermitteln im Bereich -180..180 Grad  
            if (Theta90)
            {
                tempVec = Vec3.Cross(uy, zVec);
                Vec3.Normalize(ref tempVec);
                tempcos = tempVec.x;
                tempsin = tempVec.y;
            }
            else
            {
                tempVec = ux;
                tempVec.z = 0;
                Vec3.Normalize(ref tempVec);
                tempcos = Vec3.Dot(xVec, tempVec);
                tempsin = Vec3.Dot(yVec, tempVec);
            }
            wp = Math.Acos(CheckSinCos(tempcos));
            if (tempsin < 0)
            {
                wp = -wp;
            }

            wg = wg * 180 / Math.PI;
            wt = wt * 180 / Math.PI;
            wp = wp * 180 / Math.PI;

            wg = Math.Round(wg * 10) / 10;
            wt = Math.Round(wt * 10) / 10;
            wp = Math.Round(wp * 10) / 10;
        }
        public double DeltaTheta
        {
            get
            {
                return Convert.ToInt32(FTheta * 180 / Math.PI);
            }
            set
            {
                FTheta = value * Math.PI / 180;
                FValid = false;
            }
        }
        public double DeltaGamma
        {
            get
            {
                return Convert.ToInt32(FGamma * 180 / Math.PI);
            }
            set
            {
                FGamma = value * Math.PI / 180;
                FValid = false;
            }
        }
        public double DeltaPhi
        {
            get
            {
                return Convert.ToInt32(FPhi * 180 / Math.PI);
            }
            set
            {
                FPhi = value * Math.PI / 180;
                FValid = false;
            }
        }
        public double Xrot
        {
            get
            {
                return Convert.ToInt32(FXrot * 180 / Math.PI);
            }
            set
            {
                FXrot = value * Math.PI / 180;
                FValid = false;
            }
        }
        public double Yrot
        {
            get
            {
                return Convert.ToInt32(FYrot * 180 / Math.PI);
            }
            set
            {
                FYrot = value * Math.PI / 180;
                FValid = false;
            }
        }
        public double Zrot
        {
            get
            {
                return Convert.ToInt32(FZrot * 180 / Math.PI);
            }
            set
            {
                FZrot = value * Math.PI / 180;
                FValid = false;
            }
        }
        public Matrix4x4 Matrix
        {
            get
            {
                if (FValid == false)
                {
                    GetMat();
                }

                return mat.mat;
            }
            set
            {
                Reset();
                mat.mat = value;
            }
        }
        public bool Mode
        {
            get
            {
                return FMode;
            }
            set
            {
                if (FMode != value)
                {
                    FMode = value;
                    FValid = false;
                    if (FMode) // Absolute Mode
                    {
                        GetAngle(ref FPhi, ref FTheta, ref FGamma);
                        FXrot = 0; FYrot = 0; FZrot = 0;
                    }
                    if (FMode == false) // Incremental Mode
                    {
                        FPhi = 0; FTheta = 0; FGamma = 0;
                        FXrot = 0; FYrot = 0; FZrot = 0;
                    }
                }
            }
        }
        public void SetRotAngle(TRotationAngle Index, double value)
        {
            double temp = value * Math.PI / 180;
            switch (Index)
            {
                case TRotationAngle.raPhi: FPhi = temp; break;
                case TRotationAngle.raTheta: FTheta = temp; break;
                case TRotationAngle.raGamma: FGamma = temp; break;
                case TRotationAngle.raXrot: FXrot = temp; break;
                case TRotationAngle.raYrot: FYrot = temp; break;
                case TRotationAngle.raZrot: FZrot = temp; break;
            }
        }
        public double GetRotAngle(TRotationAngle Index)
        {
            double temp = 0;
            switch (Index)
            {
                case TRotationAngle.raPhi: temp = FPhi; break;
                case TRotationAngle.raTheta: temp = FTheta; break;
                case TRotationAngle.raGamma: temp = FGamma; break;
                case TRotationAngle.raXrot: temp = FXrot; break;
                case TRotationAngle.raYrot: temp = FYrot; break;
                case TRotationAngle.raZrot: temp = FZrot; break;
            }
            return Convert.ToInt32(temp * 180 / Math.PI);
        }

    }

}
