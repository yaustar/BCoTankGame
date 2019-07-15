using System;
using System.Collections;
using System.Collections.Generic;
using CommandTerminal;
using SmartData.SmartLeaderboardTableData;
using UnityEngine;


namespace TankGame {
    public class LeaderboardUiTable : MonoBehaviour {
        [SerializeField]
        private LeaderboardTableDataReader _leaderboardTableDataSvar;

        [SerializeField]
        private List<LeaderboardUiEntry> _leaderboardUiEntries;
        

        private void Start() {
            Populate();
            
#if BUILD_DEBUG
            Terminal.Shell.AddCommand("RefreshLeaderboardUi", DebugCommandRefreshTable, 0, 0);
#endif
        }


        private void OnDestroy() {
#if BUILD_DEBUG
            Terminal.Shell.RemoveCommand("RefreshLeaderboardUi");
#endif
        }


        // Private
        private void Populate() {
            // Get the maximum number of scores we can show from he table
            // if we have less scores in data table  than disable the reminder
            // of UI entries
            
            var uiEntriesCount = _leaderboardUiEntries.Count;
            var dataTable = _leaderboardTableDataSvar.value.table;

            for (int i = 0; i < uiEntriesCount && i < dataTable.Count; i++) {
                _leaderboardUiEntries[i].Set(i + 1, dataTable[i]);
                _leaderboardUiEntries[i].gameObject.SetActive(true);
            }

            for (int i = dataTable.Count; i < uiEntriesCount; i++) {
                _leaderboardUiEntries[i].gameObject.SetActive(false);
            }
        }
        
#if BUILD_DEBUG
        private void DebugCommandRefreshTable(CommandArg[] args) {
            Populate();
        }
#endif
    }
}