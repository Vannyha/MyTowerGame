using Enemy;
using TowerModules.ModuleComponents;
using UnityEngine;

namespace TowerModules.Modules.Guns
{
    public class MachineGun: TowerModule
    {
        [SerializeField] private float baseAttackSpeed;
        [SerializeField] private BaseBullet bullet;
        [SerializeField] private Transform bulletSpawnPoint;

        private float attackTimer;

        protected override void SetupBasics()
        {
            attackTimer = baseAttackSpeed;
        }

        public override void ProcessOnFixedUpdate()
        {
            if (attackTimer - Time.deltaTime < 0f)
            {
                Shoot();
                attackTimer = baseAttackSpeed;
            }

            attackTimer -= Time.deltaTime;
        }

        private void Shoot()
        {
            if (enemyManager.GetClosestEnemyInFrustrum(out IEnemy enemy, transform.right, transform.position, 0.7f))
            {
                BaseBullet newBullet = Instantiate(bullet, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
                newBullet.SetupParams(enemy.CurrentTransform, 5f, 10f, 1f);
                Destroy(newBullet.gameObject, 5f);
            }
        }
    }
}