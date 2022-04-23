﻿using Context;
using Game;
using Tower;
using UnityEngine;
using UnityEngine.UI;

namespace UIManagers.ScreenStats
{
    public class ScreenStatsUIManager : MonoBehaviour, IScreenStatsUIManager
    {
        [SerializeField] private GameObject gameObjectPanel;
        [SerializeField] private Text hpText;
        
        private ITowerManager towerManager;
        private IGameManager gameManager;
        public void SetupBeans(GameContext context)
        {
            towerManager = context.TowerManagerInstance;
            gameManager = context.GameManagerInstance;
        }
        
        public void OpenPanel()
        {
            gameObjectPanel.SetActive(true);
        }

        public void ClosePanel()
        {
            gameObjectPanel.SetActive(false);
        }

        private void Update()
        {
            if (!gameManager.IsGameStarted)
            {
                return;
            }
            
            hpText.text = towerManager.CurrentTowerHp.ToString();
        }
    }
}