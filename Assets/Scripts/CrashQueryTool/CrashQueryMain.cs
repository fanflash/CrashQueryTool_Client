using System;
using System.IO;
using CrashQuery.Data;
using CrashQuery.UI.Main;
using FairyGUI;
using FairyGUI.Utils;
using UnityEngine;

namespace CrashQuery
{
    public class CrashQueryMain : MonoBehaviour
    {
        public const string DefaultServer = "10.0.18.77:8082";
        
        public UIPanel Panel;
        public MainPanel m_mainPanel;

        private void Awake()
        {
            AppDao.Query.UseGetMethod = true;
            AppDao.SetRootUrl(GetServerIp());
            
            UIPackage.AddPackage("UI/Basic");
            MainBinder.BindAll();
            UIObjectFactory.SetPackageItemExtension(BaseSaveFileDialog.URL,typeof(SaveFileDialog));
            UIObjectFactory.SetPackageItemExtension(BaseMessageBox.URL, typeof(MessageBox));
            UIObjectFactory.SetPackageItemExtension(BaseLoginView.URL, typeof(BaseLoginView));
            UIObjectFactory.SetPackageItemExtension(BaseQueryView.URL, typeof(QueryView));
            UIObjectFactory.SetPackageItemExtension(BaseQueryInputView.URL, typeof(QueryInputView));
            UIObjectFactory.SetPackageItemExtension(BaseQueryResultView.URL, typeof(QueryResultView));
            UIObjectFactory.SetPackageItemExtension(BaseMainPanel.URL, typeof(MainPanel));
        }

        void Start()
        {
            m_mainPanel = Panel.ui as MainPanel;
        }

        private string GetServerIp()
        {
#if !UNITY_EDITOR && UNITY_WEBGL
            return "";
#else
            return "http://" + GetServerIpByConfig() + "/";
#endif
        }
        
        private string GetServerIpByConfig()
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "config.txt");
            if (!File.Exists(path))
            {
                return DefaultServer;
            }

            try
            {
                return File.ReadAllText(path);
            }
            catch (Exception e)
            {
                Debug.LogWarning(e);
                return DefaultServer;
            }
        }
    }
    


    public class MainPanel:BaseMainPanel
    {
        private new QueryView m_queryView;
        
        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);
            
            m_queryView = base.m_queryView as QueryView;
            m_ctrlState.onChanged.Add(StageChangeHandler);
            m_loginView.m_btnEnter.onClick.Add(OnEnterBtnHandler);
        }

        private void StageChangeHandler(EventContext context)
        {
            if (m_ctrlState.selectedPage == "query")
            {
                m_queryView.RequestOption();
            }
        }

        private void OnEnterBtnHandler()
        {
            //TODO: 后面有时间再做用户验证和token这块。
            var userName = m_loginView.m_txtUserName.text;
            var password = m_loginView.m_txtPassword.text;
            if (userName == "admin" && password == "admin")
            {
                m_ctrlState.selectedPage = "query";
            }
            else
            {
                MessageBox.Error("Name or password is error", "Retry");
            }
        }
    }
    
}

