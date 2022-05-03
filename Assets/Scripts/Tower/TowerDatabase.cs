using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Tower
{
    public class TowerDatabase : ScriptableObject
    {
        [SerializeField] private List<TowerDatabaseSlot> towerInfos;

        public Tower GetTowerByType(TowerType type)
        {
            return towerInfos.First(t => t.Type == type).Tower;
        }
    }
    
    [Serializable]
    public class TowerDatabaseSlot
    {
        [SerializeField] private Tower tower;
        [SerializeField] private TowerType type;

        public Tower Tower => tower;
        public TowerType Type => type;
    }

    public enum TowerType
    {
        None = 0,
        Duo = 2,
        Trio = 3,
        Quad = 4,
        Penta = 5,
        Hexa = 6,
        Hepta = 7,
        Octa = 8,
    }
}