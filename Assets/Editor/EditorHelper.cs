using System;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace CrashQuery.EditorTool
{
    public static class EditorHelper
    {
             /// <summary>
        /// 执行批处理文件
        /// </summary>
        /// <param name="path"></param>
        /// <param name="param"></param>
        /// <param name="openFolder"></param>
        public static void DoBat(string path, string param = null, string openFolder = null)
        {
            try
            {
                if (string.IsNullOrEmpty(param))
                {
                    Process.Start(GetProjPath(path));
                }
                else
                {
                    Process.Start(GetProjPath(path), param);
                }

                if (openFolder != null)
                {
                    OpenFileOrFolder(GetProjPath(openFolder));
                }
            }
            catch (Exception ex)
            {
                Debug.Log(ex.ToString());
            }
        }
        
        /// <summary>
        /// 打开文件或文件夹
        /// </summary>
        /// <param name="path"></param>
        public static void OpenFileOrFolder(string path)
        {
            Process.Start("explorer.exe", path.Replace("/", "\\"));
        }
        
        /// <summary>
        /// 得到项目绝对路径
        /// eg:
        /// GetProjPath("") //out: "E:/project/igg/col3/UnityProjectWithDll"
        /// GetProjPath("Assets") //out: "E:/project/igg/col3/UnityProjectWithDll/Assets"
        /// </summary>
        /// <returns></returns>
        public static string GetProjPath(string relativePath = "")
        {
            if (relativePath == null)
            {
                relativePath = "";
            }

            relativePath = relativePath.Trim();
            if (!string.IsNullOrEmpty(relativePath))
            {
                if (relativePath.Contains("\\"))
                {
                    relativePath = relativePath.Replace("\\", "/");
                }

                if (!relativePath.StartsWith("/"))
                {
                    relativePath = "/" + relativePath;
                }
            }
            
            string projFolder = Application.dataPath;
            return projFolder.Substring(0, projFolder.Length - 7) + relativePath;
        }
    }
}