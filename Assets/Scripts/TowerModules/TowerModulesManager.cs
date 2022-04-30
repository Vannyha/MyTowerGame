using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Context;
using Enemy;
using Game;
using GameScripts;
using Modifiers;
using Tower;
using TowerModules.Modules;
using UnityEngine;

namespace TowerModules
{
    public class TowerModulesManager : MonoBehaviour, ITowerModulesManager
    {
        [SerializeField] private TowerModuleDatabase database;

        private IGameManager gameManager;
        private ITowerManager towerManager;
        private IEnemyManager enemyManager;
        private IModifierManager modifierManager;
        private ISaveManager saveManager;

        private ObservableCollection<TowerModuleContainer> containers;
        private List<ITowerModule> currentModulesGame = new List<ITowerModule>();
        public void SetupBeans(GameContext context)
        {
            saveManager = context.SaveManagerInstance;
            gameManager = context.GameManagerInstance;
            towerManager = context.TowerManagerInstance;
            enemyManager = context.EnemyManagerInstance;
            modifierManager = context.ModifierManagerInstance;
            List<TowerModuleContainer> tempList = saveManager.LoadValue("TowerContainers", new List<TowerModuleContainer>
            {
                new TowerModuleContainer {TowerType = TowerModuleType.MachineGun, AttackSpeed = 1f, Damage = 5f, DotRange = 0.4f, AimingStrength = 1, ProjectileSpeed = 2},
                new TowerModuleContainer {TowerType = TowerModuleType.MachineGun, AttackSpeed = 1f, Damage = 5f, DotRange = 0.4f, AimingStrength = 1, ProjectileSpeed = 2},
            });
            
            containers = new ObservableCollection<TowerModuleContainer>(tempList);
            containers.CollectionChanged += (sender, args) => saveManager.SaveValue("TowerContainers", containers.ToList());
        }

        public void SetupModulesForTower()
        {
            List<Transform> modulePlaces = towerManager.GetTowerModulesPlaces();
            
            for (int i = 0; i < containers.Count; i++)
            {
                if (i <= modulePlaces.Count)
                {
                    TowerModule tempModule = Instantiate(database.GetTowerModuleByType(containers[i].TowerType),
                        modulePlaces[i].position, modulePlaces[i].rotation);
                    tempModule.SetupManagers(enemyManager, modifierManager);
                    tempModule.SetupTowerFromContainer(containers[i]);
                    currentModulesGame.Add(tempModule);
                }
            }
        }

        public void AddNewTowerModule(TowerModuleContainer container)
        {
            containers.Add(container);
        }

        public void StopActions()
        {
            currentModulesGame.ForEach(m => m.DestroyEntity());
            currentModulesGame.Clear();
        }

        private void FixedUpdate()
        {
            if (!gameManager.IsGameStarted)
            {
                return;
            }
            
            currentModulesGame.ForEach(mod => mod.ProcessOnFixedUpdate());
        }
    }
}