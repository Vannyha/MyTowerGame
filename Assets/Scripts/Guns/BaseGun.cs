using UnityEngine;

namespace Guns
{
    public abstract class BaseGun
    {
        [SerializeField] private float fireRate;

        protected abstract void Shoot();

        protected virtual void ProcessOnFixedUpdate()
        {
            Shoot();
        }
    }
}