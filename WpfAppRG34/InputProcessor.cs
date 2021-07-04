using System.Windows;
using System.Windows.Input;

namespace RiggVar.Rgg
{
    internal class InputProcessor
    {
        protected bool down;
        protected int touchBarID;
        protected bool isHorizontal = true;

        protected IRggDraw controller;
        protected UIElement uie;

        public InputProcessor(IRggDraw c, UIElement e)
        {
            if (c == null)
            {
                return;
            }
            if (e == null)
            {
                return;
            }

            controller = c;
            uie = e;

            uie.MouseLeftButtonDown += new MouseButtonEventHandler(PaintBox_MouseLeftButtonDown);
            uie.MouseRightButtonDown += new MouseButtonEventHandler(PaintBox_MouseRightButtonDown);
            uie.MouseMove += new MouseEventHandler(PaintBox_MouseMove);
            uie.MouseLeftButtonUp += new MouseButtonEventHandler(PaintBox_MouseLeftButtonUp);
        }

        private void PaintBox_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _ = uie.CaptureMouse();
            Point p = e.GetPosition(uie);
            if (isHorizontal)
            {
                OnPointerDown((int)p.X);
            }
            else
            {
                OnPointerDown((int)p.Y);
            }
            e.Handled = true;
            down = true;
        }

        private void PaintBox_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            _ = uie.CaptureMouse();
            Point p = e.GetPosition(uie);
            if (isHorizontal)
            {
                OnPointerDown((int)p.X);
            }
            else
            {
                OnPointerDown((int)p.Y);
            }
            e.Handled = true;
            down = true;
        }

        private void PaintBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (down)
            {
                Point p = e.GetPosition(uie);
                _ = isHorizontal ? OnPointerMove(p.X) : OnPointerMove(p.Y);
                e.Handled = true;
            }
        }

        private void PaintBox_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            down = false;
            OnPointerUp();
            e.Handled = true;
            uie.ReleaseMouseCapture();
        }

        protected virtual void OnPointerDown(double v)
        {
        }

        protected virtual bool OnPointerMove(double v)
        {
            return true;
        }

        protected virtual void OnPointerUp()
        {
            down = false;
        }

    }

    internal class InputProcessorTouch : InputProcessor
    {
        private double oldV;

        public InputProcessorTouch(IRggDraw c, UIElement e, int tid, bool isVertical = false) : base(c, e)
        {
            touchBarID = tid;
            isHorizontal = !isVertical;
        }

        protected override void OnPointerDown(double v)
        {
            oldV = v;
        }

        protected override bool OnPointerMove(double v)
        {
            int delta = (int)(v - oldV);
            if (delta > 10)
            {
                controller?.HandleTouchInput(touchBarID, 1);
                oldV = v;
                return true;
            }
            else if (delta < -10)
            {
                controller?.HandleTouchInput(touchBarID, -1);
                oldV = v;
                return true;
            }
            return false;
        }
    }

}
