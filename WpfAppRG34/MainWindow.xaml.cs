using HelixToolkit.SharpDX.Core;
using HelixToolkit.Wpf.SharpDX;
using RiggVar.FB;
using RiggVar.Rgg;
using System;
using System.Diagnostics;
using System.Globalization;
using System.Text;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using OrthographicCamera = HelixToolkit.Wpf.SharpDX.OrthographicCamera;

namespace WpfAppRG34
{
    public partial class MainWindow : Window, IRggDraw
    {
        private readonly MainViewModel mvm;
        private RggViewport3DX HelixViewport;

        public MainWindow()
        {
            InitializeComponent();
            mvm = new MainViewModel();
            DataContext = mvm;

            Controller = TMain.RggController;
            Init();
        }
        private void Init()
        {
            _ = new InputProcessorTouch(this, BtnFrame.TouchBarTop, 1);
            _ = new InputProcessorTouch(this, BtnFrame.TouchBarBottom, 2);
            _ = new InputProcessorTouch(this, BtnFrame.TouchBarLeft, 3, true);
            _ = new InputProcessorTouch(this, BtnFrame.TouchBarRight, 4, true);

            Loaded += new RoutedEventHandler(Window_Loaded);
            Loaded += new RoutedEventHandler(UserControl_Loaded);
            Unloaded += new RoutedEventHandler(UserControl_Unloaded);

            BtnFrame.RggDraw = this;

            MouseWheel += MainWindow_MouseWheel;
            CreateHelixViewport();
            InitHelixViewport();
            InitHelixViewportGestures();
        }

        private void CreateHelixViewport()
        {
            HelixViewport = new RggViewport3DX();
            HelixHost.Children.Add(HelixViewport);

            HelixViewport.Camera = mvm.Camera;
            HelixViewport.EffectsManager = mvm.EffectsManager;

            HelixViewport.FixedRotationPointEnabled = true;
            Binding binding1 = new Binding
            {
                Source = mvm,
                Path = new PropertyPath("FixPunkt"),
                Mode = BindingMode.OneWay,
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
            };
            _ = BindingOperations.SetBinding(HelixViewport, Viewport3DX.FixedRotationPointProperty, binding1);

            Element3DPresenter ep = new Element3DPresenter();
            HelixViewport.Items.Add(ep);
            Binding binding2 = new Binding
            {
                Source = mvm,
                Path = new PropertyPath("Model"),
                Mode = BindingMode.OneWay,
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
            };
            _ = BindingOperations.SetBinding(ep, Element3DPresenter.ContentProperty, binding2);

            DirectionalLight3D dl1 = new DirectionalLight3D
            {
                Direction = mvm.LightDirection1,
                Color = Colors.White
            };
            HelixViewport.Items.Add(dl1);

            DirectionalLight3D dl2 = new DirectionalLight3D
            {
                Direction = mvm.LightDirection2,
                Color = Colors.White
            };
            HelixViewport.Items.Add(dl2);
        }

        private void InitHelixViewport()
        {
            HelixViewport.EnableSwapChainRendering = false;

            HelixViewport.ZoomExtentsWhenLoaded = true;
            HelixViewport.ZoomSensitivity = 1.0;
            HelixViewport.ZoomDistanceLimitFar = 5.0;
            HelixViewport.ZoomDistanceLimitNear = 0.5;

            HelixViewport.CameraRotationMode = CameraRotationMode.Turntable;
            HelixViewport.CameraMode = CameraMode.Inspect;
            HelixViewport.RotationSensitivity = 1.0;

            HelixViewport.ShowCoordinateSystem = true;
            HelixViewport.ShowViewCube = true;

            HelixViewport.ShowTriangleCountInfo = false;
            HelixViewport.ShowFrameRate = false;
            HelixViewport.ShowCameraTarget = false;
            HelixViewport.ShowCameraInfo = false;

            HelixViewport.InfoBackground = Brushes.Transparent;
            HelixViewport.InfoForeground = Brushes.White;

            HelixViewport.RotateAroundMouseDownPoint = false;
            HelixViewport.ZoomAroundMouseDownPoint = false;

            HelixViewport.IsInertiaEnabled = true;
            HelixViewport.IsPanEnabled = true;
            HelixViewport.IsMoveEnabled = true;
            HelixViewport.IsRotationEnabled = true;
            HelixViewport.IsZoomEnabled = true;
            HelixViewport.IsTouchRotateEnabled = true;
            HelixViewport.IsPinchZoomEnabled = true;
            HelixViewport.IsThreeFingerPanningEnabled = true;

            HelixViewport.ModelUpDirection = new Vector3D(0, 0, 1);
        }

