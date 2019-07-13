using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

#if BUILD_DEBUG
using CommandTerminal;
#endif



namespace TankGame {
    public class GameScene : MonoBehaviour {
        [SerializeField]
        private UnityEvent _showMainMenuEvent;
        
        [SerializeField]
        private UnityEvent _showLeaderboardEvent;

        [SerializeField]
        private SmartData.SmartInt.IntReader  _playerLivesMaxConst;
        
        [SerializeField]
        private SmartData.SmartInt.IntWriter _playerLivesCountSvar;


        // Start is called before the first frame update
        void Awake() {
            _playerLivesCountSvar.value = _playerLivesMaxConst.value;
        }


        void Start() {
#if BUILD_DEBUG
            Terminal.Shell.AddCommand("ReducePlayerLives", DebugCommandReducePlayerLives, 1, 1);
#endif
        }


        private void OnDestroy() {
#if BUILD_DEBUG
            Terminal.Shell.RemoveCommand("ReducePlayerLives");
#endif            
        }

        
        // Update is called once per frame
        void Update() {
            // Stub
            if (Input.GetKeyDown(KeyCode.Space)) {
                _showMainMenuEvent.Invoke();
            }
        }
        
        
        // Public callbacks
        public void OnPlayerDeath() {
            _playerLivesCountSvar.value -= 1;
            if (_playerLivesCountSvar.value == 0) {
                // Game over stub: Go back to main menu
                _showMainMenuEvent.Invoke();
            }
        }
        
        
        // Private
#if BUILD_DEBUG
        private void DebugCommandReducePlayerLives(CommandArg[] args) {
            for (int i = 0; i < args[0].Int && _playerLivesCountSvar.value > 0; ++i) {
                OnPlayerDeath();
            }
        } 
#endif 
    }
}