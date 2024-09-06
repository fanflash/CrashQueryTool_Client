/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace CrashQuery.UI.Basic
{
    public partial class BaseEditableIconListItemBig : GButton
    {
        public GLoader m_sign;
        public const string URL = "ui://nk9ejx23ep4rixicuu";

        public static BaseEditableIconListItemBig CreateInstance()
        {
            return (BaseEditableIconListItemBig)UIPackage.CreateObject("Basic", "EditableIconListItemBig");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_sign = (GLoader)GetChildAt(3);
        }
    }
}