// Author: gaofan
// Date:   2021.12.25
// Desc:

using CrashQuery.Core;
using CrashQuery.Data;
using CrashQuery.UI.Main;
using FairyGUI;
using FairyGUI.Utils;
using UnityEngine;

namespace CrashQuery
{
    public class QueryInputView:BaseQueryInputView
    {
        private GListExt<string,GButton> m_listApkExt;

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);
            m_listApkExt = new GListExt<string, GButton>(m_listApk, ItemRenderer);
            m_cbEditorVer.onChanged.Add(UpdateApk);
            m_listApk.onClickItem.Add(ApkListClickItemHandler);
            m_btnQuery.onClick.Add(OnClickQueryHandler);
        }

        private void OnClickQueryHandler(EventContext context)
        {
            var param = new QueryRequest();
            param.EditorVersion = m_cbEditorVer.text;
            param.Apk = m_txtApk.text;
            param.CpuType = m_cbCputype.text;
            param.Stack = m_txtCallStack.text;
            param.Token = "124";
            param.IsDev = false;
            param.IsSync = true;
            AppDao.Query.Request(param, QueryCompleteHanlder);
        }

        private void QueryCompleteHanlder(ReqResult<QueryResult> obj)
        {
            if (obj.Error.HasErr)
            {
                MessageBox.Error(obj.Error.ToString(), "ok");
                return;
            }
            
            Debug.Log(obj.ReqId);
        }

        private void ApkListClickItemHandler(EventContext context)
        {
            m_txtApk.text = m_listApkExt.SelectItem;
        }

        private void ItemRenderer(int index, string itemData, GButton item, bool isSelect)
        {
            item.title = itemData;
        }

        public void UpdateOption()
        {
            var data = AppDao.Option.Editors;
            var names = new string[data.Length];
            for (int i = 0; i < data.Length; i++)
            {
                names[i] = data[i].EditorVersion;
            }
            m_cbEditorVer.items = names;
            m_cbEditorVer.values = names;

            var cpuTypes = new string[]
            {
                "armeabi-v7a",
                "arm64-v8a",
            };
            m_cbCputype.items = cpuTypes;
            UpdateApk();
        }

        public void UpdateApk()
        {
            var editorVo = AppDao.Option.GetEditorByVer(m_cbEditorVer.value);
            if (editorVo == null)
            {
                m_txtApk.text = "";
                m_listApkExt.Data = null;
            }
            else
            {
                m_listApkExt.Data = editorVo.Apk;
            }
        }
    }
}