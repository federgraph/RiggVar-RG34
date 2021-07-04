using HelixToolkit.SharpDX.Core;
using HelixToolkit.Wpf.SharpDX;
using RiggVar.Rgg;
using SharpDX;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Color = System.Windows.Media.Color;
using Colors = System.Windows.Media.Colors;
using Vector3D = System.Windows.Media.Media3D.Vector3D;
using Point3D = System.Windows.Media.Media3D.Point3D;
using Camera = HelixToolkit.Wpf.SharpDX.Camera;
using System;

namespace WpfAppRG34
{
    public class MainViewModel : INotifyPropertyChanged, IDisposable
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string property = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        private readonly RggModelBuilder rmb;
        private readonly TGetriebeGraphData ggd;

        public EffectsManager EffectsManager { get; }

        private readonly OrthographicCamera OrthoCamera;
        public Camera Camera => OrthoCamera;

        public Vector3D LightDirection1 { get; private set; } = new Vector3D(0, 0, -1);
        public Vector3D LightDirection2 { get; private set; } = new Vector3D(0, 1, 1);
        public Color AmbientLightColor { get; } = Colors.White;
        public Color DirectionalLightColor { get; } = Colors.Red;
        public MainViewModel()
        {
            OrthoCamera = new OrthographicCamera
            {
                Width = 20,
                NearPlaneDistance = -10,
                FarPlaneDistance = 10
            };

            EffectsManager = new DefaultEffectsManager();

            rmb = new RggModelBuilder();
            ggd = new TGetriebeGraphData();

            model = rmb.ToModel3D();
        }

        public void UpdateModel(TRggModelOutput value)
        {
            ggd.ModelOutput = value;
            UpdateGraph();
        }

        public void UpdateGraph()
        {
            ggd.ViewProps.Bogen = Bogen;
            ggd.ViewProps.Koppel = Koppel;
            ggd.ViewProps.FixPoint = TMain.RggController.GetriebeGraph.ViewProps.FixPoint;
            ggd.ViewProps.WanteGestrichelt = TMain.RggController.GetriebeGraph.ViewProps.WanteGestrichelt;

            Model = rmb.UpdateGraph(ggd);
            Vector3 v = rmb.FixedRotationPoint;
            FixPunkt = new Point3D(v.X, v.Y, v.Z);
        }

        private GroupModel3D model;
        public GroupModel3D Model
        {
            get => model;
            set
            {
                model = value;
                OnPropertyChanged("Model");
            }
        }

        internal bool FBogen = true;
        internal bool Bogen
        {
            get => FBogen;
            set
            {
                FBogen = value;
                UpdateGraph();
            }
        }
        internal bool FKoppel;
        internal bool Koppel
        {
            get => FKoppel;
            set
            {
                FKoppel = value;
                UpdateGraph();
            }
        }

        private Point3D fixPunkt;
        public Point3D FixPunkt
        {
            get => fixPunkt;
            set
            {
                fixPunkt = value;
                OnPropertyChanged("FixPunkt");
            }
        }

        #region IDisposable Support
        private bool disposedValue; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                if (EffectsManager != null)
                {
                    IDisposable effectManager = EffectsManager as IDisposable;
                    Disposer.RemoveAndDispose(ref effectManager);
                }
                disposedValue = true;
            }
        }

        ~MainViewModel()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(false);
        }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion

    }
}
