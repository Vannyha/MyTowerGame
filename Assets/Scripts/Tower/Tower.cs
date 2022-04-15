using UnityEngine;

namespace Tower
{
    public class Tower : MonoBehaviour
    {
        private float currentHp = 100;

        public float CurrentHp => currentHp;


        private void OnCollisionEnter2D(Collision2D other)
        {
            currentHp--;
        }
    }
}