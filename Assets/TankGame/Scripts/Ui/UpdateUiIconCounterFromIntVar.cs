using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

namespace TankGame {
    public class UpdateUiIconCounterFromIntVar : MonoBehaviour {
        [SerializeField]
        private SmartData.SmartInt.IntReader _intSvar;
        
        [SerializeField]
        private GameObject _iconPrefab;

        [SerializeField]
        private GameObject _iconsParentObject;
        
        [SerializeField]
        private GameObject _iconsPoolContainerObject;


        private ObjectPool _iconPool;
        private int _lastFrameValue = Int32.MaxValue;
        

        private void Awake() {
            _iconPool = new ObjectPool(_iconPrefab, _iconsPoolContainerObject, 5);
        }

        
        void Start() {
            // Very broken, can't unbind the listener!
            //_intSvar.BindListener(OnSvarValueUpdated, true);
        }


        private void Update() {
            var val = _intSvar.value;
            
            if (_lastFrameValue != val) {
                OnSvarValueUpdated(val);
                _lastFrameValue = val;
            }
        }


        // Private
        private void OnSvarValueUpdated(int val) {
            val = Mathf.Max(val, 0);
            
            var iconCount = _iconsParentObject.transform.childCount;
            if (iconCount < val) {
                int numToAdd = val - iconCount;
                for (int i = 0; i < numToAdd; i++) {
                    var obj = _iconPool.GetObject();
                    obj.transform.SetParent(_iconsParentObject.transform);
                    obj.SetActive(true);
                }
            } else if (iconCount > val) {
                int numToRemove = iconCount - val;
                for (int i = 0; i < numToRemove; i++) {
                    var obj = _iconsParentObject.transform.GetChild(_iconsParentObject.transform.childCount - 1)
                        .gameObject;
                    
                    _iconPool.ReturnObject(obj);
                }   
            }
        }
    }
}