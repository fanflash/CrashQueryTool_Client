/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace CrashQuery.UI.Basic
{
    public partial class BaseFlatIconButton_Check : GButton
    {
        public Controller m_grayed;
        public const string URL = "ui://nk9ejx23gj1q8s";

        public static BaseFlatIconButton_Check CreateInstance()
        {
            return (BaseFlatIconButton_Check)UIPackage.CreateObject("Basic", "FlatIconButton_Check");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_grayed = GetControllerAt(1);
        }
    }
}