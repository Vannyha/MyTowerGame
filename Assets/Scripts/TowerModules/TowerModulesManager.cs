using System.Collections.Generic;
using Context;
using Enemy;
using Game;
using Tower;
using TowerModules.Modules;
using TowerModules.Modules.Guns;
using UnityEngine;

namespace TowerModules
{
    public class TowerModulesManager : MonoBehaviour, ITowerModulesManager
    {
        [SerializeField] private List<TowerModule> modules;

        private IGameManager gameManager;
        private ITowerManager towerManager;
        private IEnemyManager enemyManager;
        
        private List<ITowerModule> currentModules = new List<ITowerModule>();
        public void SetupBeans(GameContext context)
        {
            gameManager = context.GameManagerInstance;
            towerManager = context.TowerManagerInstance;
            enemyManager = context.EnemyManagerInstance;
        }

        public void SetupModulesForTower()
        {
            towerManager.GetTowerModulesPlaces().ForEach(pl => 
                currentModules.Add(Instantiate(modules[0], transform.position, transform.rotation)));
        }

        private void FixedUpdate()
        {
            if (!gameManager.IsGameStarted)
            {
                return;
            }
            
            
        }
    }
}