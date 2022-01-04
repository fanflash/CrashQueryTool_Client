// Author: 
// Date:   2022.01.04
// Desc:

using System.Collections.Generic;

namespace CrashQuery.Helper
{
    public static class StackHelper
    {
        public static void SplitLineWithAt(string line, ref List<string> data)
        {
           
            var atIdx = line.IndexOf("at ");
            if (atIdx != -1)
            {
                data.Add("at ");
                string[] str = line.Split(' ');
                for (int i = 0; i < str.Length; i++)
                {
                    var dotIdx = str[i].IndexOf('.');
                    if (dotIdx > 0)
                    {
                        string address = string.Empty;
                        var idx = str[i].IndexOf('(');
                        if (dotIdx > 0 && idx > 0)
                        {
                            address = str[i].Substring(dotIdx+1, idx - dotIdx-1);
                        }

                        var lib = str[i].Substring(0, dotIdx);
                        data.Add(lib);
                        data.Add(address);
                    }
                }
            }
        }

        public static void SplitLineWithPc(string line, ref List<string> data)
        {
            var pcIdx = line.IndexOf(" pc");
            if (pcIdx != -1)
            {
                //搜索以.so结尾的文本段
                var sos = line.Split(' ');
                if (sos.Length > 0)
                {
                    string address = string.Empty;
                    string lib = string.Empty;
                    for (int i = 0; i < sos.Length; i++)
                    {
                        if (sos[i].StartsWith("pc"))
                        {
                            address = sos[i + 1];
                        }
                        if (sos[i].EndsWith(".so"))
                        {
                            //只取最后.so对应的
                            lib = sos[i];
                            var libs = lib.Split('/');
                            if (libs.Length > 1)
                            {
                                lib = libs[libs.Length - 1];
                            }

                            if (lib.Length > 3)
                            {
                                lib = lib.Substring(0, lib.Length - 3);
                            }
                            
                            break;
                        }
                    }
                    data.Add("pc ");
                    data.Add(lib);
                    data.Add(address);
                }
            }
        }

        /// <summary>
        /// 地址 库名（解析）【path】
        /// </summary>
        public static string ConnectPcStack(string lib, string path, string code)
        {
            string formatStr = " {0}({1})[{2}]";
            var ret = string.Format(formatStr, lib, code, path);
            return ret;
        }
    }
}