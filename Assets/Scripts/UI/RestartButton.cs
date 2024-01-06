using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI
{
    public class RestartButton : MonoBehaviour
    {
        [SerializeField]
        private Button _button;

        private void Awake()
        {
            _button.onClick.AddListener(LoadGameScene);
        }

        private void LoadGameScene()
        {
            StartCoroutine(LoadSceneAsync(GlobalConstants.GAME_SCENE));
        }

        private IEnumerator LoadSceneAsync(string sceneName)
        {
            var asyncLoading = SceneManager.LoadSceneAsync(sceneName);

            while (!asyncLoading.isDone)
            {
                yield return null;
            }
        }

        private void OnDestroy()
        {
            _button.onClick.RemoveAllListeners();
        }
    }
}