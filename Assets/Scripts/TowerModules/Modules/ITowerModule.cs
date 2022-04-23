using Enemy;
using Entity;
using Modifiers;

namespace TowerModules.Modules
{
    public interface ITowerModule : IProcessable
    {
        void SetupManagers(IEnemyManager eManager, IModifierManager mManager);
    }
}