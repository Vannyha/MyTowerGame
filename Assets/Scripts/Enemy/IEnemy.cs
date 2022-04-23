using Entity;

namespace Enemy
{
    public interface IEnemy : IDamageable, IProcessable, IDestroyable
    {
    }
}