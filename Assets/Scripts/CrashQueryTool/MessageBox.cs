// Author: gaofan
// Date:   2021.12.25
// Desc:

using System;
using CrashQuery.UI.Main;
using FairyGUI.Utils;

namespace CrashQuery
{
    public partial class MessageBox:BaseMessageBox
    {
        private static MessageBox m_inst;
        
        public static void Show(string message)
        {
            m_inst.ShowInner(message);
        }

        public static void Error(string error, string btnTitle, Action callback = null)
        {
            m_inst.ErrorInner(error, btnTitle, callback);
        }

        public static void Close()
        {
            m_inst.CloseInner();
        }

    }
    
    public partial class MessageBox
    {
        private Action m_callback;
        
        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);
            m_inst = this;
            m_btnOk.onClick.Add(OkBtnClickHandler);
        }

        private void OkBtnClickHandler()
        {
            var callback = m_callback;
            m_callback = null;
            CloseInner();
            
            if (callback != null)
            {
                callback();
            }
        }

        private void ShowInner(string message)
        {
            visible = true;
            m_txtMsg.text = message;
            m_ctrlError.selectedPage = "msg";
            m_callback = null;
        }

        private void ErrorInner(string error, string btnTitle, Action callback = null)
        {
            visible = true;
            m_txtMsg.text = error;
            m_ctrlError.selectedPage = "error";
            m_callback = callback;
            m_btnOk.title = btnTitle;
        }

        private void CloseInner()
        {
            visible = false;
            m_callback = null;
        }
    }
}