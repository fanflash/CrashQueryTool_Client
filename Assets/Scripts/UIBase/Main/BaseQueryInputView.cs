/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace CrashQuery.UI.Main
{
    public partial class BaseQueryInputView : GComponent
    {
        public GComboBox m_cbEditorVer;
        public GLabel m_txtFilter;
        public GList m_listApk;
        public GComboBox m_cbCputype;
        public GButton m_cbDevelpment;
        public GLabel m_txtCallStack;
        public GLabel m_txtParingTrace;
        public GButton m_btnQuery;
        public GButton m_btnCheck;
        public GTextField m_txtSelectApk;
        public const string URL = "ui://5ifl14obhkf53";

        public static BaseQueryInputView CreateInstance()
        {
            return (BaseQueryInputView)UIPackage.CreateObject("Main", "QueryInputView");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_cbEditorVer = (GComboBox)GetChildAt(1);
            m_txtFilter = (GLabel)GetChildAt(3);
            m_listApk = (GList)GetChildAt(5);
            m_cbCputype = (GComboBox)GetChildAt(7);
            m_cbDevelpment = (GButton)GetChildAt(9);
            m_txtCallStack = (GLabel)GetChildAt(10);
            m_txtParingTrace = (GLabel)GetChildAt(11);
            m_btnQuery = (GButton)GetChildAt(12);
            m_btnCheck = (GButton)GetChildAt(13);
            m_txtSelectApk = (GTextField)GetChildAt(16);
        }
    }
}