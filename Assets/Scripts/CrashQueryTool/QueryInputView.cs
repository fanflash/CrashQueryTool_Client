// Author: gaofan
// Date:   2021.12.25
// Desc:

using System.Collections.Generic;
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
        private string m_selectApk;

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);
            m_listApkExt = new GListExt<string, GButton>(m_listApk, ItemRenderer);
            m_cbEditorVer.onChanged.Add(UpdateApk);
            m_listApk.onClickItem.Add(ApkListClickItemHandler);
            var filterInput = m_txtFilter.GetChild("title") as GTextInput;
            filterInput?.onChanged.Add(UpdateApk);
            m_btnQuery.onClick.Add(OnClickQueryHandler);
        }

        private void OnClickQueryHandler(EventContext context)
        {
            var param = new QueryRequest();
            param.EditorVersion = m_cbEditorVer.text;
            param.Apk = m_selectApk;
            param.CpuType = m_cbCputype.text;
            param.Stack = m_txtCallStack.text;
            param.Token = "124";
            param.IsDev = m_cbDevelpment.selected;
            param.IsSync = true;
            
            //检查
            if (string.IsNullOrEmpty(param.EditorVersion))
            {
                MessageBox.Error("[Editor ver]cannot be null", "Ok");
                return;
            }
            if (string.IsNullOrEmpty(param.CpuType))
            {
                MessageBox.Error("[Cpu type]cannot be null", "Ok");
                return;
            }
            if (string.IsNullOrEmpty(param.Apk))
            {
                MessageBox.Error("[Select apk]cannot be null", "Ok");
                return;
            }
            if (string.IsNullOrEmpty(param.Stack))
            {
                MessageBox.Error("[Original call stack]cannot be null", "Ok");
                return;
            }
            
            var reqId = AppDao.Query.Request(param, QueryCompleteHandler);
            Debug.Log($"[Query]new request, reqId={reqId}");
            MessageBox.Show("Querying...");
        }

        private void QueryCompleteHandler(ReqResult<QueryResult> obj)
        {
            MessageBox.Close();
            if (obj.Error.HasErr)
            {
                MessageBox.Error(obj.Error.ToString(), "ok");
                Debug.Log($"[Query]Request error, reqId={obj.ReqId},");
                return;
            }
            Debug.Log($"[Query]Request success, reqId={obj.ReqId}, taskId={obj.Data.Id}, state={obj.Data.State}");
        }

        private void ApkListClickItemHandler(EventContext context)
        {
            m_selectApk = m_listApkExt.SelectItem;
            m_txtSelectApk.text = m_selectApk;
           
            //缩短到完全可以显示
            var sb = SbPool.Get();
            var t = m_selectApk;
            while (t.Length > 4 && m_txtSelectApk.textWidth > m_txtSelectApk.width)
            {
                t = t.Substring(3);
                sb.Append("...").Append(t);
                m_txtSelectApk.text = sb.ToString();
                sb.Clear();
            }
            SbPool.Put(sb);
        }

        private void ItemRenderer(int index, string itemData, GButton item, bool isSelect)
        {
            item.title = itemData;
        }

        public void UpdateOption()
        {
            var editors = AppDao.Option.Editors;
            var names = new string[editors.Length];
            for (int i = 0; i < editors.Length; i++)
            {
                names[i] = editors[i].EditorVersion;
            }
            m_cbEditorVer.items = names;
            m_cbEditorVer.values = names;

            var cpuTypes = new[]
            {
                "armeabi-v7a",
                "arm64-v8a",
            };
            m_cbCputype.items = cpuTypes;
            UpdateApk();
            m_txtSelectApk.text = "none";
            m_selectApk = null;
        }

        public void UpdateApk()
        {
            var editorVo = AppDao.Option.GetEditorByVer(m_cbEditorVer.value);
            if (editorVo == null)
            {
                m_listApkExt.Data = null;
            }
            else
            {
                if (!string.IsNullOrEmpty(m_txtFilter.text))
                {
                    var filterStr = m_txtFilter.text;
                    var filters = ListPool<string>.Get();
                    for (int i = 0; i < editorVo.Apk.Length; i++)
                    {
                        var apk = editorVo.Apk[i];
                        if (apk.Contains(filterStr))
                        {
                            filters.Add(apk);
                        }
                    }

                    m_listApkExt.Data = filters;
                }
                else
                {
                    m_listApkExt.Data = editorVo.Apk;
                }
            }
        }
    }
}