/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace CrashQuery.UI.Basic
{
    public partial class BaseFlatIconButton : GButton
    {
        public Controller m_grayed;
        public const string URL = "ui://nk9ejx23gcza2i";

        public static BaseFlatIconButton CreateInstance()
        {
            return (BaseFlatIconButton)UIPackage.CreateObject("Basic", "FlatIconButton");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_grayed = GetControllerAt(1);
        }
    }
}