        private void InitHelixViewportGestures()
        {
            HelixViewport.UseDefaultGestures = false;
            AddKeyBindings();
            AddMouseBindings();
        }

        internal void Window_Loaded(object sender, RoutedEventArgs e)
        {
            MemoText.Text = "";
            TrimmText.Text = "";
            LoadDefaultData(false);
        }

        public void HandleBtnClick(int fa)
        {
            ClickText.Text = string.Format(CultureInfo.InvariantCulture, "Click = {0}", fa);

            switch (fa)
            {
                case RggActions.faRggBogen:
                    mvm.Bogen = !mvm.Bogen;
                    break;
                case RggActions.faRggKoppel:
                    mvm.Koppel = !mvm.Koppel;
                    break;

                // Zoom
                case RggActions.faRggZoomIn:
                    //TMain.RggController.ZoomIn();
                    //HelixViewport.CameraController.Zoom(1);
                    break;
                case RggActions.faRggZoomOut:
                    //TMain.RggController.ZoomOut();
                    //HelixViewport.CameraController.Zoom(-1);
                    break;
                case RggActions.faReset:
                    ResetView();
                    break;

                // Slot
                case RggActions.fa420:
                    LoadDefaultData(false);
                    break;
                case RggActions.faLogo:
                    LoadDefaultData(true);
                    break;

                // Fixpoint
                case RggActions.faFixpointD0:
                    SetFixPoint(TRiggPoint.ooD0);
                    break;
                case RggActions.faFixpointD:
                    SetFixPoint(TRiggPoint.ooD);
                    break;

                // Viewpoint
                case RggActions.faViewpoint3:
                    ResetViewpoint(TViewPoint.vp3D);
                    break;
                case RggActions.faViewpointA:
                    ResetViewpoint(TViewPoint.vpAchtern);
                    break;
                case RggActions.faViewpointS:
                    ResetViewpoint(TViewPoint.vpSeite);
                    break;
                case RggActions.faViewpointT:
                    ResetViewpoint(TViewPoint.vpTop);
                    break;

                // Param
                case RggActions.faVorstag:
                    SetAntrieb(RotaVarController.fpVorstag);
                    break;
                case RggActions.faWante:
                    SetAntrieb(RotaVarController.fpWante);
                    break;
                case RggActions.faSalingH:
                    SetAntrieb(RotaVarController.fpSalingH);
                    break;
                case RggActions.faSalingA:
                    SetAntrieb(RotaVarController.fpSalingA);
                    break;
                case RggActions.faMastfallF0C:
                    SetAntrieb(RotaVarController.fpMastfallF0C);
                    break;
                case RggActions.faMastfallF0F:
                    SetAntrieb(RotaVarController.fpMastfallF0F);
                    break;
                case RggActions.faBiegung:
                    SetAntrieb(RotaVarController.fpBiegung);
                    break;

                default:
                    break;

            }
            UpdateTrimmText();
        }

        public void HandleUpdateHint(int fa)
        {
            HintText.Text = fa == RggActions.faNoop ? "" : RggActions.GetFederActionLong(fa);
        }

        private int touchValueT;
        private int touchValueB;
        private int touchValueL;
        private int touchValueR;
        public void HandleTouchInput(int touchID, int value)
        {
            char c;
            string fs = "Touch {0} = {1}";
            switch (touchID)
            {
                case 1:
                    touchValueT += value;
                    c = 'T';
                    TouchText.Text = string.Format(CultureInfo.InvariantCulture, fs, c, touchValueT);
                    if (value > 0)
                    {
                        RotateZ(1);
                    }
                    else if (value < 0)
                    {
                        RotateZ(-1);
                    }
                    break;
                case 2:
                    touchValueB += value;
                    c = 'B';
                    TouchText.Text = string.Format(CultureInfo.InvariantCulture, fs, c, touchValueB);
                    if (value > 0)
                    {
                        Zoom(1);
                    }
                    else if (value < 0)
                    {
                        Zoom(-1);
                    }
                    break;
                case 3:
                    touchValueL -= value * Controller.BigStep;
                    c = 'L';
                    TouchText.Text = string.Format(CultureInfo.InvariantCulture, fs, c, touchValueL);
                    DoWheel(value * Controller.BigStep);
                    break;
                case 4:
                    touchValueR -= value * Controller.SmallStep;
                    c = 'R';
                    TouchText.Text = string.Format(CultureInfo.InvariantCulture, fs, c, touchValueR);
                    DoWheel(value * Controller.SmallStep);
                    break;

                default:
                    break;
            }
            UpdateTrimmText();
        }

