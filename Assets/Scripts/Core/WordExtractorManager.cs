using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using DG.Tweening;

public class WordExtractorManager : MonoBehaviour
{
    #region Public Fields
    public GameObject canvasThoughts;
    public CanvasGroup canvasGroup;
    public new Camera camera;
    public TextMeshProUGUI text;
    public TextMeshProUGUI listText;
    public GameObject thoughtPrefab;
    public GameObject thoughtFocus;
    #endregion

    #region Private Fields
    PointerEventData m_PointerEventData;
    EventSystem m_EventSystem;
    // Words list
    Dictionary<string, Vector2> words;
    #endregion

    private void Awake()
    {
        text = null;
        camera = Camera.main;
        words = new Dictionary<string, Vector2>();
        //Singleton ?

    }

    private void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());

        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            //Set up the new Pointer Event
            m_PointerEventData = new PointerEventData(m_EventSystem);
            //Set the Pointer Event Position to that of the mouse position
            m_PointerEventData.position = Mouse.current.position.ReadValue();

            //Create a list of Raycast Results
            List<RaycastResult> results = new List<RaycastResult>();

            //Raycast using the Graphics Raycaster and mouse click position
            canvasThoughts.GetComponent<GraphicRaycaster>().Raycast(m_PointerEventData, results);

            foreach(RaycastResult result in results)
            {
                text = result.gameObject.GetComponent<TextMeshProUGUI>();
                if (text != null)
                {
                    if (text.gameObject.CompareTag("Thought"))
                    {
                        thoughtFocus = text.gameObject;
                    }
                    else if (TMP_TextUtilities.FindIntersectingWord(text, Mouse.current.position.ReadValue(), camera) != -1)
                    {
                        int index = TMP_TextUtilities.FindIntersectingWord(text, Mouse.current.position.ReadValue(), camera);
                        SaveWord(text.textInfo.wordInfo[index].GetWord());
                    }
                    break;
                }
            }
        }

        if(thoughtFocus != null)
        {
            if (Mouse.current.leftButton.isPressed)
            {
                Vector2 mousePos = new Vector2((Mouse.current.position.ReadValue().x/ Screen.width) * 1920 , (Mouse.current.position.ReadValue().y / Screen.height) * 1080);
                mousePos = new Vector2(Mathf.Min(Mathf.Max(mousePos.x, 0), 1850), Mathf.Min(Mathf.Max(mousePos.y, 0), 950));
                thoughtFocus.GetComponent<RectTransform>().anchoredPosition = mousePos;
            }
            else
            {
                words[thoughtFocus.name] = thoughtFocus.GetComponent<RectTransform>().anchoredPosition;
                thoughtFocus = null;
            }
        }
        
    }

    #region Word Management
    /// <summary>
    /// Generate word on pointer
    /// </summary>
    /// <param name="word">Word to display</param>
    private void GenerateWord(string word)
    {
        Debug.Log("Intersecting with " + word);
    }

    /// <summary>
    /// Save a word in the word list
    /// </summary>
    /// <param name="word"></param>
    private void SaveWord(string word)
    {
        if (!words.ContainsKey(word))
        {
            Debug.Log("Thought saved.");
            words.Add(word, new Vector3(Random.Range(100, 1800), Random.Range(100, 900), 0));
        }
        else
            return;
    }
    #endregion

    #region UI Thoughts Management
    bool isShown = false;

    /// <summary>
    /// Display the differents words on the UI
    /// </summary>
    void DisplayWords()
    {
        string displayText = "";

        //Destroy all child from Thoughts Menu
        foreach(Transform child in this.transform)
        {
            Destroy(child.gameObject);
        }
            
        foreach(KeyValuePair<string, Vector2> entry in words)
        {
            GameObject gj = Instantiate(thoughtPrefab, this.transform);
            gj.name = entry.Key;
            gj.GetComponent<RectTransform>().anchoredPosition = entry.Value;
            gj.GetComponent<TextMeshProUGUI>().text = entry.Key;
            //displayText += entry.Key + "\n";
        }

        //listText.text = displayText;
    }

    void Display(bool show)
    {
        DisplayWords();
        Sequence s = DOTween.Sequence();
        s.AppendInterval(.25f);
        s.Append(canvasGroup.DOFade(show ? 1 : 0, .2f));

        isShown = show;
    }

    private void OnGUI()
    {
        if(GUI.Button(new Rect(0, 80, 150, 50), "Thoughts Menu"))
        {
            Display(!isShown);
        }
    }

    #endregion

    #region Input Management
    private void EnableInput()
    {

    }

    private void DisableInput()
    {

    }
    #endregion

    #region Unity Callbacks
    private void OnEnable()
    {
        EnableInput();
    }

    private void OnDisable()
    {
        DisableInput();
    }
    #endregion
}
