/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace CrashQuery.UI.Basic
{
    public partial class BaseEditableTreeItem : GButton
    {
        public Controller m_editing;
        public Controller m_expanded;
        public Controller m_leaf;
        public GGraph m_indent;
        public GTextInput m_input;
        public GButton m_expandButton;
        public GLoader m_sign;
        public const string URL = "ui://nk9ejx23au3n6t";

        public static BaseEditableTreeItem CreateInstance()
        {
            return (BaseEditableTreeItem)UIPackage.CreateObject("Basic", "EditableTreeItem");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_editing = GetControllerAt(1);
            m_expanded = GetControllerAt(2);
            m_leaf = GetControllerAt(3);
            m_indent = (GGraph)GetChildAt(3);
            m_input = (GTextInput)GetChildAt(6);
            m_expandButton = (GButton)GetChildAt(8);
            m_sign = (GLoader)GetChildAt(9);
        }
    }
}