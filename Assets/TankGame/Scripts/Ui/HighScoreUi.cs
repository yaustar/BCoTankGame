using System;
using SmartData.SmartLeaderboardTableData;
using UnityEngine;
using UnityEngine.UI;

namespace TankGame {
    public class HighScoreUi : MonoBehaviour {
        [SerializeField]
        private Text _text;

        [SerializeField]
        private SmartData.SmartInt.IntReader _playerScoreSvar;

        [SerializeField]
        private LeaderboardTableDataReader _leaderboardTableDataSvar;


        private ScoreEntryData _highestScoreEntry;

        
        private void Start() {
            var table = _leaderboardTableDataSvar.value.table;
            if (table != null) {
                _highestScoreEntry = table[0];
            } else {
                _highestScoreEntry = new ScoreEntryData() {name = "", score = 0};
            }
        }


        private void Update() {
            if (_highestScoreEntry.score < _playerScoreSvar.value) {
                _text.text = _playerScoreSvar.value.ToString();
            } else {
                _text.text = _highestScoreEntry.score.ToString();
            }
        }
    }
}