/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace CrashQuery.UI.Basic
{
    public partial class BaseComboBox : GComboBox
    {
        public Controller m_grayed;
        public const string URL = "ui://nk9ejx23gcza18";

        public static BaseComboBox CreateInstance()
        {
            return (BaseComboBox)UIPackage.CreateObject("Basic", "ComboBox");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_grayed = GetControllerAt(1);
        }
    }
}