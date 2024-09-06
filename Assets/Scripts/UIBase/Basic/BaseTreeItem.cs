/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace CrashQuery.UI.Basic
{
    public partial class BaseTreeItem : GButton
    {
        public Controller m_expaned;
        public Controller m_leaf;
        public GGraph m_indent;
        public GButton m_expandButton;
        public GLoader m_sign;
        public const string URL = "ui://nk9ejx23au3n5m";

        public static BaseTreeItem CreateInstance()
        {
            return (BaseTreeItem)UIPackage.CreateObject("Basic", "TreeItem");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_expaned = GetControllerAt(1);
            m_leaf = GetControllerAt(2);
            m_indent = (GGraph)GetChildAt(3);
            m_expandButton = (GButton)GetChildAt(5);
            m_sign = (GLoader)GetChildAt(6);
        }
    }
}