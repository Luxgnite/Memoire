using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;
using TMPro;

public class InterfaceManager : MonoBehaviour
{
    #region Private Fields
    private static InterfaceManager _instance;
    #endregion

    #region Public Fields
    public static InterfaceManager Instance
    {
        get { return _instance; }
    }

    public CanvasGroup dialogueUI;
    public CanvasGroup helpDisplay;
    public TextMeshProUGUI dialogueText;
    public bool inDialogue;
    public bool UIfocus = false;
    public bool typeWrite = true;

    [HideInInspector]
    public UnityEvent startDialogue;
    public UnityEvent endDialogue;

    [Header("Vitesse de l'effet machine à écrire. Caractère par secondes")]
    [Range(1, 200)]
    public int typeSpeed;

    private Transform targetHelp;
    private Queue<CanvasGroup> UIWaitingForDisplay;
    private Controls controls;
    private Queue<string> dialogueQueue;
    private string displayText = "...";

    //Index du "curseur" pour l'effet machine à écrire
    private float textIndex = 0;
    #endregion

    void Awake()
    {
        #region Singleton Pattern
        if (_instance == null)
            _instance = this;
        else if (_instance != this)
            Destroy(this);

        DontDestroyOnLoad(this);
        #endregion

        startDialogue = new UnityEvent();
        endDialogue = new UnityEvent();
    }
    // Start is called before the first frame update
    void Start()
    {
        dialogueUI = GetComponent<CanvasGroup>();
        UIWaitingForDisplay = new Queue<CanvasGroup>();
        controls = new Controls();
        controls.Enable();
        dialogueQueue = new Queue<string>();
        dialogueText = dialogueUI.GetComponentInChildren<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if(targetHelp != null)
        {
            helpDisplay.transform.localPosition = Camera.main.WorldToScreenPoint(new Vector3(targetHelp.position.x, targetHelp.position.y, 2));
        }

        if(inDialogue && controls.Player.DialogueNextSentence.triggered)
        {
            DialogueNextSentence();
        }


    }

    public void DisplayHelpInteract(bool show)
    {
        if (!UIfocus)
            helpDisplay.DOFade(show ? 1 : 0, .2f);
        else
            return;
    }

    public void DisplayDialogue(bool show, DialogueData diagData = null)
    {
        
        Sequence s = DOTween.Sequence();
        s.AppendInterval(.65f);
        s.Append(dialogueUI.DOFade(show ? 1 : 0, .2f));

        if (show)
        {
            //Load dialogue in a queue
            foreach(string sentence in diagData.dialogueBlocks)
            {
                dialogueQueue.Enqueue(sentence);
            }
            DialogueNextSentence();
            startDialogue?.Invoke();
            HideAllDisplayedUI();
            s.Join(dialogueUI.GetComponent<RectTransform>().DOAnchorPos(new Vector3(0, -200), .2f).From().SetEase(Ease.OutCubic));
            //s.AppendCallback(() => animatedText.ReadText(currentVillager.dialogue.conversationBlock[0]));
            
            inDialogue = true;
            dialogueUI.blocksRaycasts = true;
        }
        else
        {
            inDialogue = false;
            dialogueUI.blocksRaycasts = false;
            endDialogue?.Invoke();
            ShowAllDisplayedUI();
        }
    }

    private void FixedUpdate()
    {
        if (typeWrite)
        {
            //On récupère l'ancienne valeur du curseur
            float oldTextIndex = textIndex;
            //On récupère le texte à écrire sous forme de tableau de caractères pour le parcourir
            char[] lettres = displayText.ToCharArray();
            //Si il y a encore des lettres à écrire, on enclenche le processus
            if (textIndex < lettres.Length)
            {
                textIndex += Time.deltaTime * typeSpeed;
                //Si il y a suffisament de temps qui s'est passé, on écrit des lettres
                int maxIndex = (int)textIndex;
                if ((int)oldTextIndex != (int)textIndex)
                {
                    for (int i = (int)oldTextIndex; i < maxIndex; i++)
                    {
                        if (lettres[i] == '<')
                        {

                            string balise = "";

                            bool baliseFin = false;

                            while (!baliseFin)
                            {
                                balise += lettres[i];
                                if (lettres[i] == '>')
                                {
                                    Debug.Log("Fin de balise");
                                    baliseFin = true;
                                }
                                i++;
                                maxIndex++;
                            }
                            dialogueText.text += balise;
                            textIndex = i;
                        }
                        else
                        {
                            dialogueText.text += lettres[i];
                        }
                    }
                }
            }

        }
        else
        {
            dialogueText.text = displayText;
        }
    }

    private void DialogueNextSentence()
    {
        if (dialogueQueue.Count != 0)
        {
            displayText = dialogueQueue.Dequeue();
            dialogueText.text = "";
            textIndex = 0;
        }
        else
            DisplayDialogue(false);
    }

    public void ShowAllDisplayedUI()
    {
        if (UIfocus)
        {
            foreach(CanvasGroup UIGroup in UIWaitingForDisplay)
            {
                UIGroup.DOFade(1, 1f);
            }

            UIfocus = false;
        }
        else
            return;
    }

    public void HideAllDisplayedUI()
    {
        if (!UIfocus)
        {
            if (dialogueUI.alpha != 0)
                UIWaitingForDisplay.Enqueue(dialogueUI);
            if (helpDisplay.alpha != 0)
                UIWaitingForDisplay.Enqueue(helpDisplay);

            dialogueUI.DOFade(0, .2f);
            helpDisplay.DOFade(0, .2f);

            UIfocus = true;
        }
        else
            return;
    }

    private void OnGUI()
    {
        if(GUI.Button(new Rect(0, 0, 150, 50), "Toggle Dialogue"))
        {
            DisplayDialogue(!inDialogue);
        }
    }
}
