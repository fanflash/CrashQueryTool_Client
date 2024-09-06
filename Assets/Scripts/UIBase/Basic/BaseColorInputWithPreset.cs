/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace CrashQuery.UI.Basic
{
    public partial class BaseColorInputWithPreset : GButton
    {
        public Controller m_grayed;
        public BaseFlatIconButton m_arrow;
        public const string URL = "ui://nk9ejx23p06b7x";

        public static BaseColorInputWithPreset CreateInstance()
        {
            return (BaseColorInputWithPreset)UIPackage.CreateObject("Basic", "ColorInputWithPreset");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_grayed = GetControllerAt(1);
            m_arrow = (BaseFlatIconButton)GetChildAt(2);
        }
    }
}