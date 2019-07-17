using System;
using NaughtyAttributes;
using SmartData.SmartLeaderboardTableData;
using SmartData.SmartScoreEntryData;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace TankGame {
    public class GameOverUi : MonoBehaviour {
        [SerializeField, BoxGroup("References")]
        private GameObject _rootPanelObj;

        [SerializeField, BoxGroup("References")]
        private GameObject _gameOverPanelObj;

        [SerializeField, BoxGroup("References")]
        private GameObject _newHighScorePanelObj;

        [SerializeField, BoxGroup("References")]
        private InputField _highScoreNameInputField;

        [SerializeField, BoxGroup("References")]
        public Button _quitButton;
        
        [SerializeField, BoxGroup("Svars")]
        private LeaderboardTableDataReader _leaderboardTableDataSvar;

        [SerializeField, BoxGroup("Svars")]
        private SmartData.SmartInt.IntReader _playerScoreSvar;

        [SerializeField, BoxGroup("Svars")]
        private ScoreEntryDataWriter _lastHighScoreEntryAchievedSvar;

        [SerializeField]
        private UnityEvent _highScoreAchievedEvent;


        private void Awake() {
            _rootPanelObj.SetActive(false);
            _quitButton.onClick.AddListener(OnExit);
        }
        
        
        // Public callbacks
        public void OnGameEnd() {
            bool haveHighScore = _leaderboardTableDataSvar.value.IsHighScore(_playerScoreSvar.value);
            if (haveHighScore) {
                _newHighScorePanelObj.SetActive(true);
                _gameOverPanelObj.SetActive(false);
            } else {
                _newHighScorePanelObj.SetActive(false);
                _gameOverPanelObj.SetActive(true);
            }
            
            _rootPanelObj.SetActive(true);
        }
        

        // Private 
        private void OnExit() {
            if (_newHighScorePanelObj.activeSelf) {
                var entry = new ScoreEntryData {name = _highScoreNameInputField.text, score = _playerScoreSvar.value};

                _lastHighScoreEntryAchievedSvar.value = entry;

                _highScoreAchievedEvent.Invoke();
            }
        }
    }
}