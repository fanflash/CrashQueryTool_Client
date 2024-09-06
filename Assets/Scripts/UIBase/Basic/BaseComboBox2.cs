/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace CrashQuery.UI.Basic
{
    public partial class BaseComboBox2 : GComboBox
    {
        public Controller m_grayed;
        public const string URL = "ui://nk9ejx23kiv0ixicuz";

        public static BaseComboBox2 CreateInstance()
        {
            return (BaseComboBox2)UIPackage.CreateObject("Basic", "ComboBox2");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_grayed = GetControllerAt(1);
        }
    }
}