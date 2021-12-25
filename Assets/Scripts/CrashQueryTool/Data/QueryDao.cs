// Author: gaofan
// Date:   2021.12.25
// Desc:

using System;
using System.Collections.Generic;
using UnityEngine.Networking;

namespace CrashQuery.Data
{
    public class QueryDao
    {
        private AppDao.Context m_context;
        private string m_apiUrl;
        
        public QueryDao(AppDao.Context context)
        {
            m_context = context;
        }

        public int Request(QueryRequest param, Action<ReqResult<QueryResult>> callback = null)
        {
            if (m_apiUrl == null)
            {
                m_apiUrl = $"{m_context.RootUrl}/query";
            }
            
            var request = UnityWebRequest.Post(m_apiUrl,param.ToDict());
            var reqItem = new ReqItem<QueryResult>(request.SendWebRequest());
            reqItem.OnComplete.Add(callback);
            return reqItem.ReqId;
        }
    }
    
    public struct QueryRequest
    {
        public string EditorVersion;
        public string CpuType;
        public string Apk;
        public bool IsDev;
        public string Stack;
        public bool IsSync;
        public string Token;

        public Dictionary<string, string> ToDict()
        {
            var t = new Dictionary<string, string>(7);
            t["editor"] = EditorVersion;
            t["cpuType"] = CpuType;
            t["apk"] = Apk;
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