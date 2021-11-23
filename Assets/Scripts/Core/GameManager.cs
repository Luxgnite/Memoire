using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum SceneIndex
{
    TITLE_SCREEN = 1,
    GAME_SCENE = 2,
    UI = 3
}

public class GameManager : MonoBehaviour
{
    //Singleton instance
    public static GameManager _instance = null;

    public GameObject loadingScreen;
    public ProgressBar bar;

    // Start is called before the first frame update 
    void Awake()
    {
        # region Singleton Pattern
        if (_instance == null)
            _instance = this;
        else if (_instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        #endregion

        SceneManager.LoadSceneAsync((int)SceneIndex.TITLE_SCREEN, LoadSceneMode.Additive);
    }

    

    public void LoadGame()
    {
        loadingScreen.gameObject.SetActive(true);
        scenesLoading.Add(SceneManager.UnloadSceneAsync((int)SceneIndex.TITLE_SCREEN));
        scenesLoading.Add(SceneManager.LoadSceneAsync((int)SceneIndex.GAME_SCENE, LoadSceneMode.Additive));
        scenesLoading.Add(SceneManager.LoadSceneAsync((int)SceneIndex.UI, LoadSceneMode.Additive));

        StartCoroutine(GetSceneLoadProgress());
    }


    #region Loading Screen Management

    List<AsyncOperation> scenesLoading = new List<AsyncOperation>();
    float totalSceneProgress;

    public IEnumerator GetSceneLoadProgress()
    {
        for (int i = 0; i < scenesLoading.Count; i++)
        {
            while (!scenesLoading[i].isDone)
            {
                totalSceneProgress = 0;
                 
                foreach(AsyncOperation operation in scenesLoading)
                {
                    totalSceneProgress += operation.progress;
                }

                totalSceneProgress = (totalSceneProgress / scenesLoading.Count) * 100f;

                bar.current = Mathf.RoundToInt(totalSceneProgress);

                yield return null;
            }
        }

        loadingScreen.gameObject.SetActive(false);
    }

    #endregion

    /// <summary>
    /// Debug Command Test
    /// </summary>
    public void Test()
    {
        Debug.Log("Test");
    }
}
