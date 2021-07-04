using System.Collections.Generic;

namespace RiggVar.FB
{
    public class ActionRecord
    {
        public int ID;
        public string Name;
        public string ShortCaption;
        public string LongCaption;
        public override string ToString()
        {
            return string.Format("{0}: {1} = {2} = {3}", ID, Name, ShortCaption, LongCaption);
        }
    }

    public partial class RggActionList : Dictionary<int, ActionRecord>
    {
        private bool initialized;
        public static ActionRecord GetActionRecord(int fa)
        {
            ActionRecord ar = new ActionRecord();
            ar.ID = fa;
            ar.Name = RggActions.GetFederActionName(fa);
            ar.LongCaption = RggActions.GetFederActionLong(fa);
            ar.ShortCaption = RggActions.GetFederActionShort(fa);
            return ar;
        }
        public bool AddRecord(ActionRecord ar)
        {
            if (!ContainsKey(ar.ID))
            {
                Add(ar.ID, ar);
                return true;
            }
            return false;
        }

        public void AddRecord(int fa)
        {
            if (!ContainsKey(fa))
                Add(fa, GetActionRecord(fa));
        }
        public void AddAll()
        {
            if (!initialized)
                for (int i = 0; i < RggActions.faMax; i++)
                {
                    AddRecord(i);
                }
            initialized = true;
        }
    }
}
