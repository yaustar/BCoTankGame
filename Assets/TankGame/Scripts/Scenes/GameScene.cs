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

        [SerializeField, BoxGroup("Events")]
        private UnityEvent _spawnPlayerEvent;

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
            Terminal.Shell.AddCommand("AddScore", DebugCommandAddScore, 1, 1);
            Terminal.Shell.AddCommand("DestroyBase", DebugCommandDestroyBase, 0, 0);
            Terminal.Shell.AddCommand("DestroyPlayer", DebugCommandDestroyPlayer, 0, 0);
#endif
        }


        private void OnDestroy() {
#if BUILD_DEBUG
            Terminal.Shell.RemoveCommand("AddScore");
            Terminal.Shell.RemoveCommand("DestroyBase");
            Terminal.Shell.RemoveCommand("DestroyPlayer");
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
            } else {
                _spawnPlayerEvent.Invoke();
            }
        }


        public void OnBaseTropyDestroyed() {
            _gameEndEvent.Invoke();
        }
        
        
        // Private
#if BUILD_DEBUG
        private void DebugCommandAddScore(CommandArg[] args) {
            _playerScoreSvar.value += args[0].Int;
        }


        private void DebugCommandDestroyBase(CommandArg[] args) {
            const string OBJ_NAME = "Base-Trophy";
            DebugDestroyDamagable(OBJ_NAME);

        }


        private void DebugCommandDestroyPlayer(CommandArg[] args) {
            const string OBJ_NAME = "TankPlayer";
            DebugDestroyDamagable(OBJ_NAME);
        }


        private void DebugDestroyDamagable(string objName) {
            GameObject baseTropyObj = GameObject.Find(objName);
            var emptyList = new List<Vector2>();
            if (baseTropyObj != null) {
                var damagables = baseTropyObj.GetComponents<IDamagable>();
                for (int i = 0; i < damagables.Length; ++i) {
                    damagables[i].OnDamageGiven(int.MaxValue, null, emptyList);
                }
            } else {
                Terminal.Log("Can't find damagable GameObject " + objName);
            }
        }
#endif 
    }
}