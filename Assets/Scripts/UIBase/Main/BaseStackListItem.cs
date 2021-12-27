/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace CrashQuery.UI.Main
{
    public partial class BaseStackListItem : GButton
    {
        public Controller m_ctrlSelLib;
        public GTextField m_txtAddress;
        public GTextField m_txtCode;
        public GComboBox m_cbLib;
        public GTextField m_txtLibName;
        public const string URL = "ui://5ifl14oblkr4ixicv0";

        public static BaseStackListItem CreateInstance()
        {
            return (BaseStackListItem)UIPackage.CreateObject("Main", "StackListItem");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_ctrlSelLib = GetControllerAt(1);
            m_txtAddress = (GTextField)GetChildAt(2);
            m_txtCode = (GTextField)GetChildAt(3);
            m_cbLib = (GComboBox)GetChildAt(5);
            m_txtLibName = (GTextField)GetChildAt(6);
        }
    }
}