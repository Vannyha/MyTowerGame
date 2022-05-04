using Context;
using Tower;
using TowerModules;
using TowerModules.Modules;
using UIManagers.BottomPanel;
using UIManagers.MainScreen;
using UIManagers.ResultsPanel;
using UIManagers.ScreenStats;
using UnityEngine;

namespace Game
{
    [Singleton]
    public class GameManager : MonoBehaviour, IGameManager
    {
        [Inject] private ITowerManager towerManager;
        [Inject] private IMainScreenUIManager mainScreenUIManager;
        [Inject] private IScreenStatsUIManager screenStatsUIManager;
        [Inject] private ITowerModulesManager towerModulesManager;
        [Inject] private IBottomPanelUIManager bottomPanelUIManager;
        [Inject] private IResultsPanelUIManager resultsPanelUIManager;

        private bool isGameStarted = false;

        public bool IsGameStarted => isGameStarted;

        public void Init()
        {
            bottomPanelUIManager.OpenMainPreset();
            bottomPanelUIManager.OpenPanel();
        }

        public void StartGame()
        {
            if (isGameStarted)
            {
                return;
            }

            if (!towerManager.IsAnyTowerChoosed)
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
            towerModulesManager.AddNewTowerModule(new TowerModuleContainer
            {
                TowerType = TowerModuleType.MachineGun, AttackSpeed = 1f, Damage = 5f, DotRange = 0.4f,
                AimingStrength = 1, ProjectileSpeed = 2
            });
        }

        public void StopGame()
        {
            isGameStarted = false;
            resultsPanelUIManager.OpenPanel();
            towerModulesManager.StopActions();
        }
    }
}