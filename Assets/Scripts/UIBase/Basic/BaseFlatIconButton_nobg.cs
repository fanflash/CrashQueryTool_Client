/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace CrashQuery.UI.Basic
{
    public partial class BaseFlatIconButton_nobg : GButton
    {
        public Controller m_grayed;
        public const string URL = "ui://nk9ejx23dp9p97";

        public static BaseFlatIconButton_nobg CreateInstance()
        {
            return (BaseFlatIconButton_nobg)UIPackage.CreateObject("Basic", "FlatIconButton_nobg");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_grayed = GetControllerAt(1);
        }
    }
}