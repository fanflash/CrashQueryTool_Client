// Author: gaofan
// Date:   2021.12.25
// Desc:

using System;
using System.Collections.Generic;
using System.Text;
using CrashQuery.Core;
using UnityEngine.Networking;

namespace CrashQuery.Data
{
    public class QueryDao
    {
        public bool UseGetMethod = false;
        public MemStackFrame[] LastSuccessResult { get; private set; }
        public GEvent.Proxy OnUpdate => m_onUpdate.Interface;

        private GEvent m_onUpdate = new GEvent();
        private AppDao.Context m_context;
        private string m_apiUrl;
        
        public QueryDao(AppDao.Context context)
        {
            m_context = context;
            LastSuccessResult = Array.Empty<MemStackFrame>();
        }

        public int Request(QueryRequest param, Action<ReqResult<QueryResult>> callback = null)
        {
            if (m_apiUrl == null)
            {
                m_apiUrl = $"{m_context.RootUrl}/query";
            }

            UnityWebRequest request;
            if (UseGetMethod)
            {
                var bytes = UnityWebRequest.SerializeSimpleForm(param.ToDict());
                var queryStr = Encoding.UTF8.GetString(bytes);
                request = UnityWebRequest.Get($"{m_apiUrl}?{queryStr}");
            }
            else
            {
                //Unity的post有BUG，传给服务器的Content-Lenght为0，导致服务器取不出body的数据，所以目前只能用Get
                request = UnityWebRequest.Post(m_apiUrl, param.ToDict());
            }
            
            var reqItem = new ReqItem<QueryResult>(request.SendWebRequest());
            reqItem.OnComplete.Add(callback);
            reqItem.OnComplete.Add(OnCompleteHandler);
            return reqItem.ReqId;
        }

        private void OnCompleteHandler(ReqResult<QueryResult> obj)
        {
            if (!obj.Error.HasErr && obj.Data.State == 2)
            {
                LastSuccessResult = obj.Data.Data;
                m_onUpdate.Do();
            }
        }
    }
    
    public struct QueryRequest
    {
        public string EditorVersion;
        public string CpuType;
        public string Group;
        public string Symbol;
        public bool IsDev;
        public string Stack;
        public bool IsSync;
        public string Token;

        public Dictionary<string, string> ToDict()
        {
            var t = new Dictionary<string, string>(7);
            t["editor"] = EditorVersion;
            t["cpuType"] = CpuType;
            t["group"] = Group;
            t["symbol"] = Symbol;
            t["stack"] = Stack;
            t["token"] = Token;
            if (IsSync)
            {
                t["sync"] = "1";
            }

            if (IsDev)
            {
                t["dev"] = "1";
            }

            return t;
        }
    }
    
    [Serializable]
    public struct QueryResult
    {
        public int Id;
        public int State;
        public MemStackFrame[] Data;
    }

    [Serializable]
    public struct MemStackFrame
    {
        public string Address;
        public StackFrame[] AllLibStack;
    }

    [Serializable]
    public struct StackFrame
    {
        public string Library;
        public MethodVo Method;
    }

    [Serializable]
    public struct MethodVo
    {
        public string Code;
        public string Path;
    }
}