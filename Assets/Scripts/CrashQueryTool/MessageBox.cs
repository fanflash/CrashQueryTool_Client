// Author: gaofan
// Date:   2021.12.25
// Desc:

using System;
using CrashQuery.UI.Main;
using FairyGUI;
using FairyGUI.Utils;
using UnityEngine;

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
            m_inst.CloseInner(false);
        }

    }
    
    public partial class MessageBox
    {
        private Action m_callback;
        private float m_startShowTime;
        
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
            ResetClose();
        }

        private void ErrorInner(string error, string btnTitle, Action callback = null)
        {
            visible = true;
            m_txtMsg.text = error;
            m_ctrlError.selectedPage = "error";
            m_callback = callback;
            m_btnOk.title = btnTitle;
            ResetClose();
        }

        private void CloseInner(bool immediate = true)
        {
            if (immediate)
            {
                m_startShowTime = 0;
            }
            m_callback = null;
            
            const float showLimit = 1f;
            var d = Time.realtimeSinceStartup - m_startShowTime - showLimit;
            if (d > 0)
            {
                visible = false;
            }
            else
            {
                Timers.inst.Add(-d, 1, CloseHandler);
            }
        }

        private void CloseHandler(object param)
        {
            visible = false;
        }

        private void ResetClose()
        {
            m_startShowTime = Time.realtimeSinceStartup;
            Timers.inst.Remove(CloseHandler);
        }
    }
}