using Context;
using Enemy;
using Game;
using UnityEngine;

namespace Tower
{
    public class TowerManager : MonoBehaviour, ITowerManager
    {
        [SerializeField] private Tower tower;

        private IEnemyManager enemyManager;
        private IGameManager gameManager;
        
        private Tower currentTower;

        public float CurrentTowerHp => currentTower.CurrentHp;
        public Transform CurrentTowerTransform => currentTower.transform;

        public void SetupBeans(GameContext context)
        {
            enemyManager = context.EnemyManagerInstance;
            gameManager = context.GameManagerInstance;
            currentTower = Instantiate(tower);
            gameManager.IsGameStarted = true;
        }

        private void Update()
        {
            if (currentTower.CurrentHp < 0)
            {
                gameManager.IsGameStarted = false;
            }
        }
    }
}