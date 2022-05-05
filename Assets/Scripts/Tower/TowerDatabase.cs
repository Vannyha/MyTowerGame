using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Tower
{
    [CreateAssetMenu(fileName = "Tower Database", menuName = "Databases/Tower Database", order = 0)]
    public class TowerDatabase : ScriptableObject
    {
        [SerializeField] private List<TowerDatabaseSlot> towerInfos;

        public Tower GetTowerByType(TowerType type)
        {
            return towerInfos.First(t => t.Type == type).Tower;
        }

        public Sprite GetTowerSpriteByType(TowerType type)
        {
            return towerInfos.First(t => t.Type == type).SpriteForUI;
        }
    }
    
    [Serializable]
    public class TowerDatabaseSlot
    {
        [SerializeField] private Tower tower;
        [SerializeField] private TowerType type;
        [SerializeField] private Sprite spriteForUI;

        public Tower Tower => tower;
        public TowerType Type => type;
        public Sprite SpriteForUI => spriteForUI;
    }

    public enum TowerType
    {
        None = 0,
        Duo = 2,
        Trio = 3,
        Quad = 4,
        Penta = 5,
        Hexa = 6,
        //Hepta = 7,
        //Octa = 8,
    }
}