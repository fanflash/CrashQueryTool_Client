/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace CrashQuery.UI.Basic
{
    public partial class BaseListItemInputCenter : GLabel
    {
        public Controller m_c1;
        public GTextInput m_input;
        public const string URL = "ui://nk9ejx23ep4rixicuv";

        public static BaseListItemInputCenter CreateInstance()
        {
            return (BaseListItemInputCenter)UIPackage.CreateObject("Basic", "ListItemInputCenter");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_c1 = GetControllerAt(0);
            m_input = (GTextInput)GetChildAt(2);
        }
    }
}