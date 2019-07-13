using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace TankGame {
    public class GameController : MonoBehaviour {
        [SerializeField]
        private UnityEvent _showMainMenuEvent;
        
        [SerializeField]
        private UnityEvent _showLeaderboardEvent;

        
        // Start is called before the first frame update
        void Start() {

        }

        // Update is called once per frame
        void Update() {
            // Stub
            if (Input.GetKeyDown(KeyCode.Space)) {
                _showMainMenuEvent.Invoke();
            }
        }
    }
}