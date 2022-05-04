using Context;
using TowerModules.Modules;

namespace TowerModules
{
    public interface ITowerModulesManager : IInitResolve
    {
        void SetupModulesForTower();
        void AddNewTowerModule(TowerModuleContainer container);
        void StopActions();
    }
}