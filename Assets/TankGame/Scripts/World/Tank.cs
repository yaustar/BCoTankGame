using System;
using UnityEngine;

namespace TankGame {
    public class Tank : MonoBehaviour {
        [SerializeField]
        private float _maxSpeed;


        private ITankInput _input;


        private void Awake() {
            _input = GetComponent<ITankInput>();
        }


        private void Update() {
            if (_input != null) {
                var direction = _input.GetDirection();
                var moveDelta = new Vector3();
                
                switch (direction) {
                    case InputDirection.Down: {
                        moveDelta.y = -1f;        
                        break;
                    }
                        
                    case InputDirection.Up: {
                        moveDelta.y = 1f;
                        break;
                    }
                    
                    case InputDirection.Left: {
                        moveDelta.x = -1f;
                        break;
                    }
                    
                    case InputDirection.Right: {
                        moveDelta.x = 1f;        
                        break;
                    }
                    
                    default: break;
                }

                var trans = transform;
                var pos = trans.position;
                pos += (moveDelta * (_maxSpeed * Time.deltaTime));
                trans.position = pos;
            }
        }
    }
}