using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using CrashQuery.Core;
using IGG.Framework.Utils;
using UnityEngine;

namespace IGG.Framework.Config
{
    /// <summary>
    /// excel帮助类
    /// author: gaofan
    /// </summary>
    public class CsvHelper
    {
        public static Csv<TRow> ReadCsv<TRow>(string csvPath, Encoding encoding = null) where TRow:new()
        {
            if (!File.Exists(csvPath))
            {
                return null;
            }

            if (encoding == null)
            {
                encoding = Encoding.UTF8;
            }

            try
            {
                string csvStr = File.ReadAllText(csvPath, encoding);
                var csv = new Csv<TRow>();
                csv.SetDataByCsv(csvStr);
                return csv;
            }
            catch (Exception e)
            {
                //Logger.LogError("read csv error, path={0}, e={1}{2}", csvPath, e.Message, e.StackTrace);
            }

            return null;
        }
        public static List<string[]> ReadCsv(string path, Encoding encoding = null)
        {
            if (!File.Exists(path))
            {
                return null;
            }

            if (encoding == null)
            {
                encoding = Encoding.UTF8;
            }

            try
            {
                string csvStr = File.ReadAllText(path, encoding);
                return CsvStrToList(csvStr);
            }
            catch (Exception e)
            {
               // Logger.LogError("read csv error, path={0}, e={1}{2}", path, e.Message, e.StackTrace);
            }

            return null;
        }

        /// <summary>
        /// csv文件转列表
        /// 快速正确而且代码易读的CSV读取器
        /// 百度出来的代码要么很复杂,要么没判断特殊字符串，还是自己写一套吧
        /// </summary>
        /// <param name="csvStr"></param>
        /// <returns></returns>
        public static List<string[]> CsvStrToList(string csvStr)
        {
            StringBuilder sb = SbPool.Get();
            List<string[]> excel = new List<string[]>();
            List<string> row = g_tmpList;
            //0=新格子开始，1=一般字串第二个以后, 2特殊字符串第二个以后
            int state = 0;
            //1=新格子，2=新行
            int action = 0;
            int len = csvStr.Length;
            for (int i = 0; i < len; i++)
            {
                char c = csvStr[i];
                switch (state)
                {
                    //新的单元格刚开始
                    case 0:
                        switch (FindKeyword(g_startKeys, csvStr, ref i))
                        {
                            case KeywordType.CellSpec:
                                state = 2;
                                break;
                            //刚开始也是有可能就结束的
                            case KeywordType.CellEnd:
                                action = 1;
                                break;

                            case KeywordType.NewLine:
                                action = 2;
                                break;

                            default:
                                sb.Append(c);
                                state = 1;
                                break;
                        }
                        break;

                    //一般字符串第二个开始
                    case 1:
                        switch (FindKeyword(g_generalKeys, csvStr, ref i))
                        {
                            case KeywordType.CellEnd:
                                action = 1;
                                break;

                            case KeywordType.NewLine:
                                action = 2;
                                break;

                            default:
                                sb.Append(c);
                                break;
                        }
                        break;

                    //特殊字符串第一个字符开始
                    case 2:
                        switch (FindKeyword(g_spacKeys, csvStr, ref i))
                        {
                            case KeywordType.CellSpecChar:
                                sb.Append("\"");
                                break;

                            case KeywordType.CellSpecEnd:
                                action = 1;
                                break;

                            case KeywordType.CellSpecNewLine:
                                action = 2;
                                break;

                            default:
                                sb.Append(c);
                                break;
                        }
                        break;

                    default:
                        break;
                }

                switch (action)
                {
                    case 1:
                        row.Add(sb.ToString());
                        sb.Remove(0, sb.Length);
                        state = 0;
                        break;

                    case 2:
                        row.Add(sb.ToString());
                        sb.Remove(0, sb.Length);
                        excel.Add(row.ToArray());
                        row.Clear();
                        state = 0;
                        break;
                }
                action = 0;
            }
            
            if (state != 0)
            {
                //最后一行，最后一格有内容，并且没有换行的情况
                row.Add(sb.ToString());
                sb.Remove(0, sb.Length);
                excel.Add(row.ToArray());
            }
            else if (row.Count > 0)
            {
                //最后一行，最后一格没内容，并且没有换行的情况
                row.Add(sb.ToString());
                sb.Remove(0, sb.Length);
                excel.Add(row.ToArray());
            }

            //回收资源
            row.Clear();
            SbPool.Put(sb);
            return excel;
        }

