using System;

namespace TankGame {
    [Serializable]
    public class ScoreEntryData : IComparable<ScoreEntryData> {
        public string name = "";
        public int score = 0;

        public int CompareTo(ScoreEntryData other) {
            // Sort descending
            return other.score.CompareTo(this.score);
        }
    }
}