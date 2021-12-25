/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace CrashQuery.UI.Main
{
    public partial class BaseMessageBox : GComponent
    {
        public Controller m_ctrlError;
        public GLabel m_frame;
        public GTextField m_txtMsg;
        public GButton m_btnOk;
        public const string URL = "ui://5ifl14obekry5";

        public static BaseMessageBox CreateInstance()
        {
            return (BaseMessageBox)UIPackage.CreateObject("Main", "MessageBox");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_ctrlError = GetControllerAt(0);
            m_frame = (GLabel)GetChildAt(1);
            m_txtMsg = (GTextField)GetChildAt(2);
            m_btnOk = (GButton)GetChildAt(3);
        }
    }
}