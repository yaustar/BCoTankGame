using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace TankGame {
    public class DestructibleTileMap : MonoBehaviour, IDamagable {
        [SerializeField]
        private Tilemap _tilemap;

        [SerializeField]
        private Grid _grid;
        
        
        // Public callbacks
        public void OnDamageGiven(int damage, GiveDamage damageGiver, List<Vector2> damagePoints) {
            // Use the velocity as the direction and create a new world point
            // that is slightly ahead of the actual contact point so that we are 
            // actually in a tile
            const float PROJECTION_LENGTH = 0.1f;
            var otherPos3 = damageGiver.transform.position;
            var otherPos2 = new Vector2(otherPos3.x, otherPos3.y); 

            for (int i = 0; i < damagePoints.Count; ++i) {
                var damagePoint = damagePoints[i];
                var direction = damagePoint - otherPos2;
                direction.Normalize();
                    
                var worldPoint = damagePoint + (direction * PROJECTION_LENGTH); 

                Vector3Int gridPos = _grid.WorldToCell(worldPoint);
                _tilemap.SetTile(gridPos, null);

                // TODO Polish pass spawn some explosion here
            }
        }
    }
}