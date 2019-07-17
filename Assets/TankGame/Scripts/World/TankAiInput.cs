using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace TankGame {
    public class TankAiInput : MonoBehaviour, ITankInput {
        [SerializeField]
        private AiSensor _frontSensor;
        
        [SerializeField]
        private AiSensor _backSensor;
        
        [SerializeField]
        private AiSensor _leftSensor;
        
        [SerializeField]
        private AiSensor _rightSensor;


        private Direction _direction = Direction.None;
        
        // Stops the tank from spinning around in open spaces
        private float _secsSinceLastChangedDirection = 0f;


        private void Update() {
            _secsSinceLastChangedDirection += Time.deltaTime;
        }
        

        // Public API
        public Direction GetDirection(Tank tank) {
            // This is very dumb AI that moves around randomly
            // And only shoots if there is something of interest in front of it
            bool canGoForward = _frontSensor.GetObjectsInSensor().Count == 0;
            bool canGoLeft = _leftSensor.GetObjectsInSensor().Count == 0;
            bool canGoRight = _rightSensor.GetObjectsInSensor().Count == 0;
            bool canGoBack = _backSensor.GetObjectsInSensor().Count == 0;

            var localDirection = Direction.None;
            
            // Favour going forward and only go back if we have no other choice

            if (canGoForward) {
                localDirection = Direction.Up;
            }

            if ((canGoLeft || canGoRight) && (_secsSinceLastChangedDirection > 3f || !canGoForward)) {
                bool goSidewards = Random.Range(0f, 1f) < 0.2f;
                if (goSidewards) {
                    var minBounds = canGoLeft ? -1f : 0f;
                    var maxBounds = canGoRight ? 1f : 0f;
                    var randomSideRoll = Random.Range(minBounds, maxBounds);

                    if (randomSideRoll > 0f) {
                        localDirection = Direction.Right;
                    } else {
                        localDirection = Direction.Left;
                    }

                    _secsSinceLastChangedDirection = 0f;
                }
            } 
              
            if (canGoBack && localDirection == Direction.None) {
                localDirection = Direction.Down;
            }
            
            if (localDirection == Direction.None) {
                _direction = Direction.None;
            } else {
                _direction = DirectionHelper.LocalToWorld(tank.GetDirection(), localDirection);
            }

            return _direction;
        }


        public bool HasAttemptedFired(Tank tank) {
            // Only fire if there is something in front of use of interest
            var bulletSpawnPos = tank.GetBulletSpawnPosition();
            var rayStart = new Vector2(bulletSpawnPos.x, bulletSpawnPos.y);
            var direction = new Vector2();
            switch (tank.GetDirection()) {
                case Direction.Down: {
                    direction.x = 0f;
                    direction.y = -1f;
                    break;
                }
                
                case Direction.Up: {
                    direction.x = 0f;
                    direction.y = 1f;
                    break;
                }
                
                case Direction.Left: {
                    direction.x = -1f;
                    direction.y = 0f;
                    break;
                }
                
                case Direction.Right: {
                    direction.x = 1f;
                    direction.y = 0f;
                    break;
                }
                
                default: break;
            }
            
            var hits = Physics2D.RaycastAll(rayStart, direction);
            for (int i = 0; i < hits.Length; ++i) {
                var hit = hits[i];
                if (hit.transform.GetComponent<AiInterest>()) {
                    return true;
                }
            }

            return false;
        }
    }
}