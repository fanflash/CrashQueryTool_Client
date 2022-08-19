/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace CrashQuery.UI.Main
{
    public partial class BaseSaveFileDialog : GComponent
    {
        public GLabel m_frame;
        public GLabel m_txtPath;
        public GButton m_btnSave;
        public GButton m_btnCancel;
        public const string URL = "ui://5ifl14obqqbuixicv4";

        public static BaseSaveFileDialog CreateInstance()
        {
            return (BaseSaveFileDialog)UIPackage.CreateObject("Main", "SaveFileDialog");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_frame = (GLabel)GetChildAt(1);
            m_txtPath = (GLabel)GetChildAt(2);
            m_btnSave = (GButton)GetChildAt(4);
            m_btnCancel = (GButton)GetChildAt(5);
        }
    }
}