using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace TankGame {
    public class ScenesController : MonoBehaviour {

        // Public callbacks
        public void OnStartGame() {
            SceneManager.LoadScene(SceneNames.GAME);
        }
        
        
        public void OnShowLeaderboard() {
            SceneManager.LoadScene(SceneNames.LEADERBOARD);
        }


        public void OnShowMainMenu() {
            SceneManager.LoadScene(SceneNames.MAIN_MENU);
        }


        public void OnQuitApplication() {
            Application.Quit();
        }
    }
}
