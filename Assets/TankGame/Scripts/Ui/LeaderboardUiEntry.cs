using UnityEngine;
using UnityEngine.UI;

namespace TankGame {
    public class LeaderboardUiEntry : MonoBehaviour {
        [SerializeField]
        private Text _positionText;
        
        [SerializeField]
        private Text _playerNameText;
        
        [SerializeField]
        private Text _scoreText;
        
        
        // Public API
        public void Set(int position, ScoreEntryData scoreEntryData) {
            _positionText.text = position.ToString();
            _playerNameText.text = scoreEntryData.name;
            _scoreText.text = scoreEntryData.score.ToString();
        }
    }

}