using System.Collections.Generic;
using Context;
using Enemy;
using Game;
using UnityEngine;

namespace Tower
{
    public class TowerManager : MonoBehaviour, ITowerManager
    {
        [SerializeField] private Tower tower;
        [SerializeField] private float towerHp;

        private IEnemyManager enemyManager;
        private IGameManager gameManager;
        
        private ITower currentTower;

        public float CurrentTowerHp => currentTower.CurrentHp;
        public Transform CurrentTowerTransform => currentTower.CurrentTransform;

        public void SetupBeans(GameContext context)
        {
            enemyManager = context.EnemyManagerInstance;
            gameManager = context.GameManagerInstance;
        }

        public void SetupTowerOnGame()
        {
            currentTower = Instantiate(tower);
            currentTower.SetupTower(towerHp);
        }

        public List<Transform> GetTowerModulesPlaces()
        {
            return tower.ModulePlaces;
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