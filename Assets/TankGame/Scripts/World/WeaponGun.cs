using UnityEngine;

namespace TankGame {
    public class WeaponGun : WeaponBase {
        
        public override void Fire(Direction direction, Transform owner) {
            if (!CanFire()) {
                return;
            }
            
            var bulletObj = _pool.GetObject();
            bulletObj.transform.SetParent(owner.parent);
            var bullet = bulletObj.GetComponent<Bullet>();
            bulletObj.transform.position = _spawnTransform.position;

            bulletObj.SetActive(true);

            bullet.Restart(direction, (GameObject obj) => {
                // Safety if weapon is removed from the game for whatever reason
                if (this == null) {
                    Destroy(obj);
                } else {
                    _pool.ReturnObject(obj);
                }
            });

            _secsTimeSinceFired = 0f;
        }
    }
}