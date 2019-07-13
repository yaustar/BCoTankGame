using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


namespace TankGame {
    public class LeaderboardController : MonoBehaviour {
        [SerializeField]
        private UnityEvent _showMainMenuEvent;
        
        
        // Start is called before the first frame update
        void Start() {

        }

        // Update is called once per frame
        void Update() {
            if (Input.GetKeyDown(KeyCode.Space)) {
                _showMainMenuEvent.Invoke();
            }
        }
    }
}
