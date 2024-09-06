/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace CrashQuery.UI.Basic
{
    public partial class BasePopupMenuItem3 : GButton
    {
        public Controller m_checked;
        public const string URL = "ui://nk9ejx23mvcw8j";

        public static BasePopupMenuItem3 CreateInstance()
        {
            return (BasePopupMenuItem3)UIPackage.CreateObject("Basic", "PopupMenuItem3");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_checked = GetControllerAt(1);
        }
    }
}