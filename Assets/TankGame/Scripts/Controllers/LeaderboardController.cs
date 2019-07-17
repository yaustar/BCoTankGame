using System;
using System.Collections;
using System.Collections.Generic;
using SmartData.SmartLeaderboardTableData;
using SmartData.SmartScoreEntryData;
using UnityEngine;

#if BUILD_DEBUG
using CommandTerminal;
#endif


namespace TankGame {
    public class LeaderboardController : MonoBehaviour {
        private static readonly string PREF_LEADERBOARD_TABLE_DATA = "LeaderboardTableData";
        
        [SerializeField]
        private LeaderboardTableDataWriter _leaderboardTableDataSvar;

        [SerializeField]
        private ScoreEntryDataReader _lastHighScoreEntryAchievedSvar;


        private void Awake() {
            var json = PlayerPrefs.GetString(PREF_LEADERBOARD_TABLE_DATA, "");
            _leaderboardTableDataSvar.value = JsonUtility.FromJson<LeaderboardTableData>(json) ?? new LeaderboardTableData();

            // Have at least one default entry on the leaderboard
            var table = _leaderboardTableDataSvar.value.table;
            if (table.Count == 0) {
                AddDefaultScoreEntry();
            }
            
            table.Sort();

#if BUILD_DEBUG
            Terminal.Shell.AddCommand("AddHighScore", DebugCommandAddHighScore, 2, 2);
            Terminal.Shell.AddCommand("ClearLeaderboardTable", DebugCommandClearTable, 0, 0);
#endif
        }

        
        private void OnDestroy() {
#if BUILD_DEBUG
            Terminal.Shell.RemoveCommand("AddHighScore");
            Terminal.Shell.RemoveCommand("ClearLeaderboardTable");
#endif            
        }
        
        
        // Public callbacks 
        public void OnNewHighScoreAchieved() {
            AddScoreToTable(_lastHighScoreEntryAchievedSvar.value);
        }


        // Private
        private void AddScoreToTable(ScoreEntryData scoreEntry) {
            const int MAX_ENTRIES = LeaderboardTableData.MAX_ENTRIES;
            
            var table = _leaderboardTableDataSvar.value.table;
            table.Add(new ScoreEntryData() {name = scoreEntry.name, score = scoreEntry.score});
            table.Sort();
            
            // Scores are sorted descending
            if (table.Count > MAX_ENTRIES) {
                table.RemoveRange(MAX_ENTRIES, table.Count - MAX_ENTRIES);
            }

            SaveToDevice();
        }


        private void SaveToDevice() {
            PlayerPrefs.SetString(PREF_LEADERBOARD_TABLE_DATA, JsonUtility.ToJson(_leaderboardTableDataSvar.value));
            PlayerPrefs.Save();
        }


        private void AddDefaultScoreEntry() {
            var table = _leaderboardTableDataSvar.value.table;
            AddScoreToTable(new ScoreEntryData(){name = "Bravo Company", score = 100});
        }
        
        
#if BUILD_DEBUG
        private void DebugCommandAddHighScore(CommandArg[] args) {
            var entry = new ScoreEntryData() {
                name = args[0].String, score = args[1].Int
            };
            
            AddScoreToTable(entry);
        }


        private void DebugCommandClearTable(CommandArg[] args) {
            var table = _leaderboardTableDataSvar.value.table;
            table.Clear();
            AddDefaultScoreEntry();
            SaveToDevice();
        }
#endif // BUILD_DEBUG
    }
}