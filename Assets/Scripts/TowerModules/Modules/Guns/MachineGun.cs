using UnityEngine;

namespace TowerModules.Modules.Guns
{
    public class MachineGun: TowerModule
    {
        [SerializeField] private float baseAttackSpeed;
        [SerializeField] private GameObject bullet;

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
            
        }
    }
}