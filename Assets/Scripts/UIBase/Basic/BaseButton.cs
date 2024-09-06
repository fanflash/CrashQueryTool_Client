/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace CrashQuery.UI.Basic
{
    public partial class BaseButton : GButton
    {
        public Controller m_grayed;
        public const string URL = "ui://nk9ejx23xualm";

        public static BaseButton CreateInstance()
        {
            return (BaseButton)UIPackage.CreateObject("Basic", "Button");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_grayed = GetControllerAt(1);
        }
    }
}