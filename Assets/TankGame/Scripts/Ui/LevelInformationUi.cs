using System;
using SmartData.SmartLevelState;
using UnityEngine;


namespace TankGame {
    public class LevelInformationUi : MonoBehaviour {
        [SerializeField]
        private GameObject _rootPanelObj;

        [SerializeField]
        private LevelStateReader _levelStateSvar;


        private void Update() {
            switch (_levelStateSvar.value) {
                case LevelState.ShowIntro: {
                    if (!_rootPanelObj.activeSelf) {
                        _rootPanelObj.SetActive(true);
                    }
                    
                    break;
                }

                case LevelState.InProgress: {
                    if (_rootPanelObj.activeSelf) {
                        _rootPanelObj.SetActive(false);
                    }
                    
                    break;
                }
            }
        }
    }
}