/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace CrashQuery.UI.Main
{
    public partial class BaseQueryInputView : GComponent
    {
        public GComboBox m_cbEditorVer;
        public GLabel m_txtApk;
        public GList m_listApk;
        public GComboBox m_cbCputype;
        public GButton m_cbDevelpment;
        public GLabel m_txtCallStack;
        public GButton m_btnQuery;
        public GButton m_btnCheck;
        public const string URL = "ui://5ifl14obhkf53";

        public static BaseQueryInputView CreateInstance()
        {
            return (BaseQueryInputView)UIPackage.CreateObject("Main", "QueryInputView");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_cbEditorVer = (GComboBox)GetChildAt(1);
            m_txtApk = (GLabel)GetChildAt(3);
            m_listApk = (GList)GetChildAt(4);
            m_cbCputype = (GComboBox)GetChildAt(6);
            m_cbDevelpment = (GButton)GetChildAt(8);
            m_txtCallStack = (GLabel)GetChildAt(9);
            m_btnQuery = (GButton)GetChildAt(10);
            m_btnCheck = (GButton)GetChildAt(11);
        }
    }
}