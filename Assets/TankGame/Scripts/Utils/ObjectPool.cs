using System.Collections.Generic;
using UnityEngine;

namespace TankGame {
    public class ObjectPool {
        private List<GameObject> _pool;
        private GameObject _prefab;
        private GameObject _parent;
        

        public ObjectPool(GameObject prefab, GameObject parent, int startingCount) {
            _prefab = prefab;
            _parent = parent;
            
            _pool = new List<GameObject>();
            for (int i = 0; i < startingCount; ++i) {
                var obj = GameObject.Instantiate(_prefab, _parent.transform, false);
                obj.SetActive(false);
                _pool.Add(obj);
            }
        }


        public GameObject GetObject() {
            if (_pool.Count == 0) {
                return GameObject.Instantiate(_prefab, _parent.transform, false);
            } else {
                var obj = _pool[0];
                _pool.RemoveAt(0);

                return obj;
            }
        }


        public void ReturnObject(GameObject obj) {
            obj.transform.SetParent(_parent.transform);
            obj.SetActive(false);
            if (!_pool.Contains(obj)) {
                _pool.Add(obj);
            }
        }
    }
}