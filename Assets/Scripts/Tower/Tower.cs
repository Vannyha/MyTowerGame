using System.Collections.Generic;
using Enemy;
using TowerModules;
using TowerModules.Modules;
using UnityEngine;

namespace Tower
{
    public class Tower : MonoBehaviour, ITower
    {
        [SerializeField] private List<Transform> modulePlaces;
        [SerializeField] private GameObject towerGameObject;
        [SerializeField] private ParticleSystem explodeEffect;

        private List<ITowerModule> currentModules;
        
        private float currentHp;

        public float CurrentHp => currentHp;
        public Transform CurrentTransform => transform;
        public List<Transform> ModulePlaces => modulePlaces;

        public void SetupTower(float hp)
        {
            currentHp = hp;
        }
        
        public void ApplyChangeHp(float val)
        {
            currentHp += val;
        }

        public void DestroyEntity()
        {
            towerGameObject.SetActive(false);
            explodeEffect.Play();
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