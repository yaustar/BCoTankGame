using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;

#if BUILD_DEBUG
using CommandTerminal;
#endif



namespace TankGame {
    public class GameScene : MonoBehaviour {
        [SerializeField, BoxGroup("Events")]
        private UnityEvent _showMainMenuEvent;
        
        [SerializeField, BoxGroup("Events")]
        private UnityEvent _showLeaderboardEvent;

        [SerializeField, BoxGroup("Svars")]
        private SmartData.SmartInt.IntReader  _playerLivesMaxConst;
        
        [SerializeField, BoxGroup("Svars")]
        private SmartData.SmartInt.IntWriter _playerLivesCountSvar;

        [SerializeField, BoxGroup("Svars")]
        private SmartData.SmartInt.IntWriter _playerScoreSvar;


        void Awake() {
        }


        void Start() {
            _playerLivesCountSvar.value = _playerLivesMaxConst.value;
            _playerScoreSvar.value = 0;
            
#if BUILD_DEBUG
            Terminal.Shell.AddCommand("ReduceLives", DebugCommandReducePlayerLives, 1, 1);
            Terminal.Shell.AddCommand("AddScore", DebugCommandAddScore, 1, 1);
#endif
        }


        private void OnDestroy() {
#if BUILD_DEBUG
            Terminal.Shell.RemoveCommand("ReducePlayerLives");
            Terminal.Shell.RemoveCommand("AddScore");
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


        public void OnBaseTropyDestroyed() {
            // TODO Show end screen 
            _showMainMenuEvent.Invoke();
        }
        
        
        // Private
#if BUILD_DEBUG
        private void DebugCommandReducePlayerLives(CommandArg[] args) {
            for (int i = 0; i < args[0].Int && _playerLivesCountSvar.value > 0; ++i) {
                OnPlayerDeath();
            }
        }


        private void DebugCommandAddScore(CommandArg[] args) {
            _playerScoreSvar.value += args[0].Int;
        }
#endif 
    }
}