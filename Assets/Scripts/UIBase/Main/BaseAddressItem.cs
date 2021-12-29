/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace CrashQuery.UI.Main
{
    public partial class BaseAddressItem : GComponent
    {
        public GTextInput m_title;
        public const string URL = "ui://5ifl14obkc5pixicv1";

        public static BaseAddressItem CreateInstance()
        {
            return (BaseAddressItem)UIPackage.CreateObject("Main", "AddressItem");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_title = (GTextInput)GetChildAt(0);
        }
    }
}