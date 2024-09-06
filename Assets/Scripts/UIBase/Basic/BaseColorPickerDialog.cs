/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace CrashQuery.UI.Basic
{
    public partial class BaseColorPickerDialog : GComponent
    {
        public Controller m_showAlpha;
        public BaseWindowFrame m_frame;
        public BaseButton m_ok;
        public BaseButton m_cancel;
        public GSlider m_hueSlider;
        public BaseColorSBBox m_sbBox;
        public GGraph m_currentColorBox;
        public GGraph m_oldColorBox;
        public BaseNumericInput m_hsb_h;
        public BaseNumericInput m_hsb_s;
        public BaseNumericInput m_hsb_b;
        public BaseNumericInput m_rgb_r;
        public BaseNumericInput m_rgb_g;
        public BaseNumericInput m_rgb_b;
        public BaseTextInput m_alphaValue;
        public BaseTextInput m_colorValue;
        public BaseButton m_apply;
        public const string URL = "ui://nk9ejx23ss7s86";

        public static BaseColorPickerDialog CreateInstance()
        {
            return (BaseColorPickerDialog)UIPackage.CreateObject("Basic", "ColorPickerDialog");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_showAlpha = GetControllerAt(0);
            m_frame = (BaseWindowFrame)GetChildAt(0);
            m_ok = (BaseButton)GetChildAt(1);
            m_cancel = (BaseButton)GetChildAt(2);
            m_hueSlider = (GSlider)GetChildAt(3);
            m_sbBox = (BaseColorSBBox)GetChildAt(4);
            m_currentColorBox = (GGraph)GetChildAt(6);
            m_oldColorBox = (GGraph)GetChildAt(7);
            m_hsb_h = (BaseNumericInput)GetChildAt(16);
            m_hsb_s = (BaseNumericInput)GetChildAt(17);
            m_hsb_b = (BaseNumericInput)GetChildAt(18);
            m_rgb_r = (BaseNumericInput)GetChildAt(19);
            m_rgb_g = (BaseNumericInput)GetChildAt(20);
            m_rgb_b = (BaseNumericInput)GetChildAt(21);
            m_alphaValue = (BaseTextInput)GetChildAt(22);
            m_colorValue = (BaseTextInput)GetChildAt(23);
            m_apply = (BaseButton)GetChildAt(27);
        }
    }
}