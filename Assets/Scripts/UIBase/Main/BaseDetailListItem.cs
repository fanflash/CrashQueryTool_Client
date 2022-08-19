/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace CrashQuery.UI.Main
{
    public partial class BaseDetailListItem : GButton
    {
        public Controller m_ctrlSelLib;
        public Controller m_ctrlStyle;
        public GTextField m_library;
        public GTextField m_code;
        public GTextField m_path;
        public const string URL = "ui://5ifl14obmirgixicv3";

        public static BaseDetailListItem CreateInstance()
        {
            return (BaseDetailListItem)UIPackage.CreateObject("Main", "DetailListItem");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_ctrlSelLib = GetControllerAt(1);
            m_ctrlStyle = GetControllerAt(2);
            m_library = (GTextField)GetChildAt(1);
            m_code = (GTextField)GetChildAt(2);
            m_path = (GTextField)GetChildAt(3);
        }
    }
}