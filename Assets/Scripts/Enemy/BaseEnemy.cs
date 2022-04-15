using UnityEngine;

namespace Enemy
{
    public class BaseEnemy : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D currentRigidbody2D;

        private float currentHp;
        private float currentSpeed;
        private Transform currentTarget;

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
    }
}