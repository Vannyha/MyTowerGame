using Context;
using Tower;
using TowerModules;
using UIManagers.MainScreen;
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

        private bool isGameStarted = false;

        public bool IsGameStarted => isGameStarted;

        public void SetupBeans(GameContext context)
        {
            towerManager = context.TowerManagerInstance;
            mainScreenUIManager = context.MainScreenUIManagerInstance;
            screenStatsUIManager = context.ScreenStatsUIManagerInstance;
            towerModulesManager = context.TowerModulesManagerInstance;
            mainScreenUIManager.OpenPanel();
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
            isGameStarted = true;
        }

        public void StopGame()
        {
            isGameStarted = false;
        }
    }
}