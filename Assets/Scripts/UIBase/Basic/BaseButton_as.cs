/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace CrashQuery.UI.Basic
{
    public partial class BaseButton_as : GButton
    {
        public Controller m_grayed;
        public const string URL = "ui://nk9ejx23gss372";

        public static BaseButton_as CreateInstance()
        {
            return (BaseButton_as)UIPackage.CreateObject("Basic", "Button_as");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_grayed = GetControllerAt(1);
        }
    }
}