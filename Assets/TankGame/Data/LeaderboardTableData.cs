using System;
using System.Collections.Generic;

namespace TankGame {
    [Serializable]
    public class LeaderboardTableData {
        public const int MAX_ENTRIES = 10;
        
        
        public List<ScoreEntryData> table = new List<ScoreEntryData>();

        
        public bool IsHighScore(int score) {
            if (score <= 0) {
                return false;
            }
            
            if (table.Count == 0 || table.Count < MAX_ENTRIES) {
                return true;
            }

            var lowestScoreEntry = table[table.Count - 1];
            return lowestScoreEntry.score < score;
        }
    }
}