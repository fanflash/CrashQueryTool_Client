/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace CrashQuery.UI.Basic
{
    public partial class BaseSlider_HZ : GSlider
    {
        public GGraph m_bg;
        public const string URL = "ui://nk9ejx23gcza2c";

        public static BaseSlider_HZ CreateInstance()
        {
            return (BaseSlider_HZ)UIPackage.CreateObject("Basic", "Slider_HZ");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_bg = (GGraph)GetChildAt(0);
        }
    }
}