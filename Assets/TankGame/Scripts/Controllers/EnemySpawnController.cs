using System;
using System.Collections.Generic;
using NaughtyAttributes;
using SmartData.SmartLevelState;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace TankGame {
    public class EnemySpawnController : MonoBehaviour {
        [SerializeField, BoxGroup("Reference")]
        private List<SpawnArea> _spawnAreas;

        [SerializeField, BoxGroup("Reference")]
        private GameObject _enemyPrefab;

        [SerializeField, BoxGroup("Svars")]
        private LevelStateReader _levelStateSvar;

        [FormerlySerializedAs("_maxEnemiesToSpawnSvar")] [SerializeField, BoxGroup("Svars")]
        private SmartData.SmartInt.IntReader _maxEnemiesThisLevelToSpawnSvar;

        [SerializeField, MinMaxSlider(0f, 10f)]
        private Vector2 _randomSpawnTimeRange;
        
        
        private ObjectPool _objectPool;
        private int _enemiesSpawnedThisLevel = 0;
        private float _secsToNextSpawn = 0f;
        

        private void Awake() {
            _objectPool = new ObjectPool(_enemyPrefab, gameObject, 10);
        }


        private void Update() {
            _secsToNextSpawn -= Time.deltaTime;
            
            if (_levelStateSvar.value == LevelState.InProgress) {
                if (_secsToNextSpawn <= 0f && _enemiesSpawnedThisLevel < _maxEnemiesThisLevelToSpawnSvar.value) {
                    AttemptSpawn();
                }
            }
        }


        // Public callbacks
        public void OnLevelStarted() {
            _enemiesSpawnedThisLevel = 0;
            _secsToNextSpawn = 0f;
        }
        
        
        // Private 
        private void AttemptSpawn() {
            var randomSpawnArea = _spawnAreas[Random.Range(0, _spawnAreas.Count)];
            
            var spawnAreaPos = randomSpawnArea.GetCollider2D().transform.position;
            var points = new List<Vector2>();
            points.Add(new Vector2(spawnAreaPos.x, spawnAreaPos.y));
            
            // Is spawn area empty?
            var objs = randomSpawnArea.GetObjectsInArea();
            if (objs.Count ==  0) {
                var tankObj = _objectPool.GetObject();
                var tank = tankObj.GetComponent<Tank>();
                
                // Make tank face the right direction
                tank.Restart(randomSpawnArea.GetDefaultDirection(), (GameObject obj) => {
                    // Safety if this object is removed from the game for whatever reason
                    // Tanks would be pool managed but just in case
                    if (this == null) {
                        Destroy(obj);
                    } else {
                        _objectPool.ReturnObject(obj);
                    }
                });
                
                tankObj.transform.SetParent(transform.parent);

                // Move it to the spawn area
                tank.transform.position = randomSpawnArea.transform.position;
                
                tank.gameObject.SetActive(true);

                _secsToNextSpawn = Random.Range(_randomSpawnTimeRange.x, _randomSpawnTimeRange.y);

                _enemiesSpawnedThisLevel += 1;
            }
        }
    }
    
}