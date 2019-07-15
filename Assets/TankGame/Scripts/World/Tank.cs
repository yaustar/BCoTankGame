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

        private ObjectPool _bulletObjectPool;
        
        
        private void Awake() {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _input = GetComponent<ITankInput>();

            _bulletObjectPool = new ObjectPool(_bulletPrefab, gameObject, 10);
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
                    var bulletObj = _bulletObjectPool.GetObject();
                    bulletObj.transform.SetParent(transform.parent);
                    var bullet = bulletObj.GetComponent<Bullet>();
                    bulletObj.transform.position = _bulletSpawnTransform.position;

                    bulletObj.SetActive(true);
                    
                    bullet.Set(_facingDirection, (GameObject obj) => {
                        _bulletObjectPool.ReturnObject(obj);
                    });
                }
            }
        }
    }
}