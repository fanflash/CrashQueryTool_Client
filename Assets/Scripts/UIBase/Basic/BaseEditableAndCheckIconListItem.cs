/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace CrashQuery.UI.Basic
{
    public partial class BaseEditableAndCheckIconListItem : GButton
    {
        public GLoader m_sign;
        public GButton m_cb;
        public const string URL = "ui://nk9ejx23rj5hixicuw";

        public static BaseEditableAndCheckIconListItem CreateInstance()
        {
            return (BaseEditableAndCheckIconListItem)UIPackage.CreateObject("Basic", "EditableAndCheckIconListItem");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_sign = (GLoader)GetChildAt(4);
            m_cb = (GButton)GetChildAt(5);
        }
    }
}