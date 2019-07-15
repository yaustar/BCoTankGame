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


        private Rigidbody2D _rigidbody2D;
        private ITankInput _input;
        private bool _movingLastFrame = false; 

        
        private void Awake() {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _input = GetComponent<ITankInput>();
        }


        private void FixedUpdate() {
            if (_input != null) {
                var direction = _input.GetDirection();
                var velocity = new Vector3();

                var localRot = _spriteRoot.localEulerAngles;
                
                switch (direction) {
                    case InputDirection.Down: {
                        velocity.y = -1f;
                        localRot.z = 180f;        
                        break;
                    }
                        
                    case InputDirection.Up: {
                        velocity.y = 1f;
                        localRot.z = 0f;
                        break;
                    }
                    
                    case InputDirection.Left: {
                        velocity.x = -1f;
                        localRot.z = 90f;
                        break;
                    }
                    
                    case InputDirection.Right: {
                        velocity.x = 1f;
                        localRot.z = 270f;
                        break;
                    }
                    
                    default: break;
                }

                velocity *= _maxSpeed;
                _rigidbody2D.velocity = velocity;
                
                _spriteRoot.localEulerAngles = localRot;

                if (direction != InputDirection.None) {
                    if (!_movingLastFrame) {
                        _animator.Play("Moving");
                    }
                    
                    _movingLastFrame = true;
                } else {
                    if (_movingLastFrame) {
                        _animator.Play("Idle");
                    }
                    
                    _movingLastFrame = false;
                }
            }
        }
    }
}