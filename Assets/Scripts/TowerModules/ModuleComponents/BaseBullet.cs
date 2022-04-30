using Enemy;
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
            currentRigidbody.velocity = (currentTarget.position - transform.position).normalized * currentSpeed;
        }

        private void Update()
        {
            if (currentTarget != null)
            {
                currentRigidbody.velocity =
                    ((Vector2) (currentTarget.position - transform.position).normalized * currentAimingStrength +
                     currentRigidbody.velocity).normalized * currentSpeed;
            }
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.TryGetComponent(out IEnemy enemy))
            {
                enemy.ApplyChangeHp(0.1f);
                Destroy(gameObject);
            }
        }
    }
}