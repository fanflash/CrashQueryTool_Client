/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace CrashQuery.UI.Basic
{
    public partial class BaseNumericInput : GLabel
    {
        public Controller m_grayed;
        public GGraph m_holder;
        public const string URL = "ui://nk9ejx23au3n69";

        public static BaseNumericInput CreateInstance()
        {
            return (BaseNumericInput)UIPackage.CreateObject("Basic", "NumericInput");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_grayed = GetControllerAt(0);
            m_holder = (GGraph)GetChildAt(2);
        }
    }
}