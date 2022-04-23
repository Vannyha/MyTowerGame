using System.Collections.Generic;
using Entity;
using UnityEngine;

namespace Tower
{
    public interface ITower : IDamageable, IDestroyable, IPositioning
    {
        void SetupTower(float hp);
        List<Transform> ModulePlaces { get; }
    }
}