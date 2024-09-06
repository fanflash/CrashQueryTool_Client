/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace CrashQuery.UI.Basic
{
    public partial class BasePopupMenu_check : GComponent
    {
        public GList m_list;
        public GButton m_b0;
        public GButton m_b1;
        public GButton m_b2;
        public GButton m_b3;
        public const string URL = "ui://nk9ejx23dqzd8h";

        public static BasePopupMenu_check CreateInstance()
        {
            return (BasePopupMenu_check)UIPackage.CreateObject("Basic", "PopupMenu_check");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_list = (GList)GetChildAt(2);
            m_b0 = (GButton)GetChildAt(3);
            m_b1 = (GButton)GetChildAt(4);
            m_b2 = (GButton)GetChildAt(5);
            m_b3 = (GButton)GetChildAt(6);
        }
    }
}