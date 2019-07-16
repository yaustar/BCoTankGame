using System;
using UnityEngine;
using UnityEngine.UI;

namespace TankGame {
    public class HealthUi : MonoBehaviour {
        [SerializeField]
        private GameObject _healthObj;

        [SerializeField]
        private Slider _slider;
        
        
        private IHealth _health;


        private void Awake() {
            _health = _healthObj.GetComponent<IHealth>();
        }


        private void Update() {
            var healthAlpha = Mathf.Min((float)_health.GetHealthCurrent() / (float)_health.GetHealthMax(), 1f);
            _slider.value = healthAlpha;
        }
    }
}