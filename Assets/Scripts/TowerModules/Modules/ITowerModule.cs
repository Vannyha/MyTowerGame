using Enemy;
using Entity;
using Modifiers;

namespace TowerModules.Modules
{
    public interface ITowerModule : IProcessable, IPositioning, IDestroyable
    {
        void SetupManagers(IEnemyManager eManager, IModifierManager mManager);
        void SetupTowerFromContainer(TowerModuleContainer container);
    }
}