using System.Collections.Generic;
using Context;
using Enemy;
using Game;
using Modifiers;
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
        private IModifierManager modifierManager;
        
        private List<ITowerModule> currentModules = new List<ITowerModule>();
        public void SetupBeans(GameContext context)
        {
            gameManager = context.GameManagerInstance;
            towerManager = context.TowerManagerInstance;
            enemyManager = context.EnemyManagerInstance;
            modifierManager = context.ModifierManagerInstance;
        }

        public void SetupModulesForTower()
        {
            towerManager.GetTowerModulesPlaces().ForEach(pl => 
                currentModules.Add(Instantiate(modules[0], pl.position, pl.rotation)));
            currentModules.ForEach(module => module.SetupManagers(enemyManager, modifierManager));
        }

        private void FixedUpdate()
        {
            if (!gameManager.IsGameStarted)
            {
                return;
            }
            
            currentModules.ForEach(mod => mod.ProcessOnFixedUpdate());
        }
    }
}