using RiggVar.FB;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace RiggVar.Rgg
{
    public class CornerButton
    {
        public Rectangle BtnRect;
        public TextBlock BtnText;
        public int BtnPos;
        private int actionID = RggActions.faNoop;

        public CornerButton()
        {
            BtnRect = new Rectangle();
            BtnText = new TextBlock();
        }

        public int ActionID
        {
            get => actionID;
            set
            {
                actionID = value;
                BtnText.Text = RggActions.GetFederActionShort(value);
            }
        }

        public void InitBtn(int btnPos)
        {
            actionID = RggActions.faNoop;

            BtnPos = btnPos;
            BtnRect.Tag = btnPos;
            BtnText.Text = btnPos.ToString();
        }

    }
}
