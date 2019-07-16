using System.Collections.Generic;
using UnityEngine;

namespace TankGame {
    public interface IDamagable {
        void OnDamageGiven(int damage, GiveDamage damageGiver, List<Vector2> damagePoints);
    }
}