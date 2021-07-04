using System;
using System.Text;

namespace RiggVar.Rgg
{
    public struct Vec3
    {
        public double x;
        public double y;
        public double z;
        public Vec3(double ax, double ay, double az)
        {
            x = ax;
            y = ay;
            z = az;
        }
        public void Clear()
        {
            x = 0;
            y = 0;
            z = 0;
        }
        public static double Mag(Vec3 v)
        {
            return Math.Sqrt((v.x * v.x) + (v.y * v.y) + (v.z * v.z));
        }
        public static Vec3 Subtract(Vec3 v1, Vec3 v2)
        {
            Vec3 d;
            d.x = v1.x - v2.x;
            d.y = v1.y - v2.y;
            d.z = v1.z - v2.z;
            return d;
        }
        public static Vec3 Cross(Vec3 v1, Vec3 v2)
        {
            Vec3 c;
            c.x = (v1.y * v2.z) - (v2.y * v1.z);
            c.y = (v1.z * v2.x) - (v2.z * v1.x);
            c.z = (v1.x * v2.y) - (v2.x * v1.y);
            return c;
        }
        public static Vec3 Divide(Vec3 v, double num)
        {
            Vec3 d = new Vec3();
            if (num != 0)
            {
                d.x = v.x / num;
                d.y = v.y / num;
                d.z = v.z / num;
                return d;
            }
            d.Clear();
            return d;
        }
        public static void Normalize(ref Vec3 v)
        {
            double d;
            d = Math.Sqrt((v.x * v.x) + (v.y * v.y) + (v.z * v.z));
            if (d != 0)
            {
                v.x = v.x / d;
                v.y = v.y / d;
                v.z = v.z / d;
            }
        }
        public static double Dot(Vec3 v1, Vec3 v2)
        {
            return (v1.x * v2.x) + (v1.y * v2.y) + (v1.z * v2.z);
        }
    }

