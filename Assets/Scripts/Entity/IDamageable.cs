namespace Entity
{
    public interface IDamageable
    {
        void ApplyChangeHp(float val);
        float CurrentHp { get; }
    }
}