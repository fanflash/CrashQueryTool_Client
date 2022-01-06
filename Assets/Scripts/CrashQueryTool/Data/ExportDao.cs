// Author: 
// Date:   2021.12.31
// Desc:

using System.Collections.Generic;
using System.Text;
using CrashQuery.Core;
using CrashQuery.Helper;
using IGG.Framework.Config;
using UnityEngine;

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
        private StringBuilder m_builder = new StringBuilder();
        private List<string[]> m_listToCsv = new List<string[]>();
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
            m_builder.Clear();
            for (int i = 0; i < m_splitStack.Length; i++)
            {
                m_parseLineStack.Clear();
                string line = m_splitStack[i];
                StackHelper.SplitLineWithPc(line, ref m_parseLineStack);
                StackHelper.SplitLineWithAt(line, ref m_parseLineStack);

                if (m_parseLineStack.Count > 2)
                {
                    m_builder.Append(m_parseLineStack[0]);
                    ExchangeLibData(m_parseLineStack[1], m_parseLineStack[2], ref m_builder);
                }
                else
                {
                    m_builder.Append(line);
                }

                m_builder.AppendLine();
            }

            UnityEngine.GUIUtility.systemCopyBuffer = m_builder.ToString();
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
            m_memStackFrames.Clear();
            var result = AppDao.Query.LastSuccessResult;
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
        
        /// <summary>
        /// Address Lib Method Path
        /// </summary>
        public void OnSaveFile()
        {
            SaveStackFrame();
            m_listToCsv.Clear();
            string[] csv = new[] {"Address","Lib","Method","Path" };
            m_listToCsv.Add(csv);
            int startLine = int.MaxValue;
            for (int i = 0; i < m_splitStack.Length; i++)
            {
                string line = m_splitStack[i];
                if (line.StartsWith("backtrace"))
                {
                    startLine = i;
                }

                if (i <= startLine)
                {
                    continue;
                }
                m_parseLineStack.Clear();
                StackHelper.SplitLineWithPc(line, ref m_parseLineStack);
                StackHelper.SplitLineWithAt(line, ref m_parseLineStack);

                if (m_parseLineStack.Count > 2)
                {
                    ExchangeLibData2(m_parseLineStack[1], m_parseLineStack[2], ref m_listToCsv);
                }
            }

            var csvStr = CsvHelper.ListToCsvStr(m_listToCsv);
            var date = System.DateTime.Now;
            WebGLCopyAndPaste.SaveFile(csvStr, date.ToString("yyyy-MM-dd-HH-mm-ss"));

        }
        private void ExchangeLibData2(string lib, string address,  ref  List<string[]> builder)
        {
            var stackFrame = ExchangeAddressLibData(address);
            bool unknownLib = lib.StartsWith("split_config") || string.IsNullOrEmpty(lib);
            var allStack = stackFrame.AllLibStack;
            if (allStack == null)
            {
                return;
            }

            string[] csv = new string [4];
            for (int i = 0; i < allStack.Length; i++)
            {
                if (unknownLib)
                {
                    if (allStack[i].Method.Code != "??")
                    {
                        csv[0] = address;
                        csv[1] = allStack[i].Library;
                        csv[2] = allStack[i].Method.Code;
                        csv[3] = allStack[i].Method.Path;
                        builder.Add(csv);
                        break;
                    }
                }
                else
                {
                    if (allStack[i].Library == lib)
                    {
                        csv[0] = address;
                        csv[1] = lib;
                        csv[2] = allStack[i].Method.Code;
                        csv[3] = allStack[i].Method.Path;
                        builder.Add(csv);
                    }
                }
            }
        }
    }
}