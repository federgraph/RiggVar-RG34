using RiggVar.Rgg;
using System.Collections.Generic;

namespace RiggVar.FB
{
    public class RggActionGroups : List<int[]>
    {
        readonly TStringList GroupNames = new TStringList();

        private void AddSpecial(int[] gl, string gn)
        {
            GroupNames.Add(gn);
            Add(gl);
        }

        public int ActionCount
        {
            get
            {
                int j = 0;
                for (int i = 0; i < Count; i++)
                {
                    j += this[i].Length;
                }
                return j;
            }
        }

        public RggActionGroups()
        {
            // App
            AddSpecial(RggActions.ActionGroupEmptyAction, "EmptyAction");
            AddSpecial(RggActions.ActionGroupPages, "Pages");
            AddSpecial(RggActions.ActionGroupForms, "Forms");
            AddSpecial(RggActions.ActionGroupTouchLayout, "TouchLayout");

            // UI
            AddSpecial(RggActions.ActionGroupWheel, "Wheel");
            AddSpecial(RggActions.ActionGroupColorScheme, "ColorScheme");

            // View
            AddSpecial(RggActions.ActionGroupFederText, "FederText");
            AddSpecial(RggActions.ActionGroupViewParams, "ViewParams");
            AddSpecial(RggActions.ActionGroupParamT, "ParamT");

            // RG
            AddSpecial(RggActions.ActionGroupRggControls, "RggControls");
            AddSpecial(RggActions.ActionGroupRggFixPoints, "RggFixPoints");
            AddSpecial(RggActions.ActionGroupRggViewPoint, "RggViewPoint");
            AddSpecial(RggActions.ActionGroupRggSalingType, "RggSalingType");
            AddSpecial(RggActions.ActionGroupRggCalcType, "RggCalcType");
            AddSpecial(RggActions.ActionGroupRggAppMode, "RggAppMode");
            AddSpecial(RggActions.ActionGroupRggSuper, "RggSuper");
            AddSpecial(RggActions.ActionGroupRggReport, "RggReport");
            AddSpecial(RggActions.ActionGroupRggChart, "RggChart");
            AddSpecial(RggActions.ActionGroupRggGraph, "RggGraph");
            AddSpecial(RggActions.ActionGroupRggSegment, "RggSegment");
            AddSpecial(RggActions.ActionGroupRggRenderOptions, "RggRenderOptions");
            AddSpecial(RggActions.ActionGroupRggTrimms, "RggTrimms");
            AddSpecial(RggActions.ActionGroupRggTrimmFile, "RggTrimmFile");
            AddSpecial(RggActions.ActionGroupRggTrimmText, "RggTrimmText");
            AddSpecial(RggActions.ActionGroupRggSonstiges, "RggSonstiges");
            AddSpecial(RggActions.ActionGroupRggInfo, "RggInfo");

            // TouchFrame Buttons
            AddSpecial(RggActions.ActionGroupBtnLegendTablet, "BtnLegendTablet");
            AddSpecial(RggActions.ActionGroupBtnLegendPhone, "BtnLegendPhone");
            AddSpecial(RggActions.ActionGroupTouchBarLegend, "TouchBarLegend");

            AddSpecial(RggActions.ActionGroupCircles, "Circles");
            AddSpecial(RggActions.ActionGroupMemeFormat, "MemeFormat");
            AddSpecial(RggActions.ActionGroupReset, "Reset");
            AddSpecial(RggActions.ActionGroupViewType, "ViewType");
            AddSpecial(RggActions.ActionGroupDropTarget, "DropTarget");
            AddSpecial(RggActions.ActionGroupLanguage, "Language");
            AddSpecial(RggActions.ActionGroupCopyPaste, "CopyPaste");

            AddSpecial(RggActions.ActionGroupViewOptions, "ViewOptions");
            AddSpecial(RggActions.ActionGroupHullMesh, "HullMesh");
            AddSpecial(RggActions.ActionGroupBitmapCycle, "BitmapCycle");
        }

        public string GetGroupName(int i)
        {
            if ((i >= 0) && (i < GroupNames.Count) && (i < Count))
                return GroupNames[i];
            return string.Empty;
        }

        public int GetGroup(int fa)
        {
            int[] cr;
            int l;
            for (int i = 0; i < Count; i++)
            {
                cr = this[i];
                l = cr.Length;
                for (int j = 0; j < l-1; j++)
                {
                    if (cr[j] == fa)
                    {
                        return i;
                    }
                }
            }
            return -1;
        }

        public string GetUsage() {
            TStringList SL = new TStringList();
            for (int i = 0; i < RggActions.faMax; i++)
            {
                SL.Add(string.Format("{0}=0", i));
            }

            string s1 = "1";
            int[] cr;
            int l;
            for (int i = 0; i < Count; i++)
            {
                cr = this[i];
                l = cr.Length;
                for (int j = 0; j < l; j++)
                {
                    SL.Values(cr[j].ToString(), s1);
                }
            }

            for (int i = SL.Count-1; i >= 0; i--)
            {
                if (SL.Values(i.ToString()) == "1") {
                    SL.Delete(i);
                }
            }
            return SL.Text;
        }

    }
}
