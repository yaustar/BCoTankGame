using System.Collections.Generic;
using UnityEngine;

namespace TankGame {
    public class SpawnArea : MonoBehaviour {
        [SerializeField]
        private Collider2D _collider2D;

        [SerializeField]
        private Direction _defaultDirection;


        private List<GameObject> _objectsInArea = new List<GameObject>();
        
        
        private void OnTriggerEnter2D (Collider2D other) {
            if (!_objectsInArea.Contains(other.gameObject)) {
                _objectsInArea.Add(other.gameObject);
            }
        }
 
        private void OnTriggerExit2D (Collider2D other) {
            _objectsInArea.Remove(other.gameObject);
        }
        
        
        // Public API
        public Collider2D GetCollider2D() {
            return _collider2D;
        }


        public Direction GetDefaultDirection() {
            return _defaultDirection;
        }


        public List<GameObject> GetObjectsInArea() {
            return _objectsInArea;
        }
    }
}