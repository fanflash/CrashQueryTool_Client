// Author: gaofan
// Date:   2021.12.25
// Desc:   选项数据访问接口

using System;
using CrashQuery.Core;
using UnityEngine.Networking;

namespace CrashQuery.Data
{
    public class OptionDao
    {
        public GEvent.Proxy OnUpdate => m_onUpdate.Interface;
        public EditorVo[] Editors;

        private GEvent m_onUpdate = new GEvent();
        
        private AppDao.Context m_context;
        private string m_apiUrl;
        private ReqItem<OptionResult> m_reqItem;

        public OptionDao(AppDao.Context context)
        {
            m_context = context;
            Editors = Array.Empty<EditorVo>();
        }

        public EditorVo GetEditorByVer(string editorVersion)
        {
            if (Editors.Length < 1)
            {
                return null;
            }

            for (int i = 0; i < Editors.Length; i++)
            {
                var d = Editors[i];
                if (d.EditorVersion == editorVersion)
                {
                    return d;
                }
            }

            return null;
        }

        public void Request(Action<ReqResult<OptionResult>> callback = null)
        {
            if (m_apiUrl == null)
            {
                m_apiUrl = $"{m_context.RootUrl}/option";
            }
            
            if (m_reqItem != null)
            {
                m_reqItem.OnComplete.Add(callback);
                return;
            }
            
            var request = UnityWebRequest.Get(m_apiUrl);
            m_reqItem = new ReqItem<OptionResult>(request.SendWebRequest(), OnCompleteHandler);
            m_reqItem.OnComplete.Add(callback);
            m_reqItem.OnComplete.Add(OnCompleteAfterHandler);
        }

        private void OnCompleteHandler(ReqResult<OptionResult> obj)
        {
            if (!obj.Error.HasErr)
            {
                if (obj.Data != null && obj.Data.Items != null)
                {
                    Editors = obj.Data.Items;
                }
                else
                {
                    Editors = Array.Empty<EditorVo>();
                }
            }
            m_reqItem = null;
        }

        private void OnCompleteAfterHandler(ReqResult<OptionResult> obj)
        {
            m_onUpdate.Do();
        }
    }

    [Serializable]
    public class OptionResult
    {
        public EditorVo[] Items;
        public int BuildId;
    }

    [Serializable]
    public class EditorVo
    {
        public string EditorVersion;
        public string[] Apk;
    }
}