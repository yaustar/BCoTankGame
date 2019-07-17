using UnityEngine;

namespace TankGame {
    public class WeaponBomb : WeaponBase {
        
        public override void Fire(Direction direction, Transform owner) {
            if (!CanFire()) {
                return;
            }
            
            var bombObj = _pool.GetObject();
            bombObj.transform.SetParent(owner.parent);
            var bomb = bombObj.GetComponent<Bomb>();
            bombObj.transform.position = transform.position;
            
            bomb.Restart((GameObject obj) => {
                // Safety if weapon is removed from the game for whatever reason
                if (this == null) {
                    Destroy(obj);
                } else {
                    _pool.ReturnObject(obj);
                }
            });
            
            bombObj.SetActive(true);

            _secsTimeSinceFired = 0f;
        }
    }
}