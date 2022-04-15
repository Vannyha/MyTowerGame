﻿using Context;
using UnityEngine;

namespace Tower
{
    public interface ITowerManager : IBean
    {
        float CurrentTowerHp { get; }
        Transform CurrentTowerTransform { get; }
    }
}