using Entity;

namespace Enemy
{
    public interface IEnemy : IDamageable
    {
        void DestroyEntity();
        void ProcessOnFixedUpdate();
    }
}