using System;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

namespace TankGame {
    public class AiSensor : MonoBehaviour {
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
        public List<GameObject> GetObjectsInSensor() {
            return _objectsInArea;
        }
    }
}