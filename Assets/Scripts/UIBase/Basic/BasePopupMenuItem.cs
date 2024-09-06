/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace CrashQuery.UI.Basic
{
    public partial class BasePopupMenuItem : GButton
    {
        public Controller m_checked;
        public Controller m_grayed;
        public GTextField m_shortcut;
        public GImage m_arrow;
        public const string URL = "ui://nk9ejx23gcza2h";

        public static BasePopupMenuItem CreateInstance()
        {
            return (BasePopupMenuItem)UIPackage.CreateObject("Basic", "PopupMenuItem");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_checked = GetControllerAt(1);
            m_grayed = GetControllerAt(2);
            m_shortcut = (GTextField)GetChildAt(4);
            m_arrow = (GImage)GetChildAt(5);
        }
    }
}