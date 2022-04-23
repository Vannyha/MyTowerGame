using System;
using UnityEngine;

namespace TowerModules.ModuleComponents
{
    public class BaseBullet : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D currentRigidbody;
        
        private Transform currentTarget;
        private float currentDamage;
        private float currentSpeed;
        private float currentAimingStrength;

        public void SetupParams(Transform target, float damage, float speed, float aimingStrength)
        {
            currentTarget = target;
            currentDamage = damage;
            currentSpeed = speed;
            currentAimingStrength = aimingStrength;
        }

        private void Update()
        {
            //currentRigidbody.velocity
        }
    }
}