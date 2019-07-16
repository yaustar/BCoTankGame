using System;
using UnityEngine;
using UnityEngine.UI;

namespace TankGame {
    public class WeaponUi : MonoBehaviour {
        [SerializeField]
        private GameObject _gunObj;

        [SerializeField]
        private Image _gunReloadSlider;
        
        
        private IGun _gun;


        private void Awake() {
            _gun = _gunObj.GetComponent<IGun>();
        }


        private void Update() {
            var gunAlpha = Mathf.Min(_gun.GetSecSinceLastFired() / _gun.GetReloadTimeSecs(), 1f);
            gunAlpha = 1f - gunAlpha;

            _gunReloadSlider.fillAmount = gunAlpha;
        }
    }
}