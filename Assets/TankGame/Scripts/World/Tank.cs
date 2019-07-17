using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

namespace TankGame {
    public class Tank : MonoBehaviour, IGun, IDamagable {
        [SerializeField, BoxGroup("References")]
        private Transform _spriteRoot;
        
        [SerializeField, BoxGroup("References")]
        private Animator _animator;

        [SerializeField, BoxGroup("References")]
        private GameObject _bulletPrefab;
        
        [SerializeField, BoxGroup("References")]
        private Transform _bulletSpawnTransform;
        
        [SerializeField, BoxGroup("Properties")]
        private float _maxSpeed;

        [SerializeField, BoxGroup("Properties")]
        private float _gunReloadTimeSecs;

        [SerializeField]
        private UnityEvent _explosionStartedEvent;
        
        [FormerlySerializedAs("_diedEvent")] [SerializeField]
        private UnityEvent _explosionEndedEvent;

        
        private Rigidbody2D _rigidbody2D;
        private Collider2D _collider2D;
        private ITankInput _input;
        private bool _movingLastFrame = false;
        private Direction _facingDirection = Direction.Up;
        private float _secsTimeSinceGunFired = float.MaxValue;
        private bool _canMove = true;

        private Action<GameObject> _deadCallback;
        
        
        // Should this be managed by an external manager/controller?
        // What happens when the tank dies if there are still bullets 
        // on the screen?
        private ObjectPool _bulletObjectPool;
        
        
        private void Awake() {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _collider2D = GetComponent<Collider2D>();
            
            _input = GetComponent<ITankInput>();

            _bulletObjectPool = new ObjectPool(_bulletPrefab, gameObject, 5);
        }


        private void Update() {
            _secsTimeSinceGunFired += Time.deltaTime;
            
            if (_input != null && _canMove) {
                var direction = _input.GetDirection(this);
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

                velocity *= _maxSpeed;
                _rigidbody2D.velocity = velocity;
                
                if (direction != Direction.None) {
                    if (!_movingLastFrame) {
                        _animator.Play("Moving");
                    }

                    _facingDirection = direction;
                    _movingLastFrame = true;
                } else {
                    if (_movingLastFrame) {
                        _animator.Play("Idle");
                    }
                    
                    _movingLastFrame = false;
                }
                
                SetSpriteDirection(_facingDirection);
                
                if (_secsTimeSinceGunFired >= _gunReloadTimeSecs && _input.HasAttemptedFired(this)) {
                    var bulletObj = _bulletObjectPool.GetObject();
                    bulletObj.transform.SetParent(transform.parent);
                    var bullet = bulletObj.GetComponent<Bullet>();
                    bulletObj.transform.position = _bulletSpawnTransform.position;

                    bulletObj.SetActive(true);
                    
                    bullet.Restart(_facingDirection, (GameObject obj) => {
                        // Safety if tank is removed from the game for whatever reason
                        // Tanks would be pool managed but just in case
                        if (this == null) {
                            Destroy(obj);
                        } else {
                            _bulletObjectPool.ReturnObject(obj);
                        }
                    });

                    _secsTimeSinceGunFired = 0f;
                }
            }

            if (!_canMove) {
                _rigidbody2D.velocity = Vector2.zero;
            }
        }
        
        
        // Public API
        public float GetReloadTimeSecs() {
            return _gunReloadTimeSecs;
        }


        public float GetSecSinceLastFired() {
            return _secsTimeSinceGunFired;
        }

        
        public Direction GetDirection() {
            return _facingDirection;
        }
        
        
        public Vector3 GetBulletSpawnPosition() {
            return _bulletSpawnTransform.position;
        }


        public void Restart(Direction startDirection, Action<GameObject> destroyCallback) {
            _facingDirection = startDirection;
            SetSpriteDirection(startDirection);
            _rigidbody2D.velocity = Vector2.zero;
            _collider2D.enabled = true;
            _canMove = true;

            _deadCallback = destroyCallback;
        }


        public void OnDamageGiven(int damage, GiveDamage damageGiver, List<Vector2> damagePoints) {
            StartCoroutine(DeathAnimation());
            _canMove = false;
        }

        
        
        // Private
        private void SetSpriteDirection(Direction direction) {
            var localRot = _spriteRoot.localEulerAngles;
            
            switch (direction) {
                case Direction.Down: {
                    localRot.z = 180f;
                    break;
                }
                        
                case Direction.Up: { 
                    localRot.z = 0f;
                    break;
                }
                    
                case Direction.Left: {
                    localRot.z = 90f;
                    break;
                }
                    
                case Direction.Right: {
                    localRot.z = 270f;
                    break;
                }
                    
                default: break;
            }
            
            _spriteRoot.localEulerAngles = localRot;
        }


        private IEnumerator DeathAnimation() {
            _animator.Play("Explode");
            _collider2D.enabled = false;
            _explosionStartedEvent.Invoke();
            
            var wait = new WaitForSeconds(2f);
            yield return wait;
            
            gameObject.SetActive(false);
            _deadCallback?.Invoke(gameObject);
            _explosionEndedEvent.Invoke();
        }

    }
}