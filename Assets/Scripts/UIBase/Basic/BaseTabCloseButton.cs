/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace CrashQuery.UI.Basic
{
    public partial class BaseTabCloseButton : GButton
    {
        public Controller m_modified;
        public const string URL = "ui://nk9ejx23gcza2u";

        public static BaseTabCloseButton CreateInstance()
        {
            return (BaseTabCloseButton)UIPackage.CreateObject("Basic", "TabCloseButton");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_modified = GetControllerAt(1);
        }
    }
}