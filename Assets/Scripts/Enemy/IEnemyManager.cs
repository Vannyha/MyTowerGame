using Context;
using UnityEngine;

namespace Enemy
{
    public interface IEnemyManager : IBean
    {
        bool GetClosestEnemyInFrustrum(out IEnemy enemy, Vector2 moduleRight, Vector2 modulePos, float dot);
    }
}