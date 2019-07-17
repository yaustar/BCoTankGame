using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TankGame {
    public interface ITankInput {
        // Assume only 4 way direction
        Direction GetDirection(Tank tank);
        bool HasAttemptedFired(Tank tank);
    }
}
