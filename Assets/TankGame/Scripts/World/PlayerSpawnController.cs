using System;
using System.Collections.Generic;
using UnityEngine;

namespace TankGame {
    public class PlayerSpawnController : MonoBehaviour {
        [SerializeField]
        private SpawnArea _spawnArea;

        [SerializeField]
        private GameObject _playerPrefab;


        private Tank _playerTank;
        

        private void Awake() {
            _playerTank = Instantiate(_playerPrefab).GetComponent<Tank>();
            _playerTank.gameObject.SetActive(false);
            _playerTank.gameObject.name = "TankPlayer";
            
            OnPlayerSpawn();
        }


        // Public callbacks
        public void OnPlayerSpawn() {
            var spawnAreaPos = _spawnArea.GetCollider2D().transform.position;
            var points = new List<Vector2>();
            points.Add(new Vector2(spawnAreaPos.x, spawnAreaPos.y));
            
            // Destroy all the objects in the area
            var objs = _spawnArea.GetObjectsInArea();
            for (int i = 0; i < objs.Count; ++i) {
                var obj = objs[i];
                if (obj.activeSelf) {
                    var damagables = obj.GetComponents<IDamagable>();
                    for (int j = 0; j < damagables.Length; ++j) {
                        damagables[i].OnDamageGiven(Int32.MaxValue, null, points);
                    }
                }
            }
            
            // Make tank face the right direction
            _playerTank.Restart(_spawnArea.GetDefaultDirection(), null);

            // Move the player to the spawn area
            _playerTank.transform.position = _spawnArea.transform.position;
            
            _playerTank.gameObject.SetActive(true);
        }
    }
}