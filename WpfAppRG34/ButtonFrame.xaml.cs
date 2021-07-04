using RiggVar.FB;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace RiggVar.Rgg
{
    public partial class ButtonFrame : UserControl
    {
        private readonly Dictionary<Rectangle, CornerButton> CornerBtnDict = new Dictionary<Rectangle, CornerButton>();
        private readonly Style BtnRectStyle;
        private readonly Style BtnTextStyle;

        private CornerButton Btn1;
        private CornerButton Btn2;
        private CornerButton Btn3;
        private CornerButton Btn4;
        private CornerButton Btn5;

        private CornerButton Btn6;
        private CornerButton Btn7;
        private CornerButton Btn8;
        private CornerButton Btn9;
        private CornerButton Btn10;

        private CornerButton Btn11;
        private CornerButton Btn12;
        private CornerButton Btn13;
        private CornerButton Btn14;
        private CornerButton Btn15;
        private CornerButton Btn16;

        internal IRggDraw RggDraw;

        public ButtonFrame()
        {
            InitializeComponent();

            BtnRectStyle = BtnRect0.Style;
            BtnTextStyle = BtnText0.Style;

            InitButtons();
        }

        private void InitButtons()
        {
            Brush cl;

            // top left
            cl = Brushes.White;

            Btn1 = InitButton(GridPos1);
            Btn1.InitBtn(1);
            Btn1.BtnRect.Fill = Brushes.Yellow;
            Btn1.ActionID = RggActions.faReset;

            Btn2 = InitButton(GridPos2);
            Btn2.InitBtn(2);
            Btn2.BtnRect.Fill = cl;
            Btn2.ActionID = RggActions.faRggBogen;

            Btn3 = InitButton(GridPos3);
            Btn3.InitBtn(3);
            Btn3.BtnRect.Fill = cl;
            Btn3.ActionID = RggActions.faRggKoppel;

            Btn4 = InitButton(GridPos4);
            Btn4.InitBtn(4);
            Btn4.BtnRect.Fill = Brushes.LightGoldenrodYellow;
            Btn4.ActionID = RggActions.faFixpointD;

            Btn5 = InitButton(GridPos5);
            Btn5.InitBtn(5);
            Btn5.BtnRect.Fill = Brushes.LightGoldenrodYellow;
            Btn5.ActionID = RggActions.faFixpointD0;

            // top right left
            cl = Brushes.Plum;

            Btn6 = InitButton(GridPos6);
            Btn6.InitBtn(6);
            Btn6.BtnRect.Fill = cl;
            Btn6.ActionID = RggActions.faVorstag;

            Btn7 = InitButton(GridPos7);
            Btn7.InitBtn(7);
            Btn7.BtnRect.Fill = cl;
            Btn7.ActionID = RggActions.faWante;

            Btn8 = InitButton(GridPos8);
            Btn8.InitBtn(8);
            Btn8.BtnRect.Fill = cl;
            Btn8.ActionID = RggActions.faSalingH;

            Btn9 = InitButton(GridPos9);
            Btn9.InitBtn(9);
            Btn9.BtnRect.Fill = cl;
            Btn9.ActionID = RggActions.faSalingA;

            // bottom left
            cl = Brushes.CornflowerBlue;

            Btn10 = InitButton(GridPos10);
            Btn10.InitBtn(10);
            Btn10.BtnRect.Fill = cl;
            Btn10.ActionID = RggActions.fa420;

            Btn11 = InitButton(GridPos11);
            Btn11.InitBtn(11);
            Btn11.BtnRect.Fill = Brushes.Aquamarine;
            Btn11.ActionID = RggActions.faViewpointS;

            Btn12 = InitButton(GridPos12);
            Btn12.InitBtn(12);
            Btn12.BtnRect.Fill = Brushes.Aquamarine;
            Btn12.ActionID = RggActions.faViewpoint3;

            Btn13 = InitButton(GridPos13);
            Btn13.InitBtn(13);
            Btn13.BtnRect.Fill = cl;
            Btn13.ActionID = RggActions.faLogo;

            // bottom right
            cl = Brushes.Aqua;

            Btn14 = InitButton(GridPos14);
            Btn14.InitBtn(14);
            Btn14.BtnRect.Fill = cl;
            Btn14.ActionID = RggActions.faMastfallF0F;

            Btn15 = InitButton(GridPos15);
            Btn15.InitBtn(15);
            Btn15.BtnRect.Fill = cl;
            Btn15.ActionID = RggActions.faMastfallF0C;

            Btn16 = InitButton(GridPos16);
            Btn16.InitBtn(16);
            Btn16.BtnRect.Fill = cl;
            Btn16.ActionID = RggActions.faBiegung;
        }

        private CornerButton InitButton(Grid g)
        {
            CornerButton cb = new CornerButton();

            g.Children.Clear();

            Rectangle r = cb.BtnRect;
            r.Style = BtnRectStyle;
            _ = g.Children.Add(r);

            TextBlock t = cb.BtnText;
            t.IsHitTestVisible = false;
            t.Style = BtnTextStyle;
            _ = g.Children.Add(t);

            CornerBtnDict.Add(r, cb);
            r.MouseUp += RectangleBtnClick;
            r.MouseEnter += RectangleMouseEnter;

            return cb;
        }

        private void RectangleMouseEnter(object sender, MouseEventArgs e)
        {
            Rectangle r = sender as Rectangle;
            if (CornerBtnDict.TryGetValue(r, out CornerButton cb))
            {
                RggDraw?.HandleUpdateHint(cb.ActionID);
            }
        }

        private void RectangleBtnClick(object sender, MouseButtonEventArgs e)
        {
            Rectangle r = sender as Rectangle;
            if (CornerBtnDict.TryGetValue(r, out CornerButton cb))
            {
                RggDraw?.HandleBtnClick(cb.ActionID);
            }
        }

    }
}