    public class Matrix4x4
    {
        public double[,] m = new double[4, 4];
        public void CopyFrom(Matrix4x4 source)
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    m[i, j] = source.m[i, j];
                }
            }
        }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    sb.Append(string.Format("{0,5:F2}", m[i, j]));
                    sb.Append(" ");
                }
                sb.Append("\r\n");
            }
            return sb.ToString();
        }

    }

    public class TMatrix4x4
    {
        const int o1 = 0;
        const int o2 = 1;
        const int o3 = 2;
        const int o4 = 3;

        public Matrix4x4 mat = new Matrix4x4();

        public TMatrix4x4()
        {
        }
        public void GetLocals(ref Vec3 ux, ref Vec3 uy, ref Vec3 uz)
        {
            ux.x = mat.m[o1,o1]; ux.y = mat.m[o2,o1]; ux.z = mat.m[o3,o1];
            uy.x = mat.m[o1,o2]; uy.y = mat.m[o2,o2]; uy.z = mat.m[o3,o2];
            uz.x = mat.m[o1,o3]; uz.y = mat.m[o2,o3]; uz.z = mat.m[o3,o3];
        }
        public void Identity()
        {
            SetIdentity(mat);
        }
        public void SetIdentity(Matrix4x4 m)
        {
            for (int r = 0; r < 4; r++)
            {
                for (int c = 0; c < 4; c++)
                {
                    if (r == c)
                    {
                        m.m[r,c] = 1;
                    }
                    else
                    {
                        m.m[r,c] = 0;
                    }
                }
            }
        }
        public void Multiply(Matrix4x4 m)
        {
            Matrix4x4 tmp = new Matrix4x4();
            for (int r = 0; r < 4; r++)
            {
                for (int c = 0; c < 4; c++)
                {
                    tmp.m[r,c] = 
                        (mat.m[r,o1]*m.m[o1,c]) +
                        (mat.m[r,o2]*m.m[o2,c]) +
                        (mat.m[r,o3]*m.m[o3,c]) + 
                        (mat.m[r,o4]*m.m[o4,c]);
                }
            }

            for (int r = 0; r < 4; r++)
            {
                for (int c = 0; c < 4; c++)
                {
                    mat.m[r,c] = tmp.m[r,c];
                }
            }
        }
        public void PreMultiply(Matrix4x4 m)
        {
            Matrix4x4 tmp = new Matrix4x4();
            for (int r = 0; r < 4; r++)
            {
                for (int c = 0; c < 4; c++)
                {
                    tmp.m[r,c] = 
                        (m.m[r,o1]*mat.m[o1,c]) + 
                        (m.m[r,o2]*mat.m[o2,c]) +
                        (m.m[r,o3]*mat.m[o3,c]) + 
                        (m.m[r,o4]*mat.m[o4,c]);
                }
            }

            for (int r = 0; r < 4; r++)
            {
                for (int c = 0; c < 4; c++)
                {
                    mat.m[r,c] = tmp.m[r,c];
                }
            }
        }
        public void Transpose()
        {
            double tmp;
            tmp = mat.m[o1,o2]; mat.m[o1,o2] = mat.m[o2,o1]; mat.m[o2,o1] = tmp; mat.m[o1,o4] = 0;
            tmp = mat.m[o2,o3]; mat.m[o2,o3] = mat.m[o3,o2]; mat.m[o3,o2] = tmp; mat.m[o2,o4] = 0;
            tmp = mat.m[o3,o1]; mat.m[o3,o1] = mat.m[o1,o3]; mat.m[o1,o3] = tmp; mat.m[o3,o4] = 0;
        }
        public void Translate(double tx, double ty, double tz)
        {
            Matrix4x4 m = new Matrix4x4();
            SetIdentity(m);
            m.m[o1,o4] = tx;
            m.m[o2,o4] = ty;
            m.m[o3,o4] = tz;
            PreMultiply(m);
        }
        public void TranslateDirect(double tx, double ty, double tz)
        {
            mat.m[o1,o4] = mat.m[o1,o4] + tx;
            mat.m[o2,o4] = mat.m[o2,o4] + ty;
            mat.m[o3,o4] = mat.m[o3,o4] + tz;
        }
        public void ScaleCenter(double sx, double sy, double sz, Vec3 center)
        {
            Matrix4x4 m = new Matrix4x4();
            SetIdentity(m);
            m.m[o1,o1] = sx;
            m.m[o1,o4] = (1 - sx) * center.x;
            m.m[o2,o2] = sy;
            m.m[o2,o4] = (1 - sy) * center.y;
            m.m[o3,o3] = sz;
            m.m[o3,o4] = (1 - sz) * center.z;
            PreMultiply(m);
        }
        public void Scale(double f)
        {
            mat.m[o1,o1] = mat.m[o1,o1] * f;
            mat.m[o1,o2] = mat.m[o1,o2] * f;
            mat.m[o1,o3] = mat.m[o1,o3] * f;
            mat.m[o1,o4] = mat.m[o1,o4] * f;
            mat.m[o2,o1] = mat.m[o2,o1] * f;
            mat.m[o2,o2] = mat.m[o2,o2] * f;
            mat.m[o2,o3] = mat.m[o2,o3] * f;
            mat.m[o2,o4] = mat.m[o2,o4] * f;
            mat.m[o3,o1] = mat.m[o3,o1] * f;
            mat.m[o3,o2] = mat.m[o3,o2] * f;
            mat.m[o3,o3] = mat.m[o3,o3] * f;
            mat.m[o3,o4] = mat.m[o3,o4] * f;
        }
        public void ScaleXYZ(double xf, double yf, double zf)
        {
            mat.m[o1,o1] = mat.m[o1,o1] * xf;
            mat.m[o1,o2] = mat.m[o1,o2] * xf;
            mat.m[o1,o3] = mat.m[o1,o3] * xf;
            mat.m[o1,o4] = mat.m[o1,o4] * xf;
            mat.m[o2,o1] = mat.m[o2,o1] * yf;
            mat.m[o2,o2] = mat.m[o2,o2] * yf;
            mat.m[o2,o3] = mat.m[o2,o3] * yf;
            mat.m[o2,o4] = mat.m[o2,o4] * yf;
            mat.m[o3,o1] = mat.m[o3,o1] * zf;
            mat.m[o3,o2] = mat.m[o3,o2] * zf;
            mat.m[o3,o3] = mat.m[o3,o3] * zf;
            mat.m[o3,o4] = mat.m[o3,o4] * zf;
        }
        public void Xrot(double theta)
        {
            double ct, st;
            double Nyx, Nyy, Nyz, Nyo, Nzx, Nzy, Nzz, Nzo;

            theta = theta * (Math.PI / 180);
            ct = Math.Cos(theta);
            st = Math.Sin(theta);

            Nyx = (mat.m[o2,o1] * ct) + (mat.m[o3,o1] * st);
            Nyy = (mat.m[o2,o2] * ct) + (mat.m[o3,o2] * st);
            Nyz = (mat.m[o2,o3] * ct) + (mat.m[o3,o3] * st);
            Nyo = (mat.m[o2,o4] * ct) + (mat.m[o3,o4] * st);

            Nzx = (mat.m[o3,o1] * ct) - (mat.m[o2,o1] * st);
            Nzy = (mat.m[o3,o2] * ct) - (mat.m[o2,o2] * st);
            Nzz = (mat.m[o3,o3] * ct) - (mat.m[o2,o3] * st);
            Nzo = (mat.m[o3,o4] * ct) - (mat.m[o2,o4] * st);

            mat.m[o2,o4] = Nyo;
            mat.m[o2,o1] = Nyx;
            mat.m[o2,o2] = Nyy;
            mat.m[o2,o3] = Nyz;
            mat.m[o3,o4] = Nzo;
            mat.m[o3,o1] = Nzx;
            mat.m[o3,o2] = Nzy;
            mat.m[o3,o3] = Nzz;
        }
        public void Yrot(double theta)
        {
            double ct, st;
            double Nxx, Nxy, Nxz, Nxo, Nzx, Nzy, Nzz, Nzo;

            theta = theta * (Math.PI / 180);
            ct = Math.Cos(theta);
            st = Math.Sin(theta);

            Nxx = (mat.m[o1,o1] * ct) + (mat.m[o3,o1] * st);
            Nxy = (mat.m[o1,o2] * ct) + (mat.m[o3,o2] * st);
            Nxz = (mat.m[o1,o3] * ct) + (mat.m[o3,o3] * st);
            Nxo = (mat.m[o1,o4] * ct) + (mat.m[o3,o4] * st);

            Nzx = (mat.m[o3,o1] * ct) - (mat.m[o1,o1] * st);
            Nzy = (mat.m[o3,o2] * ct) - (mat.m[o1,o2] * st);
            Nzz = (mat.m[o3,o3] * ct) - (mat.m[o1,o3] * st);
            Nzo = (mat.m[o3,o4] * ct) - (mat.m[o1,o4] * st);

            mat.m[o1,o4] = Nxo;
            mat.m[o1,o1] = Nxx;
            mat.m[o1,o2] = Nxy;
            mat.m[o1,o3] = Nxz;
            mat.m[o3,o4] = Nzo;
            mat.m[o3,o1] = Nzx;
            mat.m[o3,o2] = Nzy;
            mat.m[o3,o3] = Nzz;
        }
        public void Zrot(double theta)
        {
            double ct, st;
            double Nyx, Nyy, Nyz, Nyo, Nxx, Nxy, Nxz, Nxo;

            theta = theta * (Math.PI / 180);
            ct = Math.Cos(theta);
            st = Math.Sin(theta);

            Nyx = (mat.m[o2,o1] * ct) + (mat.m[o1,o1] * st);
            Nyy = (mat.m[o2,o2] * ct) + (mat.m[o1,o2] * st);
            Nyz = (mat.m[o2,o3] * ct) + (mat.m[o1,o3] * st);
            Nyo = (mat.m[o2,o4] * ct) + (mat.m[o1,o4] * st);

            Nxx = (mat.m[o1,o1] * ct) - (mat.m[o2,o1] * st);
            Nxy = (mat.m[o1,o2] * ct) - (mat.m[o2,o2] * st);
            Nxz = (mat.m[o1,o3] * ct) - (mat.m[o2,o3] * st);
            Nxo = (mat.m[o1,o4] * ct) - (mat.m[o2,o4] * st);

            mat.m[o2,o4] = Nyo;
            mat.m[o2,o1] = Nyx;
            mat.m[o2,o2] = Nyy;
            mat.m[o2,o3] = Nyz;
            mat.m[o1,o4] = Nxo;
            mat.m[o1,o1] = Nxx;
            mat.m[o1,o2] = Nxy;
            mat.m[o1,o3] = Nxz;
        }
        public void Rotate(Vec3 p1, Vec3 p2, double angle)
        {
            Matrix4x4 m = new Matrix4x4();

            Vec3 vec;
            double s, sinA2, vecLength, a, b, c;

            s = Math.Cos(angle/2.0);
            vec.x = p2.x - p1.x;
            vec.y = p2.y - p1.y;
            vec.z = p2.z - p1.z;
            vecLength = Math.Sqrt((vec.x*vec.x) + (vec.y*vec.y) + (vec.z*vec.z));
            sinA2 = Math.Sin(angle/2.0);
            a = sinA2 * vec.x / vecLength;
            b = sinA2 * vec.y / vecLength;
            c = sinA2 * vec.z / vecLength;
            Translate(-p1.x, -p1.y, -p1.z);
            SetIdentity(m);
            m.m[o1,o1] = 1.0 - (2*b*b) - (2*c*c);
            m.m[o1,o2] = (2*a*b) - (2*s*c);
            m.m[o1,o3] = (2*a*c) + (2*s*b);
            m.m[o2,o1] = (2*a*b) + (2*s*c);
            m.m[o2,o2] = 1.0 - (2*a*a) - (2*c*c);
            m.m[o2,o3] = (2*b*c) - (2*s*a);
            m.m[o3,o1] = (2*a*c) - (2*s*b);
            m.m[o3,o2] = (2*b*c) + (2*s*a);
            m.m[o3,o3] = 1.0 - (2*a*a) - (2*b*b);
            PreMultiply(m);
            Translate(p1.x, p1.y, p1.z);
        }
        public void TransformPoint(ref Vec3 p)
        {
            Vec3 tmp;
            tmp.x = (mat.m[o1,o1]*p.x) + (mat.m[o1,o2]*p.y) + (mat.m[o1,o3]*p.z) + mat.m[o1,o4];
            tmp.y = (mat.m[o2,o1]*p.x) + (mat.m[o2,o2]*p.y) + (mat.m[o2,o3]*p.z) + mat.m[o2,o4];
            tmp.z = (mat.m[o3,o1]*p.x) + (mat.m[o3,o2]*p.y) + (mat.m[o3,o3]*p.z) + mat.m[o3,o4];
            p = tmp;
        }
        private int RoundI(double a)
        {
            return Convert.ToInt32(Math.Round(a));
        }
        public void Transform(float [] v, int [] tv, int nvert)
        {
            int i;
            float x, y, z;

            for (int j = nvert-1; j >= 0; j--)  
            {
                i = j * 3;
                x = v[i];
                y = v[i + 1];
                z = v[i + 2];
                tv[i    ] = RoundI((mat.m[o1,o1]*x) + (mat.m[o1,o2]*y) + (mat.m[o1,o3]*z) + mat.m[o1,o4]);
                tv[i + 1] = RoundI((mat.m[o2,o1]*x) + (mat.m[o2,o2]*y) + (mat.m[o2,o3]*z) + mat.m[o2,o4]);
                tv[i + 2] = RoundI((mat.m[o3,o1]*x) + (mat.m[o3,o2]*y) + (mat.m[o3,o3]*z) + mat.m[o3,o4]);
            }
        }		
        public void TransformF(float [] v, float [] tv, int nvert)
        {
            int i;
            float x, y, z;

            for (int j = nvert-1; j >= 0; j--)  
            {
                i = j * 3;
                x = v[i];
                y = v[i + 1];
                z = v[i + 2];
                tv[i    ] = Convert.ToSingle((mat.m[o1,o1]*x) + (mat.m[o1,o2]*y) + (mat.m[o1,o3]*z) + mat.m[o1,o4]);
                tv[i + 1] = Convert.ToSingle((mat.m[o2,o1]*x) + (mat.m[o2,o2]*y) + (mat.m[o2,o3]*z) + mat.m[o2,o4]);
                tv[i + 2] = Convert.ToSingle((mat.m[o3,o1]*x) + (mat.m[o3,o2]*y) + (mat.m[o3,o3]*z) + mat.m[o3,o4]);
            }
        }
        public void CopyFrom(TMatrix4x4 m) => mat.CopyFrom(m.mat);
        public Matrix4x4 Matrix4x4
        {
            get => mat;
            set => mat = value;
        }

    }
    
}