        private void MainWindow_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            int delta = e.Delta > 0 ? 1 : -1;

            bool shift = Keyboard.IsKeyDown(Key.LeftShift);
            if (shift)
            {
                delta *= Controller.BigStep;
            }

            HandleTouchInput(4, delta);

            e.Handled = true;
        }

        private void UpdateTrimmText()
        {
            TrimmText.Text = Controller.TrimmText;
        }

        private void ResetView()
        {
            if (HelixViewport.Camera is OrthographicCamera ocamera)
            {
                ocamera.Reset();
                ocamera.Width = 8;
                ocamera.NearPlaneDistance = -10;
                ocamera.FarPlaneDistance = 10;
            }
            ResetViewpoint(TViewPoint.vp3D);
        }

        private void Zoom(double delta)
        {
            if (HelixViewport.Camera is OrthographicCamera ocamera)
            {
                double d = delta > 0 ? -0.05 : 0.05;
                ocamera.Width *= Math.Pow(2.5, d);
            }
        }

        private void ResetViewpoint(TViewPoint vp)
        {
            if (HelixViewport.Camera is OrthographicCamera oc)
            {
                oc.Position = new Point3D(0, 0, 0);

                switch (vp)
                {
                    case TViewPoint.vpAchtern:
                        oc.LookDirection = new Vector3D(1, 0, 0);
                        oc.UpDirection = new Vector3D(0, 0, 1);
                        break;

                    case TViewPoint.vpSeite:
                        oc.LookDirection = new Vector3D(0, 1, 0);
                        oc.UpDirection = new Vector3D(0, 0, 1);
                        break;

                    case TViewPoint.vpTop:
                        oc.LookDirection = new Vector3D(0, 0, -1);
                        oc.UpDirection = new Vector3D(0, 1, 0);
                        break;

                    case TViewPoint.vp3D:
                        oc.LookDirection = SpecialRot(new Vector3D(0, 0, -1));
                        oc.UpDirection = SpecialRot(new Vector3D(0, 1, 0));
                        break;
                    default:
                        break;
                }
            }
        }

        private void RotateZ(double delta)
        {
            if (HelixViewport.Camera is OrthographicCamera oc)
            {
                double d = delta > 0 ? -1 : 1;
                Quaternion rot = new(oc.LookDirection, d * 2);
                Matrix3D m = Matrix3D.Identity;
                m.Rotate(rot);
                oc.UpDirection = m.Transform(oc.UpDirection);
            }
        }

        private Vector3D SpecialRot(Vector3D v)
        {
            Vector3D axis = new(1, 0, 0);
            double angleInDegrees = 5.0;
            Quaternion rot = new(axis, angleInDegrees);
            Matrix3D m = Matrix3D.Identity;
            m.Rotate(rot);
            return m.Transform(v);
        }

        private readonly TRggModelOutput modelOutput = new();

        private bool allowDrawing;

        private readonly RotaVarController Controller;

        internal void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateStatusColumn(1);
            UpdateStatusColumn(2);

            Controller.OnChange = this;

