/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace CrashQuery.UI.Main
{
    public partial class BaseLoginView : GComponent
    {
        public Controller m_ctrlState;
        public GLabel m_txtUserName;
        public GLabel m_txtPassword;
        public GButton m_btnEnter;
        public const string URL = "ui://5ifl14obfn7i1";

        public static BaseLoginView CreateInstance()
        {
            return (BaseLoginView)UIPackage.CreateObject("Main", "LoginView");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_ctrlState = GetControllerAt(0);
            m_txtUserName = (GLabel)GetChildAt(3);
            m_txtPassword = (GLabel)GetChildAt(4);
            m_btnEnter = (GButton)GetChildAt(5);
        }
    }
}