// Author: gaofan
// Date:   2021.12.25
// Desc:   选项数据访问接口

using System;
using System.Collections.Generic;
using CrashQuery.Core;
using UnityEngine.Networking;

namespace CrashQuery.Data
{
    public class OptionDao
    {
        public GEvent.Proxy OnUpdate => m_onUpdate.Interface;
        public EditorSymbolVo[] Editors;

        private GEvent m_onUpdate = new GEvent();
        
        private AppDao.Context m_context;
        private string m_apiUrl;
        private ReqItem<OptionResult> m_reqItem;

        public OptionDao(AppDao.Context context)
        {
            m_context = context;
            Editors = Array.Empty<EditorSymbolVo>();
        }

        public EditorSymbolVo GetEditorByVer(string editorVersion)
        {
            if (Editors.Length < 1)
            {
                return null;
            }

            for (int i = 0; i < Editors.Length; i++)
            {
                var d = Editors[i];
                if (d.Editor == editorVersion)
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
                if (obj.Data != null && obj.Data.Editors != null)
                {
                    Editors = obj.Data.Editors;
                }
                else
                {
                    Editors = Array.Empty<EditorSymbolVo>();
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
        public EditorSymbolVo[] Editors;
        public int BuildId;
    }

    [Serializable]
    public class EditorSymbolVo
    {
        public string Editor;
        public string[] Apk;
        public SymbolGroupVo[] Groups;
        
        //这个是非服务器发来的序列化数据对象
        [NonSerialized]
        public SymbolVo[] Symbols;

        public void InitSymbols()
        {
            if (Symbols != null)
            {
                return;
            }

            if (Groups == null)
            {
                return;
            }

            var sb = SbPool.Get();
            var symbols = new List<SymbolVo>();
            for (int i = 0; i < Groups.Length; i++)
            {
                var g = Groups[i];
                for (int j = 0; j < g.Symbols.Length; j++)
                {
                    var s = new SymbolVo();
                    s.Group = g.Name;
                    s.Symbol = g.Symbols[j];
                    
                    sb.Clear();
                    sb.Append('[').Append(s.Group).Append("] ").Append(s.Symbol);
                    s.Name = sb.ToString();
                    symbols.Add(s);
                }
            }
            Symbols = symbols.ToArray();
            SbPool.Put(sb);
        }
    }

    [Serializable]
    public class SymbolGroupVo
    {
        public string Name;
        public string[] Symbols;
    }

    public class SymbolVo
    {
        public string Name;
        public string Group;
        public string Symbol;
    }
}