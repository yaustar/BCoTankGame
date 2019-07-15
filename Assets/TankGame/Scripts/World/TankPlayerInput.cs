using UnityEngine;

namespace TankGame {
    public class TankPlayerInput : MonoBehaviour, ITankInput {
        private static readonly float DEAD_ZONE_SQUARED = 0.5f * 0.5f;
        
        
        // Public API
        public InputDirection GetDirection() {
            // Use the standard Unity input for the moment
            // If we have a button remapper in the game, we can change this logic
            
            // Get the highest valued axis
            var joyAxis = new Vector2();
            joyAxis.x = Input.GetAxis("Horizontal");
            joyAxis.y = Input.GetAxis("Vertical");

            if (joyAxis.sqrMagnitude > DEAD_ZONE_SQUARED) {
                // Which axis has more bias
                if (Mathf.Abs(joyAxis.x) > Mathf.Abs(joyAxis.y)) {
                    // Horizontal
                    if (joyAxis.x > 0) {
                        return InputDirection.Right;
                    } else {
                        return InputDirection.Left;
                    }
                } else {
                    // Vertical
                    if (joyAxis.y > 0) {
                        return InputDirection.Up;
                    } else {
                        return InputDirection.Down;
                    }
                }
            }

            return InputDirection.None;
        }


        public bool HasAttemptedFired() {
            return false;
        }
    }
}