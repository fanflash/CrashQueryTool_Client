/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace CrashQuery.UI.Basic
{
    public partial class BaseTextArea : GLabel
    {
        public Controller m_showEditButton;
        public GButton m_textEdit;
        public const string URL = "ui://nk9ejx23au3n6k";

        public static BaseTextArea CreateInstance()
        {
            return (BaseTextArea)UIPackage.CreateObject("Basic", "TextArea");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_showEditButton = GetControllerAt(0);
            m_textEdit = (GButton)GetChildAt(2);
        }
    }
}