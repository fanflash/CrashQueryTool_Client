/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace CrashQuery.UI.Basic
{
    public partial class BaseColorSBBox : GComponent
    {
        public GGraph m_sbArea;
        public GGraph m_sbValue;
        public const string URL = "ui://nk9ejx23nm5v8a";

        public static BaseColorSBBox CreateInstance()
        {
            return (BaseColorSBBox)UIPackage.CreateObject("Basic", "ColorSBBox");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_sbArea = (GGraph)GetChildAt(0);
            m_sbValue = (GGraph)GetChildAt(2);
        }
    }
}