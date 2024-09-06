/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace CrashQuery.UI.Basic
{
    public partial class BaseButtonWithIcon : GButton
    {
        public Controller m_grayed;
        public const string URL = "ui://nk9ejx23exa7ixicux";

        public static BaseButtonWithIcon CreateInstance()
        {
            return (BaseButtonWithIcon)UIPackage.CreateObject("Basic", "ButtonWithIcon");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_grayed = GetControllerAt(1);
        }
    }
}