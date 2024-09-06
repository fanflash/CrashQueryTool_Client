/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace CrashQuery.UI.Basic
{
    public partial class BaseColorPickerPopup : GComponent
    {
        public Controller m_showAlpha;
        public BaseTextInput m_currentColorValue;
        public GImage m_colorTable;
        public GGraph m_currentColorBox;
        public BaseNumericInput m_alphaInput;
        public BaseFlatIconButton m_more;
        public BaseFlatIconButton m_picker;
        public const string URL = "ui://nk9ejx23au3n5x";

        public static BaseColorPickerPopup CreateInstance()
        {
            return (BaseColorPickerPopup)UIPackage.CreateObject("Basic", "ColorPickerPopup");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_showAlpha = GetControllerAt(0);
            m_currentColorValue = (BaseTextInput)GetChildAt(1);
            m_colorTable = (GImage)GetChildAt(2);
            m_currentColorBox = (GGraph)GetChildAt(4);
            m_alphaInput = (BaseNumericInput)GetChildAt(6);
            m_more = (BaseFlatIconButton)GetChildAt(9);
            m_picker = (BaseFlatIconButton)GetChildAt(10);
        }
    }
}