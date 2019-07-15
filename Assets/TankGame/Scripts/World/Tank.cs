using System;
using UnityEngine;

namespace TankGame {
    public class Tank : MonoBehaviour {
        [SerializeField]
        private float _maxSpeed;

        [SerializeField]
        private Transform _spriteRoot;
        
        [SerializeField]
        private Animator _animator;

        [SerializeField]
        private GameObject _bulletPrefab;

        [SerializeField]
        private Transform _bulletSpawnTransform;
        

        private Rigidbody2D _rigidbody2D;
        private ITankInput _input;
        private bool _movingLastFrame = false;
        private Direction _facingDirection = Direction.Up;
        
        private void Awake() {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _input = GetComponent<ITankInput>();
        }


        private void Update() {
            if (_input != null) {
                var direction = _input.GetDirection();
                var velocity = new Vector3();

                var localRot = _spriteRoot.localEulerAngles;
                
                switch (direction) {
                    case Direction.Down: {
                        velocity.y = -1f;
                        localRot.z = 180f;
                        break;
                    }
                        
                    case Direction.Up: {
                        velocity.y = 1f;
                        localRot.z = 0f;
                        break;
                    }
                    
                    case Direction.Left: {
                        velocity.x = -1f;
                        localRot.z = 90f;
                        break;
                    }
                    
                    case Direction.Right: {
                        velocity.x = 1f;
                        localRot.z = 270f;
                        break;
                    }
                    
                    default: break;
                }

                velocity *= _maxSpeed;
                _rigidbody2D.velocity = velocity;
                
                _spriteRoot.localEulerAngles = localRot;

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


                if (_input.HasAttemptedFired()) {
                    var obj = Instantiate(_bulletPrefab, transform.parent, false);
                    var bullet = obj.GetComponent<Bullet>();
                    obj.transform.position = _bulletSpawnTransform.position;
                    bullet.Set(_facingDirection);
                }
            }
        }
    }
}