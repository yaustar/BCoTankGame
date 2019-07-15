using System;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace TankGame {
    public class DestructibleTileMap : MonoBehaviour {
        [SerializeField]
        private Tilemap _tilemap;

        [SerializeField]
        private Grid _grid;
        
        
        private void OnCollisionEnter2D(Collision2D other) {
            var giveDamage = other.gameObject.GetComponent<GiveDamage>();
            if (giveDamage) {
                // Use the velocity as the direction and create a new world point
                // that is slightly ahead of the actual contact point so that we are 
                // actually in a tile
                const float PROJECTION_LENGTH = 0.1f;
                var otherPos3 = other.transform.position;
                var otherPos2 = new Vector2(otherPos3.x, otherPos3.y); 

                var contactCount = other.contactCount;
                for (int i = 0; i < contactCount; ++i) {
                    var contact = other.GetContact(i);
                    var direction = contact.point - otherPos2;
                    direction.Normalize();
                    
                    var worldPoint = contact.point + (direction * PROJECTION_LENGTH); 

                    Vector3Int gridPos = _grid.WorldToCell(worldPoint);
                    _tilemap.SetTile(gridPos, null);

                    // TODO Polish pass spawn some explosion here
                }
            }
        }
    }
}