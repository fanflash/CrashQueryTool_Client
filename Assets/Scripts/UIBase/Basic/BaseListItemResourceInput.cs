/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace CrashQuery.UI.Basic
{
    public partial class BaseListItemResourceInput : GLabel
    {
        public Controller m_c1;
        public GTextField m_nameText;
        public const string URL = "ui://nk9ejx23au3n6r";

        public static BaseListItemResourceInput CreateInstance()
        {
            return (BaseListItemResourceInput)UIPackage.CreateObject("Basic", "ListItemResourceInput");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_c1 = GetControllerAt(0);
            m_nameText = (GTextField)GetChildAt(1);
        }
    }
}