/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace CrashQuery.UI.Basic
{
    public partial class BaseColorInput : GButton
    {
        public Controller m_mode;
        public const string URL = "ui://nk9ejx23au3n5y";

        public static BaseColorInput CreateInstance()
        {
            return (BaseColorInput)UIPackage.CreateObject("Basic", "ColorInput");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_mode = GetControllerAt(1);
        }
    }
}