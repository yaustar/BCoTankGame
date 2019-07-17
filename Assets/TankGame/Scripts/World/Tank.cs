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
    [RequireComponent(typeof(Collider2D))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class Tank : MonoBehaviour, IDamagable {
        [SerializeField, BoxGroup("References")]
        private Transform _spriteRoot;
        
        [SerializeField, BoxGroup("References")]
        private Animator _animator;
        
        [SerializeField, BoxGroup("Optional References")]
        private WeaponGun _optionalWeaponGun;

        [SerializeField, BoxGroup("Optional References")]
        private WeaponBomb _optionalWeaponBomb;

        
        [SerializeField, BoxGroup("Properties")]
        private float _maxSpeed;
        
        [SerializeField]
        private UnityEvent _explosionStartedEvent;
        
        [FormerlySerializedAs("_diedEvent")] [SerializeField]
        private UnityEvent _explosionEndedEvent;

        
        private Rigidbody2D _rigidbody2D;
        private Collider2D _collider2D;
        private ITankInput _input;
        private bool _movingLastFrame = false;
        private Direction _facingDirection = Direction.Up;
        private bool _canMove = true;

        private Action<GameObject> _deadCallback;
        
        
        private void Awake() {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _collider2D = GetComponent<Collider2D>();
            
            _input = GetComponent<ITankInput>();
        }


        private void Update() {
            if (_input != null && _canMove) {
                UpdateMovement();
                UpdateWeapons();
            }

            if (!_canMove) {
                _rigidbody2D.velocity = Vector2.zero;
            }
        }


        // Public API
        public Direction GetDirection() {
            return _facingDirection;
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
        private void UpdateMovement() {
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
        }
        
        
        private void UpdateWeapons() {
            if (_optionalWeaponGun != null) {
                if (_optionalWeaponGun.CanFire() && _input.HasAttemptedFired(this)) {
                    _optionalWeaponGun.Fire(_facingDirection, transform);
                }
            }

            if (_optionalWeaponBomb != null) {
                if (_optionalWeaponBomb.CanFire() && _input.HasAttemptedBomb(this)) {
                    _optionalWeaponBomb.Fire(_facingDirection, transform);
                }
            }
        }
        
        
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