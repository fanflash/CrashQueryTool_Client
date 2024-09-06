/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace CrashQuery.UI.Basic
{
    public partial class BaseComboBoxPopup : GComponent
    {
        public GList m_list;
        public const string URL = "ui://nk9ejx23gcza1a";

        public static BaseComboBoxPopup CreateInstance()
        {
            return (BaseComboBoxPopup)UIPackage.CreateObject("Basic", "ComboBoxPopup");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_list = (GList)GetChildAt(1);
        }
    }
}