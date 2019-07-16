using UnityEngine;

namespace TankGame {
    public class TankAiInput : MonoBehaviour, ITankInput {
        // Public API
        public Direction GetDirection() {
            // Stub
            return Direction.Down;
        }


        public bool HasAttemptedFired() {
            // Stub
            return false;
        }
    }
}