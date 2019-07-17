using System;
using NaughtyAttributes;
using UnityEngine;

namespace TankGame {
    public abstract class WeaponBase : MonoBehaviour {
        [SerializeField, BoxGroup("References")]
        protected GameObject _prefab;

        [SerializeField, BoxGroup("References")]
        protected Transform _spawnTransform;
        
        [SerializeField, BoxGroup("Properties")]
        protected float _reloadTimeSecs;
        
        [SerializeField, BoxGroup("Properties")]
        protected int _poolStartCount = 5;
        
        
        // Should this be managed by an external manager/controller?
        // What happens when the tank dies if there are still bullets 
        // on the screen?
        protected ObjectPool _pool;
        protected float _secsTimeSinceFired = float.MaxValue;

        
        private void Awake() {
            _pool = new ObjectPool(_prefab, gameObject, _poolStartCount);
        }


        private void OnEnable() {
            _secsTimeSinceFired = float.MaxValue;
        }


        private void Update() {
            _secsTimeSinceFired += Time.deltaTime;
        }


        // Public API
        public abstract void Fire(Direction direction, Transform owner);
        
        
        public bool CanFire() {
            return _secsTimeSinceFired >= _reloadTimeSecs;
        }


        public float GetReloadTimeSecs() {
            return _reloadTimeSecs;
        }


        public float GetSecSinceLastFired() {
            return _secsTimeSinceFired;
        }
    }
}