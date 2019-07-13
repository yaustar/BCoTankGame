using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TankGame {
    public class DontDestroyOnLoad : MonoBehaviour {
        void Awake() {
            DontDestroyOnLoad(this.gameObject);
        }
    }
}
