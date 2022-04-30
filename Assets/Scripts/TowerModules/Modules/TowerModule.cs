using Enemy;
using Modifiers;
using UnityEngine;

namespace TowerModules.Modules
{
    public abstract class TowerModule : MonoBehaviour, ITowerModule
    {
        protected IEnemyManager enemyManager;
        protected IModifierManager modifierManager;

        public Transform CurrentTransform => transform;
        public GameObject CurrentGameObject => gameObject;
        
        public virtual void ProcessOnFixedUpdate()
        {
        }

        public virtual void SetupManagers(IEnemyManager eManager, IModifierManager mManager)
        {
            enemyManager = eManager;
            modifierManager = mManager;
        }

        public virtual void SetupTowerFromContainer(TowerModuleContainer container)
        {
        }

        public void DestroyEntity()
        {
            Destroy(gameObject);
        }
    }
}