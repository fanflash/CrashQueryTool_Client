/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace CrashQuery.UI.Basic
{
    public partial class BaseFlatIconButton_Radio : GButton
    {
        public Controller m_grayed;
        public const string URL = "ui://nk9ejx23gj1q8v";

        public static BaseFlatIconButton_Radio CreateInstance()
        {
            return (BaseFlatIconButton_Radio)UIPackage.CreateObject("Basic", "FlatIconButton_Radio");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_grayed = GetControllerAt(1);
        }
    }
}