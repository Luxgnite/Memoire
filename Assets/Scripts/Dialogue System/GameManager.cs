using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager _instance = null;

    public Camera camera;
    public GameObject player;
    public string previousScene = "";

    public bool dialogue = false;

    [Range(1f, 100f)]
    public float parallaxSpeed = 1f;

    // Start is called before the first frame update
    void Awake()
    {
        //Singleton Pattern
        if (_instance == null)
            _instance = this;
        else if (_instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void Start()
    {
        Init();
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Init();
    }

    void Init()
    {
        camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeScene(string nameScene)
    {
        previousScene = SceneManager.GetActiveScene().name;
        DialogueManager._instance.DetruirePhrasePrecedente();
        SceneManager.LoadScene(nameScene);
    }

    public void ChangeScene(int nameId)
    {
        DialogueManager._instance.DetruirePhrasePrecedente();
        previousScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(nameId);
    }

    public void LoadPreviousScene()
    {
        SceneManager.LoadScene(previousScene);
        camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }
}
