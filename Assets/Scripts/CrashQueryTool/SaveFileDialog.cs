// Author: gaofan
// Date:   2022.08.19
// Desc:

using System;
using System.IO;
using CrashQuery.UI.Main;
using FairyGUI;
using FairyGUI.Utils;

namespace CrashQuery
{
    public partial class SaveFileDialog:BaseSaveFileDialog
    {
        private static SaveFileDialog m_inst;
        
        public static void Show(Action<string> saveCallback, string defPath = "")
        {
            m_inst.ShowInner(saveCallback, defPath);
        }
        
        public static void Close()
        {
            m_inst.visible = false;
        }
    }
    
    public partial class SaveFileDialog
    {
        private Action<string>  m_saveCallback;
        
        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);
            m_inst = this;
            m_btnSave.onClick.Add(SaveHandler);
            m_btnCancel.onClick.Add(CancelHandler);
        }

        private void CancelHandler(EventContext context)
        {
            visible = false;
        }

        private void SaveHandler()
        {
            var callback = m_saveCallback;
            m_saveCallback = null;

            var path = m_txtPath.text.Trim();
            if (string.IsNullOrEmpty(path))
            {
                MessageBox.Error("Path can't be empty", "ok");
                return;
            }

            var file = Path.GetFileName(path);
            if (string.IsNullOrEmpty(file))
            {
                MessageBox.Error("File name can't be empty", "ok");
                return;
            }

            var folder = Path.GetDirectoryName(path);
            if (!Directory.Exists(folder))
            {
                MessageBox.Error("Directory doesn't exist", "ok");
                return;
            }
            
            if (callback != null)
            {
                try
                {
                    callback(path);
                }
                catch (Exception e)
                {
                    MessageBox.Error($"Error:{e.Message}", "ok");
                }
            }
            visible = false;
        }

        private void ShowInner(Action<string> saveCallback, string defPath)
        {
            if (defPath == null)
            {
                defPath = "";
            }
            
            visible = true;
            m_txtPath.text = defPath;
            m_saveCallback = saveCallback;
        }
    }
}