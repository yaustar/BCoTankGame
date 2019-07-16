using System;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace TankGame {
    public class Tank : MonoBehaviour {
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

        
        private Rigidbody2D _rigidbody2D;
        private ITankInput _input;
        private bool _movingLastFrame = false;
        private Direction _facingDirection = Direction.Up;
        private float _secsTimeSinceGunFired = float.MaxValue;

        
        // Should this be managed by an external manager/controller?
        // What happens when the tank dies if there are still bullets 
        // on the screen?
        private ObjectPool _bulletObjectPool;
        
        
        private void Awake() {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _input = GetComponent<ITankInput>();

            _bulletObjectPool = new ObjectPool(_bulletPrefab, gameObject, 5);
        }


        private void Update() {
            _secsTimeSinceGunFired += Time.deltaTime;
            
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
                
                if (_input.HasAttemptedFired() && _secsTimeSinceGunFired >= _gunReloadTimeSecs) {
                    var bulletObj = _bulletObjectPool.GetObject();
                    bulletObj.transform.SetParent(transform.parent);
                    var bullet = bulletObj.GetComponent<Bullet>();
                    bulletObj.transform.position = _bulletSpawnTransform.position;

                    bulletObj.SetActive(true);
                    
                    bullet.Set(_facingDirection, (GameObject obj) => {
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
        }
    }
}