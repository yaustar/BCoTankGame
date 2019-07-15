using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace TankGame {
    public class Bullet : MonoBehaviour {
        [SerializeField]
        private float _speed;

        [SerializeField]
        private float _lifeTimeSecs;


        private Rigidbody2D _rigidbody2D;
        private float _secsSinceSpawned = 0f;


        private void Awake() {
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }


        private void Update() {
            _secsSinceSpawned += Time.deltaTime;
            if (_secsSinceSpawned > _lifeTimeSecs) {
                // Stub change for using a pool manager later
                Destroy(this.gameObject);
            }
        }


        private void OnCollisionEnter2D(Collision2D other) {
            // Stub change for using a pool manager later
            Destroy(this.gameObject);
        }


        // Public API
        public void Set(Direction direction) {
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
            
            _rigidbody2D.velocity = velocity;
        }
    }
}