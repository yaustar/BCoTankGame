using System;
using System.Collections;
using System.Collections.Generic;
using CommandTerminal;
using SmartData.SmartLeaderboardTableData;
using UnityEngine;


namespace TankGame {
    public class LeaderboardController : MonoBehaviour {
        private static readonly string PREF_LEADERBOARD_TABLE_DATA = "LeaderboardTableData";
        
        [SerializeField]
        private LeaderboardTableDataWriter _leaderboardTableDataSvar;


        private void Awake() {
            var json = PlayerPrefs.GetString(PREF_LEADERBOARD_TABLE_DATA, "");
            _leaderboardTableDataSvar.value = JsonUtility.FromJson<LeaderboardTableData>(json) ?? new LeaderboardTableData();
            
#if BUILD_DEBUG
            Terminal.Shell.AddCommand("AddHighScore", DebugCommandAddHighScore, 2, 2);
            Terminal.Shell.AddCommand("ClearLeaderboardTable", DebugCommandClearTable, 0, 0);
#endif // BUILD_DEBUG           
            
        }
        
        
        // Private
        private void AddScoreToTable(ScoreEntryData scoreEntry) {
            const int MAX_ENTRIES = 10;
            
            var table = _leaderboardTableDataSvar.value.table;
            table.Add(scoreEntry);
            table.Sort();
            if (table.Count > MAX_ENTRIES) {
                table.RemoveRange(MAX_ENTRIES, table.Count - MAX_ENTRIES);
            }

            SaveToDevice();
        }


        private void SaveToDevice() {
            PlayerPrefs.SetString(PREF_LEADERBOARD_TABLE_DATA, JsonUtility.ToJson(_leaderboardTableDataSvar.value));
            PlayerPrefs.Save();
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
            SaveToDevice();
        }
#endif // BUILD_DEBUG
    }
}