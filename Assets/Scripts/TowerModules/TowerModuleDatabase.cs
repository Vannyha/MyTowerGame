using System;
using System.Collections.Generic;
using System.Linq;
using TowerModules.Modules;
using UnityEngine;

namespace TowerModules
{
    public class TowerModuleDatabase : ScriptableObject
    {
        [SerializeField] private List<TowerModuleDatabaseSlot> towerInfos;

        public TowerModule GetTowerModuleByType(TowerModuleType type)
        {
            return towerInfos.First(t => t.ModuleType == type).Module;
        }
    }

    [Serializable]
    public class TowerModuleDatabaseSlot
    {
        [SerializeField] private TowerModule module;
        [SerializeField] private TowerModuleType type;

        public TowerModule Module => module;
        public TowerModuleType ModuleType => type;
    }

    public enum TowerModuleType
    {
        None = 0,
        MachineGun = 1,
    }
}