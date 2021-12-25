using System;

namespace CrashQuery.Core
{
 /// <summary>
    /// Author  gaofan
    /// Date    2021.12.25
    /// Desc    简易事件，主要是为了实现Add方法，方便平时使用，叫GEvent, 音为GameEvent, 主要是为了区分开Event
    /// </summary>
    public class GEvent<T>
    {
        private Action<T> m_action;
        private bool m_doing;

        /// <summary>
        /// 是否在执行中
        /// </summary>
        public bool Doing => m_doing;

        /// <summary>
        /// 是否有监听器
        /// </summary>
        public bool HasListener { get; private set; }

        /// <summary>
        /// 对外接口
        /// </summary>
        public Proxy Interface
        {
            get
            {
                if (m_proxy == null)
                {
                    m_proxy = new Proxy(this);
                }

                return m_proxy;
            }
        }

        private Proxy m_proxy;

        public void Do(T param)
        {
            if (m_action == null)
            {
                return;
            }
            
            m_doing = true;
            m_action(param);
            m_doing = false;
        }

        public void Add(Action<T> handler)
        {
            m_action -= handler;
            m_action += handler;
            HasListener = true;
        }

        public void Set(Action<T> handler)
        {
            m_action = null;
            m_action += handler;
            HasListener = true;
        }

        public void Remove(Action<T> handler)
        {
            m_action -= handler;
            HasListener = m_action == null;
        }

        public DoOnceAction<T> Clear()
        {
            var t = new DoOnceAction<T>(m_action);
            m_action = null;
            HasListener = false;
            return t;
        }

        public class Proxy
        {
            private GEvent<T> m_owner;
            
            /// <summary>
            /// 是否在运行
            /// </summary>
            public bool Doing => m_owner.Doing;

            /// <summary>
            /// 是否有监听器
            /// </summary>
            public bool HasListener => m_owner.HasListener;

            public Proxy(GEvent<T> owner)
            {
                m_owner = owner;
            }
            
            public void Add(Action<T> handler)
            {
                m_owner.Add(handler);
            }

            public void Set(Action<T> handler)
            {
                m_owner.Set(handler);
            }

            public void Remove(Action<T> handler)
            {
                m_owner.Remove(handler);
            }

            public void Clear()
            {
                m_owner.Clear();
            }
        }
        
    }
    
    /// <summary>
    /// 没有参数的GEvent
    /// </summary>
    public class GEvent
    {
        private Action m_action;
        private bool m_doing;

        /// <summary>
        /// 是否在执行中
        /// </summary>
        public bool Doing => m_doing;

        /// <summary>
        /// 是否有监听器
        /// </summary>
        public bool HasListener { get; private set; }

        /// <summary>
        /// 对外接口
        /// </summary>
        public Proxy Interface
        {
            get
            {
                if (m_proxy == null)
                {
                    m_proxy = new Proxy(this);
                }

                return m_proxy;
            }
        }

        private Proxy m_proxy;

        public void Do()
        {
            if (m_action == null)
            {
                return;
            }

            m_doing = true;
            m_action();
            m_doing = false;
        }

        public void Add(Action handler)
        {
            m_action -= handler;
            m_action += handler;
            HasListener = true;
        }

        public void Set(Action handler)
        {
            m_action = null;
            m_action += handler;
            HasListener = true;
        }

        public void Remove(Action handler)
        {
            m_action -= handler;
            HasListener = m_action == null;
        }

        public DoOnceAction Clear()
        {
            var t = new DoOnceAction(m_action);
            m_action = null;
            HasListener = false;
            return t;
        }

        public class Proxy
        {
            private GEvent m_owner;
            
            /// <summary>
            /// 是否在运行
            /// </summary>
            public bool Doing => m_owner.Doing;

            /// <summary>
            /// 是否有监听器
            /// </summary>
            public bool HasListener => m_owner.HasListener;

            public Proxy(GEvent owner)
            {
                m_owner = owner;
            }
            
            public void Add(Action handler)
            {
                m_owner.Add(handler);
            }

            public void Set(Action handler)
            {
                m_owner.Set(handler);
            }

            public void Remove(Action handler)
            {
                m_owner.Remove(handler);
            }

            public void Clear()
            {
                m_owner.Clear();
            }
        }
    }

    public struct GEventParam<TNewValue, TOldValue>
    {
        public TNewValue NewValue;
        public TOldValue OldValue;
    }

    public struct DoOnceAction<T>
    {
        private Action<T> m_action;

        public DoOnceAction(Action<T> action)
        {
            m_action = action;
        }

        public void Do(T param)
        {
            if (m_action == null)
            {
                return;
            }
            m_action(param);
            m_action = null;
        }
    }
    
    public struct DoOnceAction
    {
        private Action m_action;

        public DoOnceAction(Action action)
        {
            m_action = action;
        }

        public void Do()
        {
            if (m_action == null)
            {
                return;
            }
            m_action();
            m_action = null;
        }
    }
}