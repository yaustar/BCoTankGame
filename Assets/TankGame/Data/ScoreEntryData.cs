using System;

namespace TankGame {
    [Serializable]
    public class ScoreEntryData : IComparable<ScoreEntryData> {
        public string name = "";
        public int score = 0;

        public int CompareTo(ScoreEntryData other) {
            return this.score.CompareTo(other.score);
        }
    }
}