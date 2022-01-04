// Author: 
// Date:   2021.12.31
// Desc:

using System.Collections.Generic;
using System.Text;
using CrashQuery.Core;
using CrashQuery.Helper;

namespace CrashQuery.Data
{
    /// <summary>
    /// copy.download
    /// 地址 库名（解析）【path】
    /// </summary>
    public class ExportDao : Singleton<ExportDao>
    {
        private List<string> m_saveStack = new List<string>();

        private string[] m_splitStack;

        private Dictionary<string, MemStackFrame> m_memStackFrames = new Dictionary<string, MemStackFrame>();
        private List<string> m_parseLineStack = new List<string>();
        /// <summary>
        /// 存储源数据
        /// </summary>
        public void SaveSourceStack(string[] stack)
        {
            m_splitStack = stack;
        }

        public void CopyStackInfo()
        {
            SaveStackFrame();
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < m_splitStack.Length; i++)
            {
                m_parseLineStack.Clear();
                string line = m_splitStack[i];
                StackHelper.SplitLineWithPc(line, ref m_parseLineStack);
                StackHelper.SplitLineWithAt(line, ref m_parseLineStack);

                if (m_parseLineStack.Count > 2)
                {
                    builder.Append(m_parseLineStack[0]);
                    ExchangeLibData(m_parseLineStack[1], m_parseLineStack[2], ref builder);
                }
                else
                {
                    builder.Append(line);
                }

                builder.AppendLine();
            }

            UnityEngine.GUIUtility.systemCopyBuffer = builder.ToString();
        }
        
        private void ExchangeLibData(string lib, string address,  ref  StringBuilder builder)
        {
            var stackFrame = ExchangeAddressLibData(address);
            bool unknownLib = lib.StartsWith("split_config");
            var allStack = stackFrame.AllLibStack;
            if (allStack == null)
            {
                return;
            }

            builder.Append(address);
            for (int i = 0; i < allStack.Length; i++)
            {
                if (unknownLib)
                {
                    if (allStack[i].Method.Code != "??")
                    {
                        var ret = StackHelper.ConnectPcStack(allStack[i].Library, allStack[i].Method.Path, allStack[i].Method.Code);
                        builder.Append(ret);
                        break;
                    }
                }
                else
                {
                    if (allStack[i].Library == lib)
                    {
                        var ret = StackHelper.ConnectPcStack(lib, allStack[i].Method.Path, allStack[i].Method.Code);
                        builder.Append(ret);
                    }
                }
            }
        }
        
        


        private void SaveStackFrame()
        {
            var result = AppDao.Query.LastSuccessResult;
            var frames = new StackFrameInfo[result.Length];
            for (int i = 0; i < result.Length; i++)
            {
                if (!m_memStackFrames.TryGetValue(result[i].Address, out _))
                {
                    m_memStackFrames.Add(result[i].Address, result[i]);
                }
            }
        }

        private MemStackFrame ExchangeAddressLibData(string address)
        {
            if (m_memStackFrames.TryGetValue(address, out var data))
            {
                return data;
            }

            return new MemStackFrame();
        }
        protected override void OnDispose()
        {
            m_saveStack.Clear();
        }
    }

    internal class StackFrameInfo
    {
    }
}