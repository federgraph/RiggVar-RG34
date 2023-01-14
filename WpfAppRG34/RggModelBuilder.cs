using HelixToolkit.SharpDX.Core;
using HelixToolkit.Wpf.SharpDX;
using RiggVar.Rgg;
using System.Collections.ObjectModel;
using Vector3D = SharpDX.Vector3;

namespace RiggVar.Rgg
{
    internal class RggModelBuilder
    {
        private readonly double DiameterBase = 0.01;

        private Vector3D N0 = new Vector3D(0.000f, 0.000f, 0.000f);
        private Vector3D A0 = new Vector3D(2.560f, 0.765f, 0.430f);
        private Vector3D B0 = new Vector3D(2.560f, -0.765f, 0.430f);
        private Vector3D C0 = new Vector3D(4.140f, 0.000f, 0.340f);
        private Vector3D D0 = new Vector3D(2.870f, 0.000f, -0.100f);
        //private Vector3D E0 = new Vector3D(2.970f, 0.000f, 0.450f);
        private Vector3D F0 = new Vector3D(-0.030f, 0.000f, 0.300f);

        private Vector3D A = new Vector3D(2.47815f, 0.425f, 2.50068f);
        private Vector3D B = new Vector3D(2.47815f, -0.425f, 2.50068f);
        private Vector3D C = new Vector3D(2.41747f, 0.000f, 4.47453f);
        private Vector3D D = new Vector3D(2.69800f, 0.000f, 2.49431f);
        //private Vector3D E = new Vector3D(2.87000, 0.000, 0.45000f);
        private Vector3D F = new Vector3D(2.18047f, 0.000f, 5.97088f);
        private Vector3D M = new Vector3D(0.100f, 0.000f, 0.500f);

        private Collection<Vector3D> KoppelCollection = new Collection<Vector3D>();
        private Collection<Vector3D> MastCollection = new Collection<Vector3D>();

        private bool WanteGestrichelt;
        private bool Koppel;
        private bool Bogen;
        private TRiggPoint FixPoint;

        private TRealPoint FixPunkt = TRealPoint.Zero;
        private readonly double ScalingFactor = 0.001;

        public Vector3D FixedRotationPoint;

        private Vector3D RealPoint2Point3D(TRealPoint v)
        {
            TRealPoint p = (v - FixPunkt) * ScalingFactor;
            return new Vector3D((float)p.X, (float)p.Y, (float)p.Z);
        }

        public GroupModel3D UpdateGraph(TGetriebeGraphData value)
        {
            WanteGestrichelt = value.ViewProps.WanteGestrichelt;
            Bogen = value.ViewProps.Bogen;
            Koppel = value.ViewProps.Koppel;
            FixPoint = value.ViewProps.FixPoint;

            TRiggPoints K = value.ModelOutput.Koordinaten;
            FixPunkt = K[FixPoint];

            FixedRotationPoint = RealPoint2Point3D(FixPunkt);

            N0 = RealPoint2Point3D(K.N0);
            A0 = RealPoint2Point3D(K.A0);
            B0 = RealPoint2Point3D(K.B0);
            C0 = RealPoint2Point3D(K.C0);
            D0 = RealPoint2Point3D(K.D0);
            //E0 = RealPoint2Point3D(K.E0);
            F0 = RealPoint2Point3D(K.F0);

            A = RealPoint2Point3D(K.A);
            B = RealPoint2Point3D(K.B);
            C = RealPoint2Point3D(K.C);
            D = RealPoint2Point3D(K.D);
            //E = RealPoint2Point3D(K.E);
            F = RealPoint2Point3D(K.F);
            M = RealPoint2Point3D(K.M);

            int n;

            if (Koppel)
            {
                n = value.ModelOutput.KoppelKurve.Data.Length;
                KoppelCollection = new Collection<Vector3D>();
                for (int i = 0; i < n; i++)
                {
                    KoppelCollection.Add(RealPoint2Point3D(value.ModelOutput.KoppelKurve.Data[i]));
                }
            }

            if (Bogen)
            {
                n = value.ModelOutput.MastKurve.Data.Length;
                MastCollection = new Collection<Vector3D>();
                for (int i = 0; i < n; i++)
                {
                    MastCollection.Add(RealPoint2Point3D(value.ModelOutput.MastKurve.Data[i]));
                }
            }

            return ToModel3D();
        }

        private class Panel
        {
            public Vector3D[] Points { get; set; } = new Vector3D[0];
            public int TriangleIndex { get; set; }
        }

        private readonly Material MaterialRed = PhongMaterials.Red;
        private readonly Material MaterialGreen = PhongMaterials.Green;
        private readonly Material MaterialBlue = PhongMaterials.Blue;
        private readonly Material MaterialYellow = PhongMaterials.Yellow;
        private readonly Material MaterialGray = PhongMaterials.Gray;
        private readonly Material MaterialOrange = PhongMaterials.Orange;
        private readonly Material MaterialBlack = PhongMaterials.Black;
        private readonly Material MaterialViolet = PhongMaterials.Violet;

        public GroupModel3D ToModel3D()
        {
            GroupModel3D mg = new GroupModel3D();
            AddFaces(mg);
            AddSpheres(mg);
            AddCylinders(mg);
            AddTubes(mg);
            return mg;
        }

