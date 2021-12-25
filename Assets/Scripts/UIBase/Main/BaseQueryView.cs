/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace CrashQuery.UI.Main
{
    public partial class BaseQueryView : GComponent
    {
        public BaseQueryInputView m_inputView;
        public BaseQueryResultView m_resultView;
        public const string URL = "ui://5ifl14obhkf52";

        public static BaseQueryView CreateInstance()
        {
            return (BaseQueryView)UIPackage.CreateObject("Main", "QueryView");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_inputView = (BaseQueryInputView)GetChildAt(0);
            m_resultView = (BaseQueryResultView)GetChildAt(1);
        }
    }
}