using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using SmartData.SmartLevelState;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
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

        [SerializeField, BoxGroup("Events")]
        private UnityEvent _newLevelStartedEvent;

        
        [SerializeField, BoxGroup("Svars")]
        private SmartData.SmartInt.IntReader  _playerLivesMaxConst;
        
        [SerializeField, BoxGroup("Svars")]
        private SmartData.SmartInt.IntWriter _playerLivesCountSvar;

        [SerializeField, BoxGroup("Svars")]
        private SmartData.SmartInt.IntWriter _playerScoreSvar;

        [SerializeField, BoxGroup("Svars")]
        private LevelStateWriter _levelStateSvar;

        [FormerlySerializedAs("_maxEnemiesToSpawnSvar")] [SerializeField, BoxGroup("Svars")]
        private SmartData.SmartInt.IntWriter _maxEnemiesThisLevelToSpawnSvar;
        
        [SerializeField, BoxGroup("Svars")]
        private SmartData.SmartInt.IntWriter _enemiesLeftThisLevelSvar;

        [SerializeField]
        private int _pointsPerTankDestroyed = 10;
        
        
        void Awake() {
            Time.timeScale = 1f;
        }


        void Start() {
            _playerLivesCountSvar.value = _playerLivesMaxConst.value;
            _playerScoreSvar.value = 0;

            _levelStateSvar.value = LevelState.ShowIntro;
            StartCoroutine(StartLevelInXSecs());
            
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
        
        
        // Public callbacks
        public void OnPlayerDeath() {
            _playerLivesCountSvar.value -= 1;
            if (_playerLivesCountSvar.value == 0) {
                OnGameEnd();
            } else {
                _spawnPlayerEvent.Invoke();
            }
        }


        public void OnEnemyDeath() {
            _playerScoreSvar.value += _pointsPerTankDestroyed;
            _enemiesLeftThisLevelSvar.value = Mathf.Max(_enemiesLeftThisLevelSvar.value - 1, 0);
        }


        public void OnBaseTrophyDestroyed() {
            OnGameEnd();
        }
        
        
        // Private
        private void OnGameEnd() {
            Time.timeScale = 0f;
            _gameEndEvent.Invoke();
        }


        private IEnumerator StartLevelInXSecs() {
            _enemiesLeftThisLevelSvar.value = _maxEnemiesThisLevelToSpawnSvar.value;
            
            var wait = new WaitForSeconds(1f);
            yield return wait;

            _levelStateSvar.value = LevelState.InProgress;
            _newLevelStartedEvent.Invoke();
        }
        
        
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