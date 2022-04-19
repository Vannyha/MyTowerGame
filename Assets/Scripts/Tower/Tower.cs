using System.Collections.Generic;
using Enemy;
using UnityEngine;

namespace Tower
{
    public class Tower : MonoBehaviour, ITower
    {
        [SerializeField] private List<Vector3> modulePlaces;
        
        private float currentHp;

        public float CurrentHp => currentHp;
        public Transform CurrentTransform => transform;
        public List<Vector3> ModulePlaces => modulePlaces;

        public void SetupTower(float hp)
        {
            currentHp = hp;
        }
        
        public void ApplyChangeHp(float val)
        {
            currentHp += val;
        }
        
        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.TryGetComponent(out IEnemy enemy))
            {
                enemy.ApplyChangeHp(-20);
            }
        }
    }
}