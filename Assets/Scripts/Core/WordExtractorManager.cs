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
    public GameObject lineNodePrefab;
    public GameObject nodeFocus;
    public GameObject lineCreated;
    public WordList wordList;

    [Header("DA WAY")]
    public Word StartWord;
    public Word EndWord;
    #endregion

    #region Private Fields
    PointerEventData m_PointerEventData;
    EventSystem m_EventSystem;
    // Words list
    List<Word> words;
    #endregion

    private void Awake()
    {
        text = null;
        camera = Camera.main;
        words = new List<Word>();
        //Singleton ?

    }

    private void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    private void FixedUpdate()
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
                Image node = result.gameObject.GetComponent<Image>();
                if (text != null)
                {
                    if (text.gameObject.CompareTag("Thought"))
                    {
                        this.thoughtFocus = text.gameObject;
                    }
                    else if (TMP_TextUtilities.FindIntersectingWord(text, Mouse.current.position.ReadValue(), camera) != -1)
                    {
                        int index = TMP_TextUtilities.FindIntersectingWord(text, Mouse.current.position.ReadValue(), camera);
                        SaveWord(text.textInfo.wordInfo[index].GetWord());
                    }
                    break;
                }else if(node != null)
                {
                    if (node.gameObject.CompareTag("ThoughtNode"))
                    {
                        this.nodeFocus = node.gameObject;
                    }
                }
            }
        }

        if(thoughtFocus != null && thoughtFocus.GetComponent<Word>().state == WordState.BASIC)
        {
            if (Mouse.current.leftButton.isPressed)
            {
                Vector2 mousePos = new Vector2((Mouse.current.position.ReadValue().x/ Screen.width) * 1920 , (Mouse.current.position.ReadValue().y / Screen.height) * 1080);
                //Viewport "Margins" 
                mousePos = new Vector2(Mathf.Min(Mathf.Max(mousePos.x, 100), 1800), Mathf.Min(Mathf.Max(mousePos.y, 100), 900));
                thoughtFocus.GetComponent<RectTransform>().anchoredPosition = mousePos;
            }
            else
            {
                thoughtFocus = null;
            }
        }else if(nodeFocus != null)
        {
            nodeFocus.GetComponentInParent<Word>().resetLine();
            if (Mouse.current.leftButton.isPressed)
            {
                if (this.lineCreated == null /*&& nodeFocus.GetComponentInParent<Word>().state != WordState.END*/)
                {
                    this.lineCreated = Instantiate(lineNodePrefab, this.transform);
                }
                else
                {
                    lineCreated.GetComponent<LineRenderer>().SetPositions(new Vector3[] { nodeFocus.transform.position, Camera.main.ScreenToWorldPoint(new Vector3(Mouse.current.position.ReadValue().x, Mouse.current.position.ReadValue().y, Camera.main.nearClipPlane)) });
                }
            }
            else
            {
                //Set up the new Pointer Event
                m_PointerEventData = new PointerEventData(m_EventSystem);
                //Set the Pointer Event Position to that of the mouse position
                m_PointerEventData.position = Mouse.current.position.ReadValue();

                //Create a list of Raycast Results
                List<RaycastResult> results = new List<RaycastResult>();

                //Raycast using the Graphics Raycaster and mouse click position
                canvasThoughts.GetComponent<GraphicRaycaster>().Raycast(m_PointerEventData, results);
                foreach (RaycastResult result in results)
                {
                    Image node = result.gameObject.GetComponent<Image>();
                    if(lineCreated != null && node != null && node != nodeFocus && node.CompareTag("ThoughtNode"))
                    {
                        Word originWord = nodeFocus.gameObject.GetComponentInParent<Word>();
                        Word destinationWord = node.gameObject.GetComponentInParent<Word>();

                        if(wordList.IsSameTheme(originWord.text, destinationWord.text) && !IsLooping(originWord, destinationWord))
                        {
                            originWord.linkLine = lineCreated;
                            originWord.linkedWord = destinationWord;
                            lineCreated = null;
                            if (DoIWin())
                            {
                                Debug.Log("Eureka !");
                            }
                        }  
                    }
                    else
                    {
                        Destroy(this.lineCreated);
                    }
                }
                nodeFocus = null;
            }
        }
        
    }

    public bool IsLooping(Word originWord, Word destinationWord)
    {
        Word currentWord = destinationWord;
        int count = 10;
        while(currentWord.linkedWord != null && count <= 10)
        {
            currentWord = currentWord.linkedWord;
            count--;
        }

        if (currentWord == originWord)
        {
            return true;
        }

        return false;
    }
    public bool DoIWin()
    {
        Word currentWord = StartWord;
        int count = 10;
        while (currentWord.linkedWord != null && currentWord.state != WordState.END && count <= 10)
        {
            currentWord = currentWord.linkedWord;
            count--;
        }

        if(currentWord.state == WordState.END)
        {
            return true;
        }
        return false;
    }

    #region Word Management
    /// <summary>
    /// Generate "UI word" effect on pointer
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
        if (!words.Exists(thoughtWord => thoughtWord.text == word))
        {
            Debug.Log("Thought saved.");
            GameObject gj = Instantiate(thoughtPrefab, this.transform);
            gj.name = word;
            gj.GetComponent<TextMeshProUGUI>().text = word;
            gj.GetComponent<RectTransform>().anchoredPosition = new Vector2(Random.Range(100, 1800), Random.Range(100, 900));
            Word componentWord = gj.GetComponent<Word>();
            componentWord.text = word;
        }
        else
            return;
    }
    #endregion

    #region UI Thoughts Management
    bool isShown = false;

    void Display(bool show)
    {
        Sequence s = DOTween.Sequence();
        s.AppendInterval(.25f);
        s.Append(canvasGroup.DOFade(show ? 1 : 0, .2f));
        Camera.main.orthographic = show;

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
