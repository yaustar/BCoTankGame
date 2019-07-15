using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TankGame {
    public enum InputDirection {
        None,
        Up,
        Down, 
        Left,
        Right
    }
    
    
    public interface ITankInput {
        // Assume only 4 way direction
        InputDirection GetDirection();
        bool HasAttemptedFired();
    }
}
