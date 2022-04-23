using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
        
        private List<IEnemy> currentEnemies = new List<IEnemy>();
        private SlowUpdateProc slowUpdateProcess;

        public void SetupBeans(GameContext context)
        {
            towerManager = context.TowerManagerInstance;
            gameManager = context.GameManagerInstance;
            slowUpdateProcess = new SlowUpdateProc(SpawnEnemy, spawnRate);
        }

        public bool GetClosestEnemyInFrustrum(out IEnemy enemy, Vector2 moduleRight, Vector2 modulePos, float dot)
        {
            IEnumerable<IEnemy> enemiesInFrustrum = currentEnemies.Where(obj => Vector2.Dot(moduleRight, ((Vector2) obj.CurrentTransform.position - modulePos).normalized) > dot);

            if (!enemiesInFrustrum.Any())
            {
                enemy = null;
                return false;
            }

            enemy = enemiesInFrustrum.OrderBy(obj =>
                (obj.CurrentTransform.position - towerManager.CurrentTowerTransform.position).magnitude).First();
            return true;
        }

        private void FixedUpdate()
        {
            if (!gameManager.IsGameStarted)
            {
                if (currentEnemies.Count != 0)
                {
                    currentEnemies.ForEach(enemy => enemy.DestroyEntity());
                    currentEnemies.Clear();
                }
                return;
            }

            slowUpdateProcess.ProceedOnFixedUpdate();
            for (int i = currentEnemies.Count - 1; i >= 0; i--)
            {
                IEnemy enemy = currentEnemies[i];
                if (enemy.CurrentHp < 0f)
                {
                    currentEnemies.Remove(enemy);
                    enemy.DestroyEntity();
                    continue;
                }
                
                enemy.ProcessOnFixedUpdate();
            }
        }

        private void SpawnEnemy()
        {
            Vector2 spawnPos = (Vector2) towerManager.CurrentTowerTransform.position + new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized * spawnDistance;
            BaseEnemy enemy = Instantiate(enemies[Random.Range(0, enemies.Count)], spawnPos, Quaternion.identity);
            enemy.SetParams(baseSpeed, 10f, towerManager.CurrentTowerTransform);
            currentEnemies.Add(enemy);
        }
    }
}