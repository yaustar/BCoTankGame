using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace TankGame {
    public class BaseTrophy : MonoBehaviour, IDamagable {
        [SerializeField]
        private int _startingHealth;

        [SerializeField]
        private UnityEvent _destroyedEvent;
        

        private int _health;
        
        
        private void Awake() {
            _health = _startingHealth;
        }


        public void OnDamageGiven(int damage, GiveDamage damageGiver, List<Vector2> damagePoints) {
            _health -= damage;
            if (_health <= 0) {
                gameObject.SetActive(false);
                _destroyedEvent.Invoke();
                
                // TODO Play an explosion effect
            }
        }
    }
}