        /// <summary>
        /// 列表转csv文件
        /// </summary>
        /// <param name="csvList"></param>
        /// <returns></returns>
        public static string ListToCsvStr(List<string[]> csvList)
        {
            var sb = SbPool.Get();
            for (int i = 0; i < csvList.Count; i++)
            {
                var rowList = csvList[i];
                int redEnd = rowList.Length - 1;
                for (int j = 0; j < redEnd; j++)
                {
                    var value = ConvStrToCsvFormat(rowList[j]);
                    sb.Append(value).Append(g_cellEnd.Key);
                }
                sb.Append(ConvStrToCsvFormat(rowList[redEnd]))
                  .Append(g_newLine.Key);
            }

            if (sb.Length > g_newLine.KeyLen)
            {
                sb.Remove(sb.Length - g_newLine.KeyLen, g_newLine.KeyLen);
            }
            return SbPool.PutAndToStr(sb);
        }

        /// <summary>
        /// 格式化字符串为可以组成csv的合规文本
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ConvStrToCsvFormat(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return "";
            }

            //有引号，无论如何也算特殊字符了
            if (value.Contains("\""))
            {
                return $"\"{value.Replace("\"", "\"\"")}\"";
            }

            //有逗号或者有换行
            if (value.Contains(",") || value.Contains("\r") || value.Contains("\n"))
            {
                return $"\"{value}\"";
            }

            return value;
        }

        private static KeywordType FindKeyword(Keyword[] keywords, string str, ref int index)
        {
            int hasLen = str.Length - index;
            for (int i = 0; i < keywords.Length; i++)
            {
                Keyword keyword = keywords[i];
                if (hasLen < keyword.KeyLen)
                {
                    continue;
                }
                bool finded = true;
                for (int j = 0; j < keyword.KeyLen; j++)
                {
                    if (str[index + j] != keyword.Key[j])
                    {
                        finded = false;
                        break;
                    }
                }
                if (!finded)
                {
                    continue;
                }
                index += keyword.KeyLen - 1;
                return keyword.Type;
            }
            return KeywordType.None;
        }

        private static List<string> g_tmpList = new List<string>();

        private static readonly Keyword g_callSpec = new Keyword(KeywordType.CellSpec, "\"");
        private static readonly Keyword g_newLine = new Keyword(KeywordType.NewLine, "\r\n");
        private static readonly Keyword g_cellEnd = new Keyword(KeywordType.CellEnd, ",");
        private static readonly Keyword g_cellSpecChar = new Keyword(KeywordType.CellSpecChar, "\"\"");
        private static readonly Keyword g_cellSpecEnd = new Keyword(KeywordType.CellSpecEnd, "\",");
        private static readonly Keyword g_cellSpecNewLine = new Keyword(KeywordType.CellSpecNewLine, "\"\r\n");

        private static readonly Keyword[] g_startKeys = { g_callSpec, g_cellEnd, g_newLine };
        private static readonly Keyword[] g_generalKeys = { g_cellEnd, g_newLine };
        private static readonly Keyword[] g_spacKeys = { g_cellSpecChar, g_cellSpecEnd, g_cellSpecNewLine };

        private enum KeywordType
        {
            None,
            CellEnd,
            NewLine,
            CellSpec,
            CellSpecChar,
            CellSpecEnd,
            CellSpecNewLine,
        }

        private struct Keyword
        {
            public readonly KeywordType Type;
            public readonly string Key;
            public readonly int KeyLen;

            public Keyword(KeywordType type, string key)
            {
                Type = type;
                Key = key;
                KeyLen = key.Length;
            }
        }
    }
}
