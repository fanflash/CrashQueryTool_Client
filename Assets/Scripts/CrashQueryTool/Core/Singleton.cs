// Author: gaofan
// Date:   2021.12.25
// Desc:

using System;

namespace CrashQuery.Core
{
    public partial class Singleton<T>
    {
        private static T g_inst;

        /// <summary>
        /// 是否存在
        /// </summary>
        public static bool Exist => g_inst != null;

        public static T Inst
        {
            get
            {
                if (g_inst == null)
                {
                    g_inst = new T();
                }

                g_inst.OnUse();
                return g_inst;
            }
        }

        public static bool DisposeInst()
        {
            if (g_inst == null)
            {
                return true;
            }

            return g_inst.Dispose();
        }
    }

    /// <summary>
    /// Author  gaofan
    /// Date    2017.12.6
    /// Desc    单例类基类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract partial class Singleton<T> : IDisposer where T : Singleton<T>, new()
    {
        private bool m_disposed;

        protected Singleton()
        {
            if (g_inst != null)
            {
#if UNITY_EDITOR
                throw new Exception(this + "是单例类，不能实例化两次");
#endif
            }
        }

        public bool Disposed => m_disposed;

        protected virtual void OnUse() {}

        /// <summary>
        /// 处理释放相关事情
        /// </summary>
        protected abstract void OnDispose();

        public bool Dispose()
        {
            if (m_disposed)
            {
                return false;
            }

            g_inst = default;
            m_disposed = true;
            OnDispose();
            return true;
        }
    }
}