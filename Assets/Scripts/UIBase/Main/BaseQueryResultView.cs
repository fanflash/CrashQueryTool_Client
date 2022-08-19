/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace CrashQuery.UI.Main
{
    public partial class BaseQueryResultView : GComponent
    {
        public Controller m_ctrlPlatform;
        public GList m_listResult;
        public GButton m_btnDown;
        public GList m_listDetail;
        public GTextField m_address;
        public GButton m_btnCopy;
        public const string URL = "ui://5ifl14obhkf54";

        public static BaseQueryResultView CreateInstance()
        {
            return (BaseQueryResultView)UIPackage.CreateObject("Main", "QueryResultView");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_ctrlPlatform = GetControllerAt(0);
            m_listResult = (GList)GetChildAt(0);
            m_btnDown = (GButton)GetChildAt(1);
            m_listDetail = (GList)GetChildAt(5);
            m_address = (GTextField)GetChildAt(7);
            m_btnCopy = (GButton)GetChildAt(10);
        }
    }
}