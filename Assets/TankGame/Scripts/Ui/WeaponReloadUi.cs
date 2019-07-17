using UnityEngine;
using UnityEngine.UI;

namespace TankGame {
    public class WeaponReloadUi : MonoBehaviour {
        [SerializeField]
        private WeaponBase _weapon;
        
        [SerializeField]
        private Image _slider;

        
        private void Update() {
            var alpha = Mathf.Min(_weapon.GetSecSinceLastFired() / _weapon.GetReloadTimeSecs(), 1f);
            alpha = 1f - alpha;

            _slider.fillAmount = alpha;
        }
    }
}