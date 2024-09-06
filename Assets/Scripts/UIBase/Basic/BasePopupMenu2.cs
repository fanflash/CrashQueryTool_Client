/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace CrashQuery.UI.Basic
{
    public partial class BasePopupMenu2 : GComponent
    {
        public GList m_list;
        public const string URL = "ui://nk9ejx23dqzd8i";

        public static BasePopupMenu2 CreateInstance()
        {
            return (BasePopupMenu2)UIPackage.CreateObject("Basic", "PopupMenu2");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_list = (GList)GetChildAt(1);
        }
    }
}