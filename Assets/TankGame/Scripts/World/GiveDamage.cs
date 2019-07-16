using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace TankGame {
    public class GiveDamage : MonoBehaviour {
        [SerializeField]
        private int _damage;
        
        
        private void OnCollisionEnter2D(Collision2D other) {
            var damagables = other.transform.GetComponents<IDamagable>();
            var damagePoints = new List<Vector2>();

            for (int i = 0; i < other.contactCount; ++i) {
                damagePoints.Add(other.GetContact(i).point);
            }

            for (int i = 0; i < damagables.Length; ++i) {
                damagables[i].OnDamageGiven(_damage, this, damagePoints);
            }
        }
    }
}