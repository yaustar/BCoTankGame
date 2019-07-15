using System;
using UnityEngine;

namespace TankGame {
    public class Tank : MonoBehaviour {
        [SerializeField]
        private float _maxSpeed;

        [SerializeField]
        private Animator _animator;
        

        private ITankInput _input;
        private bool _movingLastFrame = false; 

        private void Awake() {
            _input = GetComponent<ITankInput>();
        }


        private void Update() {
            if (_input != null) {
                var direction = _input.GetDirection();
                var moveDelta = new Vector3();
                var trans = transform;

                var localRot = trans.localEulerAngles;
                
                switch (direction) {
                    case InputDirection.Down: {
                        moveDelta.y = -1f;
                        localRot.z = 180f;        
                        break;
                    }
                        
                    case InputDirection.Up: {
                        moveDelta.y = 1f;
                        localRot.z = 0f;
                        break;
                    }
                    
                    case InputDirection.Left: {
                        moveDelta.x = -1f;
                        localRot.z = 90f;
                        break;
                    }
                    
                    case InputDirection.Right: {
                        moveDelta.x = 1f;
                        localRot.z = 270f;
                        break;
                    }
                    
                    default: break;
                }
                
                var pos = trans.position;
                pos += (moveDelta * (_maxSpeed * Time.deltaTime));
                trans.position = pos;

                trans.localEulerAngles = localRot;

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