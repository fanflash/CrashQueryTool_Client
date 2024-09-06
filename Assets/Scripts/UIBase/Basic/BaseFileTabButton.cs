/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace CrashQuery.UI.Basic
{
    public partial class BaseFileTabButton : GButton
    {
        public Controller m_modified;
        public BaseTabCloseButton m_closeButton;
        public const string URL = "ui://nk9ejx23p0yg7r";

        public static BaseFileTabButton CreateInstance()
        {
            return (BaseFileTabButton)UIPackage.CreateObject("Basic", "FileTabButton");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_modified = GetControllerAt(1);
            m_closeButton = (BaseTabCloseButton)GetChildAt(4);
        }
    }
}