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
        
        [SerializeField, BoxGroup("Events")]
        private UnityEvent _gameEndEvent;

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
            Terminal.Shell.AddCommand("DestroyBase", DebugCommandDestroyBase, 0, 0);
#endif
        }


        private void OnDestroy() {
#if BUILD_DEBUG
            Terminal.Shell.RemoveCommand("ReducePlayerLives");
            Terminal.Shell.RemoveCommand("AddScore");
            Terminal.Shell.RemoveCommand("DestroyBase");
#endif            
        }

        
        // Update is called once per frame
        void Update() {
 
        }
        
        
        // Public callbacks
        public void OnPlayerDeath() {
            _playerLivesCountSvar.value -= 1;
            if (_playerLivesCountSvar.value == 0) {
                _gameEndEvent.Invoke();
            }
        }


        public void OnBaseTropyDestroyed() {
            _gameEndEvent.Invoke();
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


        private void DebugCommandDestroyBase(CommandArg[] args) {
            const string OBJ_NAME = "Base-Trophy";
            GameObject baseTropyObj = GameObject.Find(OBJ_NAME);
            var emptyList = new List<Vector2>();
            if (baseTropyObj != null) {
                var damagables = baseTropyObj.GetComponents<IDamagable>();
                for (int i = 0; i < damagables.Length; ++i) {
                    damagables[i].OnDamageGiven(int.MaxValue, null, emptyList);
                }
            } else {
                Terminal.Log("Can't find Base Trophy GameObject " + OBJ_NAME);
            }
        }
#endif 
    }
}