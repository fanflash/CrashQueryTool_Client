// Author: 
// Date:   2021.12.25
// Desc:

using CrashQuery.Data;
using CrashQuery.UI.Main;
using FairyGUI.Utils;

namespace CrashQuery
{
    public class QueryView:BaseQueryView
    {
        private new QueryInputView m_inputView;
        
        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);
            m_inputView = base.m_inputView as QueryInputView;
            AppDao.Option.OnUpdate.Add(UpdateHandler);
        }

        private void UpdateHandler()
        {
            m_inputView.UpdateOption();
        }

        public void RequestOption()
        {
            MessageBox.Show("Server connection...");
            AppDao.Option.Request(ReqHandler);
        }

        private void ReqHandler(ReqResult<OptionResult> obj)
        {
            if (obj.Error.HasErr)
            {
                MessageBox.Error(obj.Error.ToString(), "Retry", RequestOption);
            }
            else
            {
                MessageBox.Close();
            }
        }
    }
}