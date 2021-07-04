using HelixToolkit.Wpf.SharpDX;
using HelixToolkit.SharpDX.Core;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Media3D;

namespace RiggVar.Rgg
{
    public class RggViewport3DX : Viewport3DX
    {
        protected override void OnMouseWheel(MouseWheelEventArgs e)
        {
            bool control = Keyboard.IsKeyDown(Key.LeftCtrl);
            if (control)
            {
                base.OnMouseWheel(e);
                e.Handled = true;
                return;
            }

            e.Handled = false;
        }

        public RggViewport3DX() : base()
        {
            Color c = Color.FromArgb(0xFF, 0x33, 0x33, 0x33);
            Brush b = new SolidColorBrush(c);

            Background = b;
            BackgroundColor = c;
            IsMoveEnabled = true;
            IsZoomEnabled = true;

            ZoomDistanceLimitFar = 10.0;
            ZoomDistanceLimitNear = 1;

            ZoomSensitivity = 1.0;

            CameraRotationMode = CameraRotationMode.Turntable;
            CameraMode = CameraMode.Inspect;
            RotationSensitivity = 1.0;

            ModelUpDirection = new Vector3D(0, 0, 1);
        }
    }

}
