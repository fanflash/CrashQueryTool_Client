// Author: 
// Date:   2021.12.25
// Desc:

namespace CrashQuery.Data
{
    public static class AppDao
    {
        private static readonly Context g_context = new Context();
        public static readonly OptionDao Option = new OptionDao(g_context);
        public static readonly QueryDao Query = new QueryDao(g_context);

        public static void SetRootUrl(string url)
        {
            g_context.RootUrl = url;
        }

        public class Context
        {
            public string RootUrl;
        }
    }
}