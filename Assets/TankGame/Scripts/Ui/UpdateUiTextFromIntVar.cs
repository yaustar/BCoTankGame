using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace TankGame {
    public class UpdateUiTextFromIntVar : MonoBehaviour {
        [SerializeField]
        private SmartData.SmartInt.IntReader _intSvar;


        private Text _text;
        private int _lastFrameValue = Int32.MaxValue;

        
        private void Awake() {
            _text = this.GetComponent<Text>();
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
            _text.text = val.ToString();
        }
    }
}