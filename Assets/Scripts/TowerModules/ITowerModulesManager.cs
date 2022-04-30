using Context;
using TowerModules.Modules;

namespace TowerModules
{
    public interface ITowerModulesManager : IBean
    {
        void SetupModulesForTower();
        void AddNewTowerModule(TowerModuleContainer container);
        void StopActions();
    }
}