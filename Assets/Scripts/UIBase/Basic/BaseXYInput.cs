/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace CrashQuery.UI.Basic
{
    public partial class BaseXYInput : GLabel
    {
        public Controller m_grayed;
        public Controller m_xy;
        public GGraph m_holder;
        public const string URL = "ui://nk9ejx23wqe79a";

        public static BaseXYInput CreateInstance()
        {
            return (BaseXYInput)UIPackage.CreateObject("Basic", "XYInput");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_grayed = GetControllerAt(0);
            m_xy = GetControllerAt(1);
            m_holder = (GGraph)GetChildAt(2);
        }
    }
}