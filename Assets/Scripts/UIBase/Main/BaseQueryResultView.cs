/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace CrashQuery.UI.Main
{
    public partial class BaseQueryResultView : GComponent
    {
        public GList m_listResult;
        public GButton m_btnDown;
        public const string URL = "ui://5ifl14obhkf54";

        public static BaseQueryResultView CreateInstance()
        {
            return (BaseQueryResultView)UIPackage.CreateObject("Main", "QueryResultView");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_listResult = (GList)GetChildAt(0);
            m_btnDown = (GButton)GetChildAt(1);
        }
    }
}