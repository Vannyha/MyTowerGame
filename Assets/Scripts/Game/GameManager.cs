using Context;
using Tower;
using TowerModules;
using UIManagers.BottomPanel;
using UIManagers.MainScreen;
using UIManagers.ResultsPanel;
using UIManagers.ScreenStats;
using UnityEngine;

namespace Game
{
    public class GameManager : MonoBehaviour, IGameManager
    {
        private ITowerManager towerManager;
        private IMainScreenUIManager mainScreenUIManager;
        private IScreenStatsUIManager screenStatsUIManager;
        private ITowerModulesManager towerModulesManager;
        private IBottomPanelUIManager bottomPanelUIManager;
        private IResultsPanelUIManager resultsPanelUIManager;

        private bool isGameStarted = false;

        public bool IsGameStarted => isGameStarted;

        public void SetupBeans(GameContext context)
        {
            towerManager = context.TowerManagerInstance;
            mainScreenUIManager = context.MainScreenUIManagerInstance;
            screenStatsUIManager = context.ScreenStatsUIManagerInstance;
            towerModulesManager = context.TowerModulesManagerInstance;
            bottomPanelUIManager = context.BottomPanelUIManagerInstance;
            resultsPanelUIManager = context.ResultsPanelUIManagerInstance;
            bottomPanelUIManager.OpenMainPreset();
            bottomPanelUIManager.OpenPanel();
        }

        public void StartGame()
        {
            if (isGameStarted)
            {
                return;
            }
            
            towerManager.SetupTowerOnGame();
            towerModulesManager.SetupModulesForTower();
            screenStatsUIManager.OpenPanel();
            mainScreenUIManager.ClosePanel();
            bottomPanelUIManager.ClosePanel();
            isGameStarted = true;
        }

        public void FinishGame()
        {
            resultsPanelUIManager.ClosePanel();
            bottomPanelUIManager.OpenPanel();
            bottomPanelUIManager.OpenMainPreset();
            screenStatsUIManager.ClosePanel();
        }

        public void StopGame()
        {
            isGameStarted = false;
            resultsPanelUIManager.OpenPanel();
        }
    }
}