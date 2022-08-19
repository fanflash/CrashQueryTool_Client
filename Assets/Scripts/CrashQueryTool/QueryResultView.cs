// Author: gaofan
// Date:   2021.12.25
// Desc:

using System.Collections.Generic;
using CrashQuery.Core;
using CrashQuery.Data;
using CrashQuery.Helper;
using CrashQuery.UI.Main;
using FairyGUI;
using FairyGUI.Utils;

namespace CrashQuery
{
    public class QueryResultView:BaseQueryResultView
    {
        private GListExt<StackFrameInfo, BaseStackListItem> m_listResultEx;
        private GListExt<StackFrame, BaseDetailListItem> m_detailResEx;
        
        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);
            m_listResultEx = new GListExt<StackFrameInfo, BaseStackListItem>(m_listResult, ItemRenderer, true, null, OnClickItem);
            AppDao.Query.OnUpdate.Add(UpdateView);
            
            m_btnDown.onClick.Add(OnClickDownLoadHandle);

            m_detailResEx = new GListExt<StackFrame, BaseDetailListItem>(m_listDetail, ItemRenderer);
            m_btnCopy.onClick.Add(OnClickCopyHandle);
        }

        private void UpdateView()
        {
            var result = AppDao.Query.LastSuccessResult;
            var frames = new StackFrameInfo[result.Length];
            for (int i = 0; i < result.Length; i++)
            {
                var info = new StackFrameInfo();
                info.Frame = result[i];
                frames[i] = info;
            }
            m_listResultEx.Data = frames;
        }

        private void ItemRenderer(int index, StackFrameInfo itemData, BaseStackListItem item, bool isSelect)
        {
            int indexLib = 0;
            if (itemData.Libs == null)
            {
                itemData.Libs = new string[itemData.Frame.AllLibStack.Length];
                
                for (int i = 0; i < itemData.Frame.AllLibStack.Length; i++)
                {
                    var t = itemData.Frame.AllLibStack[i];
                    itemData.Libs[i] = t.Library;
                    if (t.Method.Code != "??")
                    {
                        indexLib = i;
                    }
                }
            }

            item.m_txtAddress.text = itemData.Frame.Address;
            item.m_cbLib.items = itemData.Libs;
            item.m_cbLib.values = itemData.Libs;
            
            if (string.IsNullOrEmpty(itemData.SelectLib))
            {
                itemData.SelectLib = itemData.Libs[indexLib];
            }

            item.m_ctrlSelLib.selectedPage = itemData.Libs.Length > 1 ? "enable" : "disable";
            item.data = itemData;
            UpdateItemByLib(item, itemData.SelectLib);
            item.m_cbLib.onChanged.Set(OnSelectHandler);
        }
        
        /// <summary>
        /// 选择
        /// </summary>
        /// <param name="itemdata"></param>
        /// <param name="itemrender"></param>
        private void OnClickItem(StackFrameInfo itemdata, BaseStackListItem itemrender)
        {
            m_detailResEx.Data = itemdata.Frame.AllLibStack;
            m_address.text = itemdata.Frame.Address;
        }
        
        
        private void ItemRenderer(int index, StackFrame itemdata, BaseDetailListItem item, bool isselect)
        {
            item.m_code.text = itemdata.Method.Code;
            item.m_path.text = itemdata.Method.Path;
            item.m_library.text = itemdata.Library;
            item.m_ctrlStyle.selectedIndex = index % 2;
        }

        private void OnSelectHandler(EventContext context)
        {
            var sender = context.sender as GComboBox;
            UpdateItemByLib(sender.parent as BaseStackListItem, sender.value);
        }

        private bool UpdateItemByLib(BaseStackListItem item, string lib)
        {
            var data = item.data as StackFrameInfo;
            if (data == null)
            {
                return false;
            }

            if (!data.FindFrameDataByLib(lib, out var frame))
            {
                frame = data.FirstFrame;
            }

            data.SelectLib = frame.Library;
            item.m_txtCode.text = frame.Method.Code;
            item.m_cbLib.value = frame.Library;
            item.m_txtLibName.text = frame.Library;
            return true;
        }

        /// <summary>
        /// 下载转为csv
        /// 文本内容会结合原来是数据一起输出
        /// </summary>
        /// <param name="context">转csv输出</param>
        private void OnClickDownLoadHandle(EventContext context)
        {
            ExportHelper.ExportCsv(AppDao.Query.LastSuccessResult);
        }
        
        /// <summary>
        /// 拷贝 
        /// </summary>
        /// <param name="context"></param>
        private void OnClickCopyHandle(EventContext context)
        {
            ExportHelper.ExportToClipboard(AppDao.Query.LastSuccessResult);
        }

        class StackFrameInfo
        {
            public MemStackFrame Frame;
            public string[] Libs;
            public string SelectLib;

            public StackFrame FirstFrame => Frame.AllLibStack[0];
            private Dictionary<string, StackFrame> m_libMap;

            public bool FindFrameDataByLib(string libName, out StackFrame frame)
            {
                if (Frame.AllLibStack.Length < 1)
                {
                    frame = default;
                    return false;
                }

                if (Frame.AllLibStack.Length == 1)
                {
                    ref var item = ref Frame.AllLibStack[0];
                    if (item.Library == libName)
                    {
                        frame = item;
                        return true;
                    }

                    frame = default;
                    return false;
                }
                
                //多于一个建表
                if (m_libMap == null)
                {
                    m_libMap = new Dictionary<string, StackFrame>(Frame.AllLibStack.Length);
                    for (int i = 0; i < Frame.AllLibStack.Length; i++)
                    {
                        var stack = Frame.AllLibStack[i];
                        m_libMap[stack.Library] = stack;
                    }
                }

                return m_libMap.TryGetValue(libName, out frame);
            }

            public StackFrame GetSelectionLib()
            {
                if (FindFrameDataByLib(SelectLib, out var frame))
                {
                    return frame;
                }
                return Frame.AllLibStack[0];
            }
        }
    }
}