using Enemy;
using Helpers.SlowUpdate;
using TowerModules.ModuleComponents;
using UnityEngine;

namespace TowerModules.Modules.Guns
{
    public class MachineGun: TowerModule
    {
        [SerializeField] private BaseBullet bullet;
        [SerializeField] private Transform bulletSpawnPoint;
        
        SlowUpdateProc slowUpdateProc;

        private float attackTimer;
        private float currentDamage;
        private float currentDotRange;
        private float currentBulletSpeed;
        private float currentAimingStrength;

        public override void SetupTowerFromContainer(TowerModuleContainer container)
        {
            attackTimer = container.AttackSpeed;
            currentDamage = container.Damage;
            currentDotRange = container.DotRange;
            currentBulletSpeed = container.ProjectileSpeed;
            currentAimingStrength = container.AimingStrength;
            slowUpdateProc = new SlowUpdateProc(Shoot, attackTimer);
        }

        public override void ProcessOnFixedUpdate()
        {
            slowUpdateProc.ProceedOnFixedUpdate();
        }

        private void Shoot()
        {
            if (enemyManager.GetClosestEnemyInFrustrum(out IEnemy enemy, transform.right, transform.position, currentDotRange))
            {
                BaseBullet newBullet = Instantiate(bullet, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
                newBullet.SetupParams(enemy.CurrentTransform, currentDamage, currentBulletSpeed, currentAimingStrength);
                Destroy(newBullet.gameObject, 5f);
            }
        }
    }
}