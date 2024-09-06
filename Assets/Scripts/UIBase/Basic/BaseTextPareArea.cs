/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace CrashQuery.UI.Basic
{
    public partial class BaseTextPareArea : GComponent
    {
        public GTextInput m_txtContent;
        public const string URL = "ui://nk9ejx23awloixicv2";

        public static BaseTextPareArea CreateInstance()
        {
            return (BaseTextPareArea)UIPackage.CreateObject("Basic", "TextPareArea");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_txtContent = (GTextInput)GetChildAt(0);
        }
    }
}