using Tower;
using UnityEngine;

namespace Enemy
{
    public class BaseEnemy : MonoBehaviour, IEnemy
    {
        [SerializeField] private Rigidbody2D currentRigidbody2D;
        [SerializeField] private ParticleSystem particleSystem;

        private float currentHp;
        private float currentSpeed;
        private Transform currentTarget;
        private float counterTest = 10f;

        public float CurrentHp => currentHp;
        public Transform CurrentTransform => transform;

        public void ApplyChangeHp(float val)
        {
            currentHp += val;
        }

        public void DestroyEntity()
        {
            ParticleSystem particleSystemCurrent = Instantiate(particleSystem, transform.position, particleSystem.transform.rotation);
            Destroy(particleSystemCurrent.gameObject, particleSystemCurrent.main.duration);
            Destroy(gameObject);
        }

        public void ProcessOnFixedUpdate()
        {
            currentRigidbody2D.AddForce((currentTarget.position - transform.position).normalized);
            currentRigidbody2D.velocity = Vector2.ClampMagnitude(currentRigidbody2D.velocity, currentSpeed);
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