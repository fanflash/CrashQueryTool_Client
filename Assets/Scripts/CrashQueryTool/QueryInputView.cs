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
        private GListExt<SymbolVo,GButton> m_listApkExt;
        private SymbolVo m_selectApk;

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);
            m_listApkExt = new GListExt<SymbolVo, GButton>(m_listApk, ItemRenderer);
            m_cbEditorVer.onChanged.Add(UpdateSymbol);
            m_listApk.onClickItem.Add(ApkListClickItemHandler);
            var filterInput = m_txtFilter.GetChild("title") as GTextInput;
            filterInput?.onChanged.Add(UpdateSymbol);
            m_btnQuery.onClick.Add(OnClickQueryHandler);
        }

        private void OnClickQueryHandler(EventContext context)
        {
            var param = new QueryRequest();
            param.EditorVersion = m_cbEditorVer.text;
            param.Group = m_selectApk.Group;
            param.Symbol = m_selectApk.Symbol;
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
            if (string.IsNullOrEmpty(param.Symbol) ||
                string.IsNullOrEmpty(param.Group))
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
            
            //缩短到完全可以显示
            var nameSb = SbPool.Get();
            nameSb.Append(m_selectApk.Symbol);
            
            var titleSb = SbPool.Get();
            var prefix = titleSb.Append('[').Append(m_selectApk.Group).Append("] ").ToString();
            titleSb.Append(nameSb);
            
            for (int i = 0; i < m_selectApk.Name.Length; i++)
            {
                m_txtSelectApk.text = titleSb.ToString();
                titleSb.Clear();
                if (nameSb.Length < 4 ||
                    m_txtSelectApk.textWidth < m_txtSelectApk.width)
                {
                    break;
                }
                nameSb.Remove(0, 3);
                titleSb.Append(prefix).Append("...").Append(nameSb);
            }
            SbPool.Put(nameSb);
            SbPool.Put(titleSb);
        }

        private void ItemRenderer(int index, SymbolVo itemData, GButton item, bool isSelect)
        {
            item.title = itemData.Name;
            item.data = itemData;
        }

        public void UpdateOption()
        {
            var editors = AppDao.Option.Editors;
            var names = new string[editors.Length];
            for (int i = 0; i < editors.Length; i++)
            {
                names[i] = editors[i].Editor;
            }
            m_cbEditorVer.items = names;
            m_cbEditorVer.values = names;

            var cpuTypes = new[]
            {
                "armeabi-v7a",
                "arm64-v8a",
            };
            m_cbCputype.items = cpuTypes;
            UpdateSymbol();
            m_txtSelectApk.text = "none";
            m_selectApk = null;
        }
        
        public void UpdateSymbol()
        {
            var editorVo = AppDao.Option.GetEditorByVer(m_cbEditorVer.value);
            if (editorVo == null)
            {
                m_listApkExt.Data = null;
                return;
            }
            
            if (editorVo.Symbols == null)
            {
                editorVo.InitSymbols();
            }
            
            m_cacheFilterList.Clear();
            if (!string.IsNullOrEmpty(m_txtFilter.text))
            {
                var filterStr = m_txtFilter.text;
                var filters = m_cacheFilterList;
                for (int i = 0; i < editorVo.Symbols.Length; i++)
                {
                    var s = editorVo.Symbols[i];
                    if (s.Name.Contains(filterStr))
                    {
                        filters.Add(s);
                    }
                }
                m_listApkExt.Data = filters;
            }
            else
            {
                m_listApkExt.Data = editorVo.Symbols;
            }
        }

        private List<SymbolVo> m_cacheFilterList = new List<SymbolVo>();
    }
}