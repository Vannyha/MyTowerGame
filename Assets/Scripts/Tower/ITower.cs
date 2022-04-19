using System.Collections.Generic;
using Entity;
using UnityEngine;

namespace Tower
{
    public interface ITower : IDamageable
    {
        void SetupTower(float hp);
        Transform CurrentTransform { get; }
        List<Vector3> ModulePlaces { get; }
    }
}