using Tower;
using UnityEngine;

namespace Enemy
{
    public class BaseEnemy : MonoBehaviour, IEnemy
    {
        [SerializeField] private Rigidbody2D currentRigidbody2D;

        private float currentHp;
        private float currentSpeed;
        private Transform currentTarget;
        private float counterTest = 10f;

        public float CurrentHp => currentHp;

        public void ApplyChangeHp(float val)
        {
            currentHp += val;
        }

        public void DestroyEntity()
        {
            Destroy(gameObject);
        }

        public void ProcessOnFixedUpdate()
        {
            currentRigidbody2D.velocity = (currentTarget.position - transform.position).normalized * currentSpeed;
        }

        public void SetParams(float speed, float hp, Transform target)
        {
            currentSpeed = speed;
            currentHp = hp;
            currentTarget = target;
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.TryGetComponent(out ITower tower))
            {
                tower.ApplyChangeHp(-counterTest);
            }
        }
    }
}