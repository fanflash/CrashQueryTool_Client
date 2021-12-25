﻿using System;
using CrashQuery.UI.Main;
using FairyGUI;
using UnityEngine;

namespace CrashQuery
{
    public class CrashQueryMain : MonoBehaviour
    {
        public UIPanel Panel;
        public MainPanel m_mainPanel;

        private void Awake()
        {
            UIPackage.AddPackage("UI/Basic");
            MainBinder.BindAll();
        }

        void Start()
        {
            m_mainPanel = new MainPanel(Panel.ui as BaseMain);
        }
        
        void Update()
        {
        
        }
    }

    public class MainPanel
    {
        private BaseMain m_ui;
        public MainPanel(BaseMain ui)
        {
            m_ui = ui;
            ui.m_btnEnter.onClick.Add(OnEnterBtnHandler);
        }

        private void OnEnterBtnHandler()
        {
            m_ui.m_ctrlState.selectedPage = "query";
        }
    }
    
}
