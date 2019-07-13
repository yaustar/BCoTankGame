using UnityEngine;
using UnityEngine.SceneManagement;

namespace TankGame {
    public class OnAppStart {
        private static readonly bool _sInitialised = false;
        

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void OnBeforeSceneLoadRuntimeMethod() {
            if (_sInitialised) return;

#if BUILD_DEBUG
            SceneManager.LoadSceneAsync(SceneNames.DEBUG_GLOBAL, LoadSceneMode.Additive);
#endif

            SceneManager.LoadSceneAsync(SceneNames.GLOBAL, LoadSceneMode.Additive);
        }
    }
}