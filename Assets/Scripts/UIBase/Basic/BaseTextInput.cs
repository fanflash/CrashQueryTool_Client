/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace CrashQuery.UI.Basic
{
    public partial class BaseTextInput : GLabel
    {
        public Controller m_grayed;
        public Controller m_noBorder;
        public Controller m_showClear;
        public BaseFlatIconButton_nobg m_clear;
        public const string URL = "ui://nk9ejx23gcza1s";

        public static BaseTextInput CreateInstance()
        {
            return (BaseTextInput)UIPackage.CreateObject("Basic", "TextInput");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_grayed = GetControllerAt(0);
            m_noBorder = GetControllerAt(1);
            m_showClear = GetControllerAt(2);
            m_clear = (BaseFlatIconButton_nobg)GetChildAt(4);
        }
    }
}