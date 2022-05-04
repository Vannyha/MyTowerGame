using Context;
using UnityEngine;

namespace Enemy
{
    public interface IEnemyManager : IInitResolve
    {
        bool GetClosestEnemyInFrustrum(out IEnemy enemy, Vector2 moduleRight, Vector2 modulePos, float dot);
    }
}