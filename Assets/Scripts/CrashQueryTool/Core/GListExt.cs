// Author: 
// Date:   2021.12.25
// Desc:

using System.Collections;
using System.Collections.Generic;
using FairyGUI;
using UnityEngine;

namespace CrashQuery.Core
{
   /// <summary>
    /// GList扩展，可以方便设置数据
    /// @author gaofan
    /// </summary>
    /// <typeparam name="TData"></typeparam>
    /// <typeparam name="TItemRenderer"></typeparam>
    public class GListExt<TData, TItemRenderer> where TItemRenderer : GObject
    {
        /// <summary>
        /// 列表项渲染器
        /// </summary>
        /// <param name="index">数据的索引</param>
        /// <param name="itemData">数据项</param>
        /// <param name="item">显示对象</param>
        public delegate void ListItemRenderer(int index, TData itemData, TItemRenderer item, bool isSelect);

        /// <summary>
        /// 列表组件提供者
        /// </summary>
        /// <param name="index"></param>
        /// <param name="itemData"></param>
        /// <param name="isSelect"></param>
        public delegate string ListItemProvider(int index, TData itemData, bool isSelect);

        /// <summary>
        /// 列表元素被点击的事件
        /// </summary>
        /// <param name="itemData"></param>
        /// <param name="itemRender"></param>
        /// <returns></returns>
        public delegate void OnClickItemEvent(TData itemData, TItemRenderer itemRender);


        protected ListItemRenderer m_itemRenderer;
        protected ListItemProvider m_itemProvider;
        protected OnClickItemEvent m_onClickItem;

        protected GList m_owner;
        protected IList<TData> m_data;

        /// <summary>
        /// 用于帧来缓存SelectIndex;
        /// </summary>
        private int m_selectIndex;
        private int m_preFrameId = -1;

        /// <summary>
        /// 最小minNumChildren
        /// </summary>
        private int m_minNumChildren;

        private BitArray m_selections;
        
        public GList Owner => m_owner;

        /// <summary>
        /// 列表扩展
        /// </summary>
        /// <param name="owner">原例表</param>
        /// <param name="itemRenderer">列表中每项的渲染器</param>
        /// <param name="isVirtual">是否为虚拟列表</param>
        /// <param name="itemProvider">
        /// 如果是虚拟列表，
        /// 并且列表中的项是不同组件混合。
        /// 则可以用这个处理器，跟据每项的数据 ，返回不同的组件资源路径。
        /// </param>
        /// <param name="onClickItem">列表元素被点击的事件(注意不要轻易对同一个GList重复创建扩展GListExt,否侧onClickItem可能在点击时触发多次)</param>
        public GListExt(GList owner, ListItemRenderer itemRenderer = null, bool isVirtual = true,
            ListItemProvider itemProvider = null,
            OnClickItemEvent onClickItem = null)
        {
            m_selectIndex = -1;
            m_owner = owner;
            m_onClickItem = onClickItem;
            m_owner.onClickItem.Add(OnClickItemHandler);

            if (isVirtual)
            {
                m_owner.SetVirtual();
            }

            if (itemProvider != null)
            {
                SetItemProvider(itemProvider, false);
            }

            SetItemRenderer(itemRenderer, true);
        }

        /// <summary>
        /// 设置或获取列表项的数据
        /// </summary>
        public IList<TData> Data
        {
            set
            {
                m_data = value;
                Update();
            }
            get => m_data;
        }




        /// <summary>
        ///
        /// </summary>
        public int SelectIndex
        {
            set
            {
                if (m_selectIndex == value)
                {
                    return;
                }

                m_owner.selectedIndex = value;
                m_selectIndex = value;
                Update();
            }

            get
            {
                if (m_preFrameId != Time.frameCount)
                {
                    m_selectIndex = m_owner.selectedIndex;
                    m_preFrameId = Time.frameCount;
                }
                return m_selectIndex;
            }
        }

        public TData SelectItem
        {
            get
            {
                if (m_data == null)
                {
                    return default;
                }

                if (SelectIndex > -1 && SelectIndex < m_data.Count)
                {
                    return m_data[SelectIndex];
                }
                else
                {
                    return default;
                }
            }
        }

        /// <summary>
        /// 当List数据有更新时，可以调用这个来更新显示列表
        /// </summary>
        public virtual void Update()
        {
            if (m_owner == null)
            {
                return;
            }
            
            if (m_itemProvider != null && !m_owner.isVirtual)
            {
                //因为在非虚拟列表的情况下，m_itemProvider只对增长部分的item执行
                //会造成非增长部分会一直使用之前的对象，造成表现有问题，所以强制清空一次
                m_owner.numItems = 0;
            }

            m_owner.numItems = m_data == null
                ? m_minNumChildren
                : Mathf.Max(m_data.Count, m_minNumChildren);
        }

        /// <summary>
        /// 设置itemProvider为列表的item提供者
        /// </summary>
        /// <param name="itemProvider"></param>
        /// <param name="update"></param>
        public void SetItemProvider(ListItemProvider itemProvider, bool update = true)
        {
            m_itemProvider = itemProvider;
            if (itemProvider == null)
            {
                m_owner.itemProvider = null;
            }
            else
            {
                m_owner.itemProvider = ItemProviderHandler;
            }

            if (update)
            {
                Update();
            }
        }

        protected virtual string ItemProviderHandler(int index)
        {
            if (m_itemProvider == null)
            {
                return null;
            }

            if (m_data == null || index < 0 || index >= m_data.Count)
            {
                return m_itemProvider(index, default, false);
            }

            return m_itemProvider(index, m_data[index], GetSelectionByIndex(index));
        }

