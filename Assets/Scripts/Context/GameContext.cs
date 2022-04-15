using System.Collections.Generic;
using System.Linq;
using Enemy;
using Game;
using Tower;
using UIManagers.ScreenStats;
using UnityEngine;

namespace Context
{
    public class GameContext : MonoBehaviour
    {
        [SerializeField] private GameManager gameManager;
        [SerializeField] private TowerManager towerManager;
        [SerializeField] private EnemyManager enemyManager;
        [SerializeField] private ScreenStatsUIManager screenStatsUIManager;

        public ITowerManager TowerManagerInstance => towerManager;
        public IEnemyManager EnemyManagerInstance => enemyManager;
        public IGameManager GameManagerInstance => gameManager;
        public IScreenStatsUIManager ScreenStatsUIManagerInstance => screenStatsUIManager;
        
        public void Awake()
        {
            IEnumerable<GameObject> objects = FindObjectsOfType<GameObject>(true).Where(go => go.TryGetComponent<IBean>(out _));
            objects.Select(go => go.GetComponent<IBean>()).ToList().ForEach(bean => bean.SetupBeans(this));
            objects.ToList().ForEach(obj => obj.SetActive(true));
        }
    }
}