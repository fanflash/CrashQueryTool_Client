/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace CrashQuery.UI.Basic
{
    public partial class BaseResourceInput : GLabel
    {
        public Controller m_c1;
        public Controller m_grayed;
        public GTextField m_nameText;
        public const string URL = "ui://nk9ejx23au3n6j";

        public static BaseResourceInput CreateInstance()
        {
            return (BaseResourceInput)UIPackage.CreateObject("Basic", "ResourceInput");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_c1 = GetControllerAt(0);
            m_grayed = GetControllerAt(1);
            m_nameText = (GTextField)GetChildAt(4);
        }
    }
}