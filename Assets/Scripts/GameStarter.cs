using System.Collections;
using LevelSettings;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStarter : MonoBehaviour
{
    [SerializeField] 
    private LevelsSettingsProvider _levelsSettingsProvider;
        
    private void Awake()
    {
        _levelsSettingsProvider.Initialize();
    }

    public void LoadGameScene(int level)
    {
        PlayerPrefs.SetInt(GlobalConstants.CURRENT_LEVEL, level);
        PlayerPrefs.Save();
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
}