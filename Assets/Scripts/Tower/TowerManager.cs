using System.Collections.Generic;
using Context;
using Enemy;
using Game;
using GameScripts;
using Save;
using UnityEngine;

namespace Tower
{
    public class TowerManager : MonoBehaviour, ITowerManager
    {
        [SerializeField] private TowerDatabase database;
        [SerializeField] private float towerHp;

        private IEnemyManager enemyManager;
        private IGameManager gameManager;
        private ISaveManager saveManager;
        
        private ITower currentTower;
        private TowerType choosedTower;

        public float CurrentTowerHp => currentTower.CurrentHp;
        public Transform CurrentTowerTransform => currentTower.CurrentTransform;
        public bool IsAnyTowerChoosed => choosedTower != TowerType.None;

        public void SetupBeans(GameContext context)
        {
            enemyManager = context.EnemyManagerInstance;
            gameManager = context.GameManagerInstance;
            saveManager = context.SaveManagerInstance;
            choosedTower = saveManager.LoadValue(SaveKeys.CurrentTower, TowerType.None);
        }

        public void SetupTowerOnGame()
        {
            currentTower = Instantiate(database.GetTowerByType(choosedTower));
            currentTower.SetupTower(towerHp);
        }

        public List<Transform> GetTowerModulesPlaces()
        {
            return currentTower.ModulePlaces;
        }

        public void SetTowerType(TowerType type)
        {
            choosedTower = type;
            saveManager.SaveValue(SaveKeys.CurrentTower, type);
        }

        private void FixedUpdate()
        {
            if (!gameManager.IsGameStarted)
            {
                return;
            }
            
            if (currentTower.CurrentHp < 0)
            {
                gameManager.StopGame();
                currentTower.DestroyEntity();
            }
        }
    }
}