using System.Collections.Generic;
using System.Linq;
using Enemy;
using Game;
using Modifiers;
using Tower;
using TowerModules;
using UIManagers.MainScreen;
using UIManagers.ScreenStats;
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

        public ITowerManager TowerManagerInstance => towerManager;
        public IEnemyManager EnemyManagerInstance => enemyManager;
        public IGameManager GameManagerInstance => gameManager;
        public IScreenStatsUIManager ScreenStatsUIManagerInstance => screenStatsUIManager;
        public IMainScreenUIManager MainScreenUIManagerInstance => mainScreenUIManager;
        public ITowerModulesManager TowerModulesManagerInstance => towerModulesManager;
        public IModifierManager ModifierManagerInstance => modifierManager;

        public void Awake()
        {
            IEnumerable<GameObject> objects = FindObjectsOfType<GameObject>(true).Where(go => go.TryGetComponent<IBean>(out _));
            objects.Select(go => go.GetComponent<IBean>()).ToList().ForEach(bean => bean.SetupBeans(this));
            objects.ToList().ForEach(obj => obj.SetActive(true));
        }
    }
}