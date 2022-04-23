using System.Collections.Generic;
using Entity;
using UnityEngine;

namespace Tower
{
    public interface ITower : IDamageable, IDestroyable
    {
        void SetupTower(float hp);
        Transform CurrentTransform { get; }
        List<Transform> ModulePlaces { get; }
    }
}