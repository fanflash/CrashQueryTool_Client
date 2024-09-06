/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace CrashQuery.UI.Basic
{
    public partial class BasePopupMenu : GComponent
    {
        public GList m_list;
        public const string URL = "ui://nk9ejx23gcza2g";

        public static BasePopupMenu CreateInstance()
        {
            return (BasePopupMenu)UIPackage.CreateObject("Basic", "PopupMenu");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_list = (GList)GetChildAt(1);
        }
    }
}