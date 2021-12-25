// Author: gaofan
// Date:   2021.12.25
// Desc: 核心请求数据

using System;
using CrashQuery.Core;
using UnityEngine;
using UnityEngine.Networking;

namespace CrashQuery.Data
{
    public class ReqItem<T>
    {
        private static int g_reqCount;
        
        public int ReqId { get; private set; }
        public GEvent<ReqResult<T>>.Proxy OnComplete => m_onComplete.Interface;
        public ReqResult<T> Result => m_result;
        public bool IsDone => m_isDone;
        
        private ReqResult<T> m_result;
        private bool m_isDone;
        private Action<ReqResult<T>> m_callback;
        private GEvent<ReqResult<T>> m_onComplete = new GEvent<ReqResult<T>>();
        private UnityWebRequestAsyncOperation m_option;

        public ReqItem(UnityWebRequestAsyncOperation option, Action<ReqResult<T>> callback = null)
        {
            ReqId = g_reqCount;
            g_reqCount++;
            
            m_callback = callback;
            m_option = option;
            option.completed += CompleteHandler;
        }

        public void Cancel(bool doCallback = false)
        {
            if (m_isDone)
            {
                return;
            }
            if (doCallback)
            {
                DoErrCallback(999, "cancel request");
            }
            else
            {
                m_isDone = true;
                m_result.Error.ErrId = 999;
                m_result.Error.Message = "cancel request";
                Clear();
            }
        }

        private void DoErrCallback(int errId, string msg)
        {
            m_result.Error.ErrId = errId;
            m_result.Error.Message = msg;
            DoCallback();
        }

        private void DoCallback()
        {
            m_result.ReqId = ReqId;
            var onceAction = m_onComplete.Clear();
            var callback = m_callback;
            
            //先缓存和沮理，避免在回调里又调用自己的事件
            Clear();
            
            if (callback != null)
            {
                callback(m_result);
            }
            onceAction.Do(m_result);
        }

        private void Clear()
        {
            m_callback = null;
            m_onComplete.Clear();
            m_option.completed -= CompleteHandler;
            m_option.webRequest.Dispose();
            m_option = null;
        }

        private void CompleteHandler(AsyncOperation obj)
        {
            m_isDone = true;
            var req = m_option.webRequest;
            if (!string.IsNullOrEmpty(req.error))
            {
                DoErrCallback(101, req.error);
                return;
            }
            
            if (req.isHttpError)
            {
                DoErrCallback(102, "http error");
                return;
            }
            
            if (req.isNetworkError)
            {
                DoErrCallback(103,"network error");
                return;
            }

            var data = req.downloadHandler.text;
            if (string.IsNullOrEmpty(data))
            {
                DoErrCallback(104,"data is empty");
                return;
            }

            if (m_result.Error.TryParse(data))
            {
                DoErrCallback(m_result.Error.ErrId, m_result.Error.Message);
                return;
            }

            try
            {
                var t = JsonUtility.FromJson<T>(data);
                m_result.Data = JsonUtility.FromJson<T>(data);
                DoCallback();
            }
            catch (Exception e)
            {
                DoErrCallback(105,e.Message);
            }
        }
    }
    
    public struct ReqResult<T>
    {
        public int ReqId;
        public T Data;
        public ReqError Error;
    }
    
    public struct ReqError
    {
        public int ErrId;
        public string Message;

        public bool HasErr => ErrId != 0 || !string.IsNullOrEmpty(Message);

        public bool TryParse(string data)
        {
            if (string.IsNullOrEmpty(data))
            {
                return false;
            }
            
            const string prefix = "$error:";
            if (!data.StartsWith(prefix))
            {
                return false;
            }

            var t = data.Substring(prefix.Length);
            var param = t.Split('|');
            int.TryParse(param[0], out ErrId);
            if (param.Length > 1)
            {
                Message = param[1];
            }

            if (Message == null)
            {
                Message = "";
            }

            return true;
        }

        public override string ToString()
        {
            return $"Error: {Message} (code {ErrId})";
        }
    }
}