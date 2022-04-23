using Context;
using UnityEngine;

namespace Modifiers
{
    public class ModifierManager : MonoBehaviour, IModifierManager
    {
        private float damageModifier = 1f;
        private float aimingStrengthModifier = 1f;
        private float speedModifier = 1f;

        public float DamageModifier => damageModifier;
        public float AimingStrengthModifier => aimingStrengthModifier;
        public float SpeedModifier => speedModifier;

        public void SetupBeans(GameContext context)
        {
            
        }
    }
}