using System;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.UI;

namespace TankGame {
    public class WeaponUi : MonoBehaviour {
        [SerializeField, BoxGroup("Gun")]
        private GameObject _gunObj;

        [SerializeField, BoxGroup("Gun")]
        private Image _gunReloadSlider;
        
        [SerializeField, BoxGroup("Bomb")]
        private GameObject _bombObj;

        [SerializeField, BoxGroup("Bomb")]
        private Image _bombReloadSlider;
        
        private IGun _gun;
        private IBomb _bomb;


        private void Awake() {
            _gun = _gunObj.GetComponent<IGun>();
            _bomb = _bombObj.GetComponent<IBomb>();
        }


        private void Update() {
            var gunAlpha = Mathf.Min(_gun.GetSecSinceLastFired() / _gun.GetGunReloadTimeSecs(), 1f);
            gunAlpha = 1f - gunAlpha;

            _gunReloadSlider.fillAmount = gunAlpha;
            
            var bombAlpha = Mathf.Min(_bomb.GetSecSinceLastBombed() / _bomb.GetBombReloadTimeSecs(), 1f);
            bombAlpha = 1f - bombAlpha;

            _bombReloadSlider.fillAmount = bombAlpha;
        }
    }
}