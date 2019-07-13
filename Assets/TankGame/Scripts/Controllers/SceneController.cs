using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace TankGame {
    public class SceneController : MonoBehaviour {
        public UnityEvent _event;
        
        private void Update() {
            if (Input.GetKeyDown(KeyCode.K)) {
                _event.Invoke();
            }
        }
        

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
