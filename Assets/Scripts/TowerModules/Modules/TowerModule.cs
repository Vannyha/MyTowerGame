using Enemy;
using Modifiers;
using UnityEngine;

namespace TowerModules.Modules
{
    public abstract class TowerModule : MonoBehaviour, ITowerModule
    {
        protected IEnemyManager enemyManager;
        protected IModifierManager modifierManager;
        public virtual void ProcessOnFixedUpdate()
        {
        }

        public virtual void SetupManagers(IEnemyManager eManager, IModifierManager mManager)
        {
            enemyManager = eManager;
            modifierManager = mManager;
            SetupBasics();
        }

        protected virtual void SetupBasics()
        {
        }
    }
}