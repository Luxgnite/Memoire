using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

        //SceneManager.LoadSceneAsync((int)SceneIndex.TITLE_SCREEN, LoadSceneMode.Additive);
    }

    public void LoadGame()
    {
        SceneManager.UnloadSceneAsync((int)SceneIndex.TITLE_SCREEN);
        SceneManager.LoadSceneAsync((int)SceneIndex.GAME_SCENE, LoadSceneMode.Additive);
        SceneManager.LoadSceneAsync((int)SceneIndex.UI, LoadSceneMode.Additive);
    }

    public void Test()
    {
        Debug.Log("Test");
    }
}