        private void AddFaces(GroupModel3D mg)
        {
            MeshBuilder mb = new MeshBuilder();
            mb.AddTriangle(A, B, D);
            AddPart(mg, mb.ToMesh(), MaterialGreen);
        }

        private void AddSpheres(GroupModel3D mg)
        {
            MeshBuilder mb;
            double radius = 5 * DiameterBase;

            mb = new MeshBuilder();
            mb.AddSphere(A0, radius);
            mb.AddSphere(A, radius);
            AddPart(mg, mb.ToMesh(), MaterialRed);

            mb = new MeshBuilder();
            mb.AddSphere(B0, radius);
            mb.AddSphere(B, radius);
            AddPart(mg, mb.ToMesh(), MaterialGreen);

            mb = new MeshBuilder();
            mb.AddSphere(C0, radius);
            mb.AddSphere(C, radius);
            AddPart(mg, mb.ToMesh(), MaterialYellow);

            mb = new MeshBuilder();
            mb.AddSphere(D0, radius);
            mb.AddSphere(D, radius);
            AddPart(mg, mb.ToMesh(), MaterialBlue);

            mb = new MeshBuilder();
            mb.AddSphere(N0, radius);
            mb.AddSphere(F0, radius);
            mb.AddSphere(M, radius);
            mb.AddSphere(F, radius);
            AddPart(mg, mb.ToMesh(), MaterialOrange);
        }

        private void AddCylinders(GroupModel3D mg)
        {
            MeshBuilder mb;
            double diameter;
            int thetaDiv = 10;

            diameter = 3 * DiameterBase;
            mb = new MeshBuilder();
            mb.AddCylinder(A0, A, diameter, thetaDiv);
            mb.AddCylinder(B0, B, diameter, thetaDiv);
            mb.AddCylinder(C0, C, diameter, thetaDiv);
            AddPart(mg, mb.ToMesh(), MaterialGray);

            diameter = 2 * DiameterBase;
            mb = new MeshBuilder();
            mb.AddCylinder(A, C, diameter, thetaDiv);
            mb.AddCylinder(B, C, diameter, thetaDiv);
            AddPart(mg, mb.ToMesh(), MaterialGray);

            if (!Bogen)
            {
                diameter = 6 * DiameterBase;
                mb = new MeshBuilder();
                mb.AddCylinder(D0, D, diameter, thetaDiv);
                mb.AddCylinder(D, C, diameter, thetaDiv);
                AddPart(mg, mb.ToMesh(), MaterialBlue);
            }

            diameter = 5 * DiameterBase;
            mb = new MeshBuilder();
            mb.AddCylinder(C, F, diameter, thetaDiv);
            AddPart(mg, mb.ToMesh(), MaterialViolet);

            diameter = 4 * DiameterBase;
            mb = new MeshBuilder();
            mb.AddCylinder(A, D, diameter, thetaDiv);
            mb.AddCylinder(B, D, diameter, thetaDiv);
            AddPart(mg, mb.ToMesh(), MaterialGreen);

            diameter = 4 * DiameterBase;
            mb = new MeshBuilder();
            mb.AddCylinder(N0, F0, diameter, thetaDiv);
            mb.AddCylinder(A0, D0, diameter, thetaDiv);
            mb.AddCylinder(B0, D0, diameter, thetaDiv);
            mb.AddCylinder(C0, D0, diameter, thetaDiv);
            mb.AddCylinder(A0, C0, diameter, thetaDiv);
            mb.AddCylinder(B0, C0, diameter, thetaDiv);
            mb.AddCylinder(A0, B0, diameter, thetaDiv);
            AddPart(mg, mb.ToMesh(), MaterialBlack);

            diameter = 3 * DiameterBase;
            mb = new MeshBuilder();
            mb.AddCylinder(F0, F, diameter, thetaDiv);
            AddPart(mg, mb.ToMesh(), MaterialOrange);
        }

        private void AddTubes(GroupModel3D mg)
        {
            MeshBuilder mb;
            double diameter;
            int thetaDiv = 10;

            bool isTubeClosed = false;
            bool wantFrontCap = true;
            bool wantBackCap = true;

            if (Koppel && KoppelCollection.Count > 3)
            {
                diameter = 1 * DiameterBase;
                mb = new MeshBuilder();
                mb.AddTube(KoppelCollection, diameter, thetaDiv, isTubeClosed, wantFrontCap, wantBackCap);
                AddPart(mg, mb.ToMesh(), MaterialYellow);
            }

            if (Bogen && MastCollection.Count > 3)
            {
                Material m = WanteGestrichelt ? MaterialGray : MaterialViolet;
                diameter = 6 * DiameterBase;
                mb = new MeshBuilder();
                mb.AddTube(MastCollection, diameter, thetaDiv, isTubeClosed, wantFrontCap, wantBackCap);
                AddPart(mg, mb.ToMesh(), m);
            }

        }

        private void AddPart(GroupModel3D gm, MeshGeometry3D mg, Material m)
        {
            MeshGeometryModel3D mgm = new MeshGeometryModel3D()
            {
                Geometry = mg,
                Material = m
            };
            gm.Children.Add(mgm);
        }

    }
}
