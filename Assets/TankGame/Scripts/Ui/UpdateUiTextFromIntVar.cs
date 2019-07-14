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
        
        
        private void Awake() {
            _text = this.GetComponent<Text>();
        }

        
        private void Start() {
            _intSvar.BindListener(OnSvarValueUpdated, true);
        }
        
        
        // Private
        private void OnSvarValueUpdated(int val) {
            _text.text = val.ToString();
        }
    }
}