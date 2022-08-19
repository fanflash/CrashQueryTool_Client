// Author: gaofan
// Date:   2022.08.18
// Desc:

using System;
using System.IO;
using CrashQuery.Core;
using CrashQuery.Data;
using UnityEngine;

namespace CrashQuery.Helper
{
    public static class ExportHelper
    {
        private static Csv<CallStackFrame> g_exportCsv = new Csv<CallStackFrame>();

        public static string ExportCsv(MemStackFrame[] data)
        {
            for (int i = 0; i < data.Length; i++)
            {
                var frame = data[i];
                var isFirst = true;
                for (int j = 0; j < frame.AllLibStack.Length; j++)
                {
                    var libStack = frame.AllLibStack[j];
                    if (string.IsNullOrEmpty(libStack.Method.Code) || libStack.Method.Code == "??")
                    {
                        continue;
                    }

                    var row = new CallStackFrame();
                    if (isFirst)
                    {
                        row.Address = frame.Address;
                        isFirst = false;
                    }

                    row.Library = libStack.Library;
                    row.Method = libStack.Method.Code;
                    row.Path = libStack.Method.Path;
                    g_exportCsv.Add(row);
                }

                if (isFirst)
                {
                    //说明一个有内容的都没有
                    var row = new CallStackFrame();
                    row.Address = frame.Address;
                    row.Library = "??";
                    row.Method = "??";
                    row.Path = "??:0";
                    g_exportCsv.Add(row);
                }
            }

            var csvStr = g_exportCsv.ToString();
            g_exportCsv.Clear();
            return csvStr;
        }

        public static void ExportToFile(MemStackFrame[] data)
        {
            if (data.Length < 1)
            {
                MessageBox.Error("No data", "ok");
                return;
            }

            var csvStr = ExportCsv(data);
            var csvName = DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss") + ".csv";
            
#if UNITY_STANDALONE_WIN || UNITY_EDITOR
            var path = Path.Combine(Directory.GetCurrentDirectory(), csvName);
            SaveFileDialog.Show(p =>File.WriteAllText(path, csvStr), path);
#elif UNITY_WEBGL
            WebGLCopyAndPaste.SaveFile(csvStr,  csvName);
#else
            MessageBox.Show("Not support");
#endif
        }

        public static void ExportToClipboard(MemStackFrame[] data)
        {
            if (data.Length < 1)
            {
                MessageBox.Error("No data", "ok");
                return;
            }
            
            var csvStr = ExportCsv(data);
            GUIUtility.systemCopyBuffer = csvStr;
        }

        public struct CallStackFrame
        {
            [CsvHeader(0, "")]
            public string Address;
            
            [CsvHeader(1, "??")]
            public string Library;
            
            [CsvHeader(2, "??")]
            public string Method;
            
            [CsvHeader(3, "??:0")]
            public string Path;
        }
    }
}