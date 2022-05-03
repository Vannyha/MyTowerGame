using System.Collections.Generic;
using Context;
using UnityEngine;

namespace Tower
{
    public interface ITowerManager : IBean
    {
        float CurrentTowerHp { get; }
        Transform CurrentTowerTransform { get; }
        bool IsAnyTowerChoosed { get; }
        void SetupTowerOnGame();
        List<Transform> GetTowerModulesPlaces();
        void SetTowerType(TowerType type);
    }
}