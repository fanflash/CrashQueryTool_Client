/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace CrashQuery.UI.Basic
{
    public partial class BaseColorMenuItem : GButton
    {
        public Controller m_checked;
        public GGraph m_shape;
        public const string URL = "ui://nk9ejx23p06b80";

        public static BaseColorMenuItem CreateInstance()
        {
            return (BaseColorMenuItem)UIPackage.CreateObject("Basic", "ColorMenuItem");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_checked = GetControllerAt(1);
            m_shape = (GGraph)GetChildAt(2);
        }
    }
}