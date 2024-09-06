/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace CrashQuery.UI.Basic
{
    public partial class BaseWindowFrame : GLabel
    {
        public GGraph m_dragArea;
        public GGraph m_contentArea;
        public const string URL = "ui://nk9ejx23gcza1l";

        public static BaseWindowFrame CreateInstance()
        {
            return (BaseWindowFrame)UIPackage.CreateObject("Basic", "WindowFrame");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_dragArea = (GGraph)GetChildAt(1);
            m_contentArea = (GGraph)GetChildAt(3);
        }
    }
}