using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace TankGame {
    public class Bullet : MonoBehaviour, IDamagable {
        [SerializeField]
        private float _speed;

        [SerializeField]
        private float _lifeTimeSecs;


        private Rigidbody2D _rigidbody2D;
        private float _secsSinceSpawned = 0f;

        private Action<GameObject> _deadCallback;


        private void Awake() {
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }


        private void Update() {
            _secsSinceSpawned += Time.deltaTime;
            if (_secsSinceSpawned > _lifeTimeSecs) {
                OnDead();
            }
        }


        private void OnCollisionEnter2D(Collision2D other) {
            OnDead();
        }


        // Public API
        public void Restart(Direction direction, Action<GameObject> destroyCallback) {
            var velocity = new Vector3();

            switch (direction) {
                case Direction.Down: {
                    velocity.y = -1f;
                    break;
                }
                
                case Direction.Up: {
                    velocity.y = 1f;
                    break;
                }

                case Direction.Left: {
                    velocity.x = -1f;
                    break;
                }

                case Direction.Right: {
                    velocity.x = 1f;
                    break;
                }
                    
                default: break;
            }

            velocity *= _speed;

            _secsSinceSpawned = 0f;
            _rigidbody2D.velocity = velocity;
            _deadCallback = destroyCallback;
        }


        public void OnDamageGiven(int damage, GiveDamage damageGiver, List<Vector2> damagePoints) {
            OnDead();
        }


        // Private 
        private void OnDead() {
            if (_deadCallback != null) {
                _deadCallback(gameObject);
            } else {
                Destroy(gameObject);
            }
        }
    }
}