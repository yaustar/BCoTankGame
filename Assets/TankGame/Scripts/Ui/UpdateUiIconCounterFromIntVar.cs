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


        private void Awake() {
            _iconPool = new ObjectPool(_iconPrefab, _iconsPoolContainerObject, 5);
        }

        
        void Start() {
            _intSvar.BindListener(OnSvarValueUpdated, true);
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