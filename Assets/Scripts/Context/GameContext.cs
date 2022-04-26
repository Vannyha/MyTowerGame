using System.Collections.Generic;
using System.Linq;
using Enemy;
using Game;
using Modifiers;
using Tower;
using TowerModules;
using UIManagers.BottomPanel;
using UIManagers.LaboratoryScreen;
using UIManagers.MainScreen;
using UIManagers.ResultsPanel;
using UIManagers.ScreenStats;
using UIManagers.ShopScreen;
using UIManagers.WorkshopScreen;
using UnityEngine;

namespace Context
{
    public class GameContext : MonoBehaviour
    {
        [SerializeField] private GameManager gameManager;
        [SerializeField] private TowerManager towerManager;
        [SerializeField] private EnemyManager enemyManager;
        [SerializeField] private ModifierManager modifierManager;
        [SerializeField] private TowerModulesManager towerModulesManager;
        [SerializeField] private ScreenStatsUIManager screenStatsUIManager;
        [SerializeField] private MainScreenUIManager mainScreenUIManager;
        [SerializeField] private WorkshopScreenUIManager workshopScreenUIManager;
        [SerializeField] private ShopScreenUIManager shopScreenUIManager;
        [SerializeField] private ResultsPanelUIManager resultsPanelUIManager;
        [SerializeField] private LaboratoryScreenUIManager laboratoryScreenUIManager;
        [SerializeField] private BottomPanelUIManager bottomPanelUIManager;

        public ITowerManager TowerManagerInstance => towerManager;
        public IEnemyManager EnemyManagerInstance => enemyManager;
        public IGameManager GameManagerInstance => gameManager;
        public IScreenStatsUIManager ScreenStatsUIManagerInstance => screenStatsUIManager;
        public IMainScreenUIManager MainScreenUIManagerInstance => mainScreenUIManager;
        public ITowerModulesManager TowerModulesManagerInstance => towerModulesManager;
        public IModifierManager ModifierManagerInstance => modifierManager;
        public IWorkshopScreenUIManager WorkshopScreenUIManagerInstance => workshopScreenUIManager;
        public IShopScreenUIManager ShopScreenUIManagerInstance => shopScreenUIManager;
        public IResultsPanelUIManager ResultsPanelUIManagerInstance => resultsPanelUIManager;
        public ILaboratoryScreenUIManager LaboratoryScreenUIManagerInstance => laboratoryScreenUIManager;
        public IBottomPanelUIManager BottomPanelUIManagerInstance => bottomPanelUIManager;

        public void Awake()
        {
            IEnumerable<GameObject> objects = FindObjectsOfType<GameObject>(true).Where(go => go.TryGetComponent<IBean>(out _));
            objects.Select(go => go.GetComponent<IBean>()).ToList().ForEach(bean => bean.SetupBeans(this));
            objects.ToList().ForEach(obj => obj.SetActive(true));
        }
    }
}