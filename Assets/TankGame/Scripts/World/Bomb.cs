using System;
using System.Collections.Generic;
using UnityEngine;

namespace TankGame {
    [RequireComponent(typeof(GiveDamage))]
    public class Bomb : MonoBehaviour, IDamagable {
        private GiveDamage _giveDamage;


        private List<GameObject> _objectsInArea = new List<GameObject>();
        private Action<GameObject> _deadCallback;
        private float _timeSinceSpawn;
        private bool _isDead = false;
        
        private void Awake() {
            _giveDamage = GetComponent<GiveDamage>();
            _giveDamage.enabled = false;
        }


        private void Update() {
            _timeSinceSpawn += Time.deltaTime;
            if (_timeSinceSpawn > 0.1f) {
                if (!_giveDamage.enabled) {
                    if (_objectsInArea.Count == 0) {
                        _giveDamage.enabled = true;
                    }
                }
            }
        }
        
        
        private void OnTriggerEnter2D (Collider2D other) {
            if (!_objectsInArea.Contains(other.gameObject)) {
                _objectsInArea.Add(other.gameObject);
            }
            
            if (_giveDamage.enabled) {
                OnDead();  
            } 
        }
 
        
        private void OnTriggerExit2D (Collider2D other) {
            _objectsInArea.Remove(other.gameObject);
        }


        // Public API
        public void Restart( Action<GameObject> destroyCallback) {
            _timeSinceSpawn = 0f;
            _deadCallback = destroyCallback;
            _objectsInArea.Clear();
            _giveDamage.enabled = false;
            _giveDamage.Restart();
            _isDead = false;
        }


        public void OnDamageGiven(int damage, GiveDamage damageGiver, List<Vector2> damagePoints) {
            OnDead();
        }

        
        
        // Private 
        private void OnDead() {
            if (!_isDead) {
                if (_deadCallback != null) {
                    _deadCallback(gameObject);
                } else {
                    Destroy(gameObject);
                }

                _isDead = true;
            }
        }
    }
}