            AntriebChanged();
        }

        internal void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {
            Controller.OnChange = null;
        }

        internal void LoadDefaultData(bool useLogoData)
        {
            allowDrawing = false;
            Controller.WantLogoData = useLogoData;

            int t = 0;
            if (t == 0)
            {
                Controller.DefaultRigg(useLogoData);
            }
            else
            {
                string ed;
                RggDataSerializer j = new RggDataSerializer();
                ed = Controller.WantLogoData ? j.LogoData : j.TestData;
                Controller.RemoteRigg("json", ed);
            }

            AntriebChanged();
        }

        public void DoWheel(int delta)
        {
            TFederParam dcv = Controller.FederParam;
            double value;
            value = Controller.TempIst;
            value += delta;
            Controller.SetParamProp(dcv, value);
        }

        public void SetFixPoint(TRiggPoint value)
        {
            Controller.GetriebeGraph.ViewProps.FixPoint = value;
            Draw();
        }

        private void AntriebChanged()
        {
            SetAntrieb(Rigg.Vorstag);
        }
        public void SetAntrieb(int value)
        {
            allowDrawing = false;
            try
            {
                Controller.Parameter = value; // --> Draw()
            }
            finally
            {
                allowDrawing = true;
            }
            Draw();
        }

        public class ComboItem
        {
            public string N { get; set; }
            public int V { get; set; }
            public ComboItem(string name, int value)
            {
                N = name;
                V = value;
            }
            public override string ToString()
            {
                return N;
            }
        }

        //public void HandleKey(char c)
        //{ 
        //    switch (c)
        //    {
        //        case 'c': Controller.Parameter = RotaVarController.fpController; break;
        //        case 'e': Controller.Parameter = RotaVarController.fpWinkel; break;
        //        case 'v': Controller.Parameter = RotaVarController.fpVorstag; break;
        //        case 's': Controller.Parameter = RotaVarController.fpWante; break;
        //        case 'o': Controller.Parameter = RotaVarController.fpWoben; break;
        //        case 'h': Controller.Parameter = RotaVarController.fpSalingH; break;
        //        case 'a': Controller.Parameter = RotaVarController.fpSalingA; break;
        //        case 'l': Controller.Parameter = RotaVarController.fpSalingL; break;
        //        case 'w': Controller.Parameter = RotaVarController.fpSalingW; break;
        //        case 'm': Controller.Parameter = RotaVarController.fpMastfallF0C; break;
        //        case 'f': Controller.Parameter = RotaVarController.fpMastfallF0F; break;
        //        case 'b': Controller.Parameter = RotaVarController.fpBiegung; break;
        //        case 'd': Controller.Parameter = RotaVarController.fpD0x; break;
        //    }
        //}

        public static string BeautifyJson(string js)
        {
            /*
            {"Faktor":1,"Name":"420","OffsetX":0,"OffsetZ":0,
            "RK":{
            "A0":{"x":2560,"y":765,"z":430},
            "C0":{"x":4140,"y":0,"z":340},
            "D0":{"x":2870,"y":0,"z":-100},
            "E0":{"x":2970,"y":0,"z":450},
            "F0":{"x":-30,"y":0,"z":300}},
            "RL":{"CA":50,"ML":6115,"MO":2000,"MU":2600,"MV":5000},
            "SB":{
            "CP":{"Max":200,"Min":50,"Pos":100},
            "SA":{"Max":1000,"Min":780,"Pos":850},
            "SH":{"Max":300,"Min":140,"Pos":220},
            "SL":{"Max":600,"Min":450,"Pos":640},
            "VO":{"Max":4600,"Min":4400,"Pos":4500},
            "WI":{"Max":1050,"Min":850,"Pos":950},
            "WL":{"Max":4200,"Min":4050,"Pos":4120},
            "WO":{"Max":2070,"Min":2000,"Pos":2020}}}
            */

            string[] ta = {
                            "RK",
                            "RL",
                            "SB",
                            "A0",
                            "C0",
                            "D0",
                            "E0",
                            "F0",
                            "RL",
                            "CP",
                            "SA",
                            "SH",
                            "SL",
                            "VO",
                            "WI",
                            "WL",
                            "WO"
                           };

            string[] sa = js.Split(',');

            bool b;
            string s;
            int last = sa.Length - 1;
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < sa.Length; i++)
            {
                s = sa[i];
                b = false;
                foreach (string t in ta)
                {
                    if (s.StartsWith("\"" + t, StringComparison.InvariantCulture))
                    {
                        b = true;
                        break;
                    }
                }
                if (b)
                {
                    if (s.Contains("RK") || s.Contains("SB"))
                    {
                        s = s.Insert(6, Environment.NewLine);
                    }
                    _ = sb.Append(Environment.NewLine);
                    _ = sb.Append(s);
                    _ = sb.Append(',');
                }
                else
                {
                    _ = sb.Append(s);
                    if (i < last)
                    {
                        _ = sb.Append(',');
                    }
                }
            }

            return sb.ToString();
        }

        public static string GetData(bool WantJson)
        {
            TRggDocument rggdoc = new TRggDocument();
            TMain.RggController.rigg.GetDocument(rggdoc);

            RggData rggdat = new RggData();
            rggdoc.CopyToRggData(rggdat);

            rggdat.Name = "Current";

            RggDataSerializer ds = new RggDataSerializer()
            {
                WantJson = WantJson
            };
            return ds.Write(rggdat);
        }

        private void UpdateMemoText()
        {
            string s = GetData(true);
            s = BeautifyJson(s);
            MemoText.Text = s;
            TrimmText.Text = Controller.TrimmText;
        }

        private void UpdateStatusColumn(int c)
        {
            StringBuilder sb = new StringBuilder();
            switch (c)
            {
                case 1:
                    Controller.GetStatusReportColumn1(sb);
                    if (StatusColumn1 != null)
                    {
                        StatusColumn1.Text = sb.ToString();
                    }
                    break;

                case 2:
                    Controller.GetStatusReportColumn2(sb);
                    if (StatusColumn2 != null)
                    {
                        StatusColumn2.Text = sb.ToString();
                    }
                    break;

                case 3:
                    Controller.GetStatusReportColumn3(sb);
                    if (StatusColumn3 != null)
                    {
                        StatusColumn3.Text = sb.ToString();
                    }
                    break;

                default:
                    break;
            }
        }

        public void Draw()
        {
            if (mvm == null)
            {
                return;
            }

            if (!allowDrawing)
            {
                return;
            }

            UpdateGraph();
            Controller.rigg.UpdateFactArrayFromRigg(Controller.FederParam);
            UpdateStatusColumn(3);
            UpdateMemoText();
        }

        internal bool SwapEvent(string et, string ed)
        {
            allowDrawing = false;
            try
            {
                Controller.RemoteRigg(et, ed);
                AntriebChanged();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return false;
            }
            return true;
        }

        private void UpdateGraph()
        {
            if (mvm != null)
            {
                modelOutput.Koordinaten = Controller.rigg.rP;
                modelOutput.MastKurve.Data = Controller.rigg.MastKurve;
                modelOutput.KoppelKurve.Data = Controller.rigg.Koppelkurve();

                mvm.UpdateModel(modelOutput);
            }
        }

        private void AddKeyBindings()
        {
            KeyBinding k;
            k = new KeyBinding
            {
                Key = Key.B,
                Command = ViewportCommands.BackView
            };
            _ = HelixViewport.InputBindings.Add(k);

            k = new KeyBinding
            {
                Key = Key.F,
                Command = ViewportCommands.FrontView
            };
            _ = HelixViewport.InputBindings.Add(k);


            k = new KeyBinding
            {
                Key = Key.U,
                Command = ViewportCommands.TopView
            };
            _ = HelixViewport.InputBindings.Add(k);

            k = new KeyBinding
            {
                Key = Key.U,
                Command = ViewportCommands.TopView
            };
            _ = HelixViewport.InputBindings.Add(k);

            k = new KeyBinding
            {
                Key = Key.L,
                Command = ViewportCommands.LeftView
            };
            _ = HelixViewport.InputBindings.Add(k);

            k = new KeyBinding
            {
                Key = Key.R,
                Command = ViewportCommands.RightView
            };
            _ = HelixViewport.InputBindings.Add(k);

            k = new KeyBinding
            {
                Key = Key.E,
                Modifiers = ModifierKeys.Control,
                Command = ViewportCommands.ZoomExtents
            };
            _ = HelixViewport.InputBindings.Add(k);
        }

        private void AddMouseBindings()
        {
            MouseBinding mb;
            mb = new MouseBinding
            {
                Gesture = new MouseGesture(MouseAction.LeftClick),
                Command = ViewportCommands.Rotate
            };
            _ = HelixViewport.InputBindings.Add(mb);

            mb = new MouseBinding
            {
                Gesture = new MouseGesture(MouseAction.RightClick),
                Command = ViewportCommands.Pan
            };
            _ = HelixViewport.InputBindings.Add(mb);

            mb = new MouseBinding
            {
                Gesture = new MouseGesture(MouseAction.LeftClick, ModifierKeys.Control),
                Command = ViewportCommands.Pan
            };
            _ = HelixViewport.InputBindings.Add(mb);

        }

    }
}
