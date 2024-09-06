/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace CrashQuery.UI.Basic
{
    public partial class BaseEditableIconListItem : GButton
    {
        public GLoader m_sign;
        public const string URL = "ui://nk9ejx23d8p07iuek";

        public static BaseEditableIconListItem CreateInstance()
        {
            return (BaseEditableIconListItem)UIPackage.CreateObject("Basic", "EditableIconListItem");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_sign = (GLoader)GetChildAt(4);
        }
    }
}