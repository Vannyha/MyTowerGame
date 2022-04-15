using System.Collections.Generic;
using Context;
using Game;
using Helpers.SlowUpdate;
using Tower;
using UnityEngine;

namespace Enemy
{
    public class EnemyManager : MonoBehaviour, IEnemyManager
    {
        [SerializeField] private List<BaseEnemy> enemies;
        [SerializeField] private float spawnRate;
        [SerializeField] private float spawnDistance;
        [SerializeField] private float baseSpeed;

        private ITowerManager towerManager;
        private IGameManager gameManager;
        
        private List<BaseEnemy> currentEnemies = new List<BaseEnemy>();
        private SlowUpdateProc slowUpdateProcess;

        public void SetupBeans(GameContext context)
        {
            towerManager = context.TowerManagerInstance;
            gameManager = context.GameManagerInstance;
            slowUpdateProcess = new SlowUpdateProc(SpawnEnemy, spawnRate);
        }

        private void FixedUpdate()
        {
            if (!gameManager.IsGameStarted)
            {
                return;
            }

            slowUpdateProcess.ProceedOnFixedUpdate();
            currentEnemies.ForEach(enemy => enemy.ProcessOnFixedUpdate());
        }

        private void SpawnEnemy()
        {
            Vector2 spawnPos = (Vector2) towerManager.CurrentTowerTransform.position + new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
            BaseEnemy enemy = Instantiate(enemies[Random.Range(0, enemies.Count)], spawnPos, Quaternion.identity);
            enemy.SetParams(baseSpeed, 10f, towerManager.CurrentTowerTransform);
            currentEnemies.Add(enemy);
        }
    }
}