        /// <summary>
        /// 设置列表项的渲染器
        /// 如果设置时已经有数据，会更新列表
        /// </summary>
        /// <param name="itemRenderer"></param>
        /// <param name="update"></param>
        public void SetItemRenderer(ListItemRenderer itemRenderer, bool update = true)
        {
            m_itemRenderer = itemRenderer;
            if (itemRenderer == null)
            {
                m_owner.itemRenderer = null;
            }
            else
            {
                m_owner.itemRenderer = ItemRendererHandler;
            }

            if (update)
            {
                Update();
            }
        }

        protected virtual void ItemRendererHandler(int index, GObject item)
        {
            if (m_itemRenderer == null)
            {
                return;
            }

            TItemRenderer renderer = item as TItemRenderer;
            if (renderer == null)
            {
                Debug.LogError($"ItemRender is not type:{typeof(TItemRenderer).Name},index:{index}");
                item.visible = false;
                return;
            }

            if (m_data == null || index < 0 || index >= m_data.Count)
            {
                m_itemRenderer(index, default, renderer, false);
                return;
            }

            m_itemRenderer(index, m_data[index], renderer, GetSelectionByIndex(index));
        }

        private void NullItemRenderer(int index, GObject item)
        {
        }

        protected bool m_isDisposed;

        public virtual void Dispose()
        {
            if (m_isDisposed)
            {
                return;
            }

            m_isDisposed = true;
            m_owner.onClickItem.Remove(OnClickItemHandler);
            m_owner.itemRenderer = null;
            m_owner = null;
            m_onClickItem = null;
            m_itemRenderer = null;
            m_data = null;
        }

        private void OnClickItemHandler(EventContext context)
        {
            var comp = context.data as GComponent;
            if (comp == null)
            {
                return;
            }

            var isMultipleSelectionMode = IsMultipleSelectionMode();
            var itemIndex = -1;
            if (isMultipleSelectionMode && comp is GButton button)
            {
                itemIndex = GetIndexByItem(button);
                SetSelection(itemIndex, button.selected);
            }

            if (m_onClickItem == null ||
                !(context.data is TItemRenderer itemRender))
            {
                return;
            }

            if (itemIndex < 0)
            {
                itemIndex = GetIndexByItem(comp);
            }

            if (m_data == null || itemIndex < 0 || itemIndex >= m_data.Count)
            {
                m_onClickItem(default, itemRender);
            }
            else
            {
                m_onClickItem(m_data[itemIndex], itemRender);
            }
        }

        private int GetIndexByItem(GObject item)
        {
            var childIndex = m_owner.GetChildIndex(item);
            return m_owner.ChildIndexToItemIndex(childIndex);
        }

        private bool IsMultipleSelectionMode()
        {
            return m_owner.selectionMode == ListSelectionMode.Multiple ||
                   m_owner.selectionMode == ListSelectionMode.Multiple_SingleClick;
        }

        public bool SetSelection(int index, bool selected)
        {
            if (!IsMultipleSelectionMode() || m_data == null ||
                index < 0 || index >= m_data.Count) 
            {
                return false;
            }

            if(m_selections == null)
            {
                m_selections = new BitArray(m_data.Count);
            }

            if (m_selections.Length != m_data.Count)
            {
                m_selections.Length = m_data.Count;
            }

            m_selections.Set(index, selected);
            return true;
        }

        public bool GetSelectionByIndex(int index)
        {
            if (m_data == null || index < 0 || index >= m_data.Count)
            {
                return false;
            }

            if (IsMultipleSelectionMode())
            {
                if (m_selections == null || index >= m_selections.Length)
                {
                    return false;
                }

                return m_selections.Get(index);
            }

            return index == SelectIndex;
        }

        /// <summary>
        /// 设置数据 
        /// 主要为了 某些数据变化时根本不需要刷新界面用
        /// 重新修改数据 等到滑动到此数据时修改即可
        /// 节约全局刷新
        /// 只有数据的变化  没有长度变化 没有顺序变化 可用
        /// </summary>
        /// <param name="data"></param>
        public void VirtualSetRefreshData(IList<TData> data)
        {
            if (!Owner.isVirtual)
            {
                Debug.LogError("不是无限循环列表 不需要此方法 直接Data = ");
                return;
            }

            if (m_data.Count != data.Count)
            {
                //长度不同 无法使用此功能
                Data = data;
                return;
            }

            m_data = data;

        }

        /// <summary>
        /// 获取当前list显示的所有对象的数据
        /// 无限循环表 就是当前界面上显示的那几个的数据
        /// </summary>
        /// <param name="data"></param>
        public void VirtualGetShowData(ref List<TData> data)
        {
            if (!Owner.isVirtual)
            {
                Debug.LogError("不是无限循环列表 不需要此方法 你直接拿全Data不就好了");
                return;
            }

            var allChild = Owner.GetChildren();
            data.Clear();
            for (int i = 0; i < allChild.Length-1; i++)
            {
                var index = Owner.ChildIndexToItemIndex(i);
                if (index >= 0 && index <= m_data.Count-1)
                {
                    data.Add(m_data[index]);
                }            
            }

        }


        #region 对GList属性的直接转述
        public void ScrollTop(bool ani = false)
        {
            m_owner.scrollPane.ScrollTop(ani);
        }

        public void ScrollBottom(bool ani = false)
        {
            m_owner.scrollPane.ScrollBottom(ani);
        }

        #endregion
    }
}