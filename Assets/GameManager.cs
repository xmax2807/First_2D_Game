using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] private LoadingScreen LoadingScreen;
    [SerializeField] public SettingData SettingData;
    public Scene CurrentScene => SceneManager.GetActiveScene();
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        SettingData.LoadData();
    }
    public void GameOver()
    {
        MenuManager.Instance.PushMenu(FindObjectOfType<GameOverMenu>(true));
        TimeManager.Instance.StopAllCoroutines();
    }
    public void ReloadScene(System.Action Result)
    {
        LoadScene(CurrentScene.name, Result);
    }

    public AsyncOperation UnloadCurrenScene()
    {
        Resources.UnloadUnusedAssets();
        return SceneManager.UnloadSceneAsync(CurrentScene.buildIndex);
    }

    public void LoadScene(string name, System.Action Result)
    {
        MenuManager.Instance.PopAll();
        GameObject.Find("Canvas").GetComponent<Canvas>().enabled = false;
        TimeManager.Instance.StopAllCoroutines();
        LoadingScreen.gameObject.SetActive(true);
        
        TimeManager.Instance.WaitFor(new WaitForEndOfFrame(), () =>
        {

            var UnLoadProgress = UnloadCurrenScene();

            TimeManager.Instance.WaitUntil(() => UnLoadProgress == null || UnLoadProgress.isDone, () =>
            {
                var LoadProgress = StartLoadingScene(name);
                if (LoadProgress == null)
                {
                    ReloadScene(null);
                    //LoadingScreen.Finish();
                    return;
                }

                TimeManager.Instance.WaitUntil(() => LoadProgress.isDone, () =>
                {
                    TimeManager.Instance.WaitForSecondsUnscaled(1.5f, () =>
                    {
                        Result?.Invoke();
                        LoadingScreen.Finish();
                    });
                });

            });

        });
    }

    public void LoadNextScene(System.Action Result)
    {
        try
        {
            var sceneName = GetSceneNameByIndex(CurrentScene.buildIndex + 1);

            if (!CheckIfSceneExist(sceneName)) return;
            InGameDataManager.Instance.SaveGame();
            LoadScene(sceneName, Result);
        }
        catch { return; }

    }
    public void LoadPreviousScene(System.Action Result)
    {
        try
        {
            var sceneName = GetSceneNameByIndex(CurrentScene.buildIndex - 1);

            if (!CheckIfSceneExist(sceneName)) return;
            InGameDataManager.Instance.SaveGame();
            LoadScene(sceneName, Result);
        }
        catch { return; }
    }

    private AsyncOperation StartLoadingScene(string name)
    {
        if (!CheckIfSceneExist(name))
        {
            Debug.Log("SceneNotValid");
            return null;
        }

        return SceneManager.LoadSceneAsync(name);
    }

    private bool CheckIfSceneExist(string name)
    {
        if (string.IsNullOrEmpty(name))
            return false;

        for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            var scenePath = SceneUtility.GetScenePathByBuildIndex(i);
            var lastSlash = scenePath.LastIndexOf("/");
            var sceneName = scenePath.Substring(lastSlash + 1, scenePath.LastIndexOf(".") - lastSlash - 1);

            if (string.Compare(name, sceneName, true) == 0)
                return true;
        }

        return false;
    }

    private string GetSceneNameByIndex(int index)
    {
        if (index < 0)
            return null;

        var scenePath = SceneUtility.GetScenePathByBuildIndex(index);
        var lastSlash = scenePath.LastIndexOf("/");
        var sceneName = scenePath.Substring(lastSlash + 1, scenePath.LastIndexOf(".") - lastSlash - 1);

        return sceneName;
    }
}
