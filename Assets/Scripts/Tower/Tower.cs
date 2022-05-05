using System.Collections.Generic;
using Enemy;
using TowerModules.Modules;
using UnityEngine;

namespace Tower
{
    public class Tower : MonoBehaviour, ITower
    {
        [SerializeField] private List<Transform> modulePlaces;
        [SerializeField] private SpriteRenderer towerSpriteObject;
        [SerializeField] private ParticleSystem explodeEffect;

        private List<ITowerModule> currentModules;
        
        private float currentHp;

        public float CurrentHp => currentHp;
        public Transform CurrentTransform => transform;
        public GameObject CurrentGameObject => gameObject;
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
            towerSpriteObject.enabled = false;
            explodeEffect.Play();
            Destroy(gameObject, explodeEffect.main.duration);
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