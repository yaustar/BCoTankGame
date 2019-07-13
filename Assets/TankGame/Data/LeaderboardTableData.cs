using System;
using System.Collections.Generic;

namespace TankGame {
    [Serializable]
    public class LeaderboardTableData {
        public List<ScoreEntryData> table = new List<ScoreEntryData>();
    }
}