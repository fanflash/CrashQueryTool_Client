/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace CrashQuery.UI.Main
{
    public partial class BaseMainPanel : GComponent
    {
        public Controller m_ctrlState;
        public BaseLoginView m_loginView;
        public BaseQueryView m_queryView;
        public const string URL = "ui://5ifl14obonwu0";

        public static BaseMainPanel CreateInstance()
        {
            return (BaseMainPanel)UIPackage.CreateObject("Main", "MainPanel");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_ctrlState = GetControllerAt(0);
            m_loginView = (BaseLoginView)GetChildAt(1);
            m_queryView = (BaseQueryView)GetChildAt(2);
        }
    }
}