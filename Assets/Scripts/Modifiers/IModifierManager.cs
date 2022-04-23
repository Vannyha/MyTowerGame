using Context;

namespace Modifiers
{
    public interface IModifierManager : IBean
    {
        float DamageModifier { get; }
        float AimingStrengthModifier { get; }
        float SpeedModifier { get; }
    }
}