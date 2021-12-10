using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class InterfaceManager : MonoBehaviour
{
    #region Private Fields
    private InterfaceManager _instance;
    #endregion

    #region Public Fields
    public InterfaceManager Instance
    {
        get { return _instance; }
    }

    public CanvasGroup canvasGroup;
    public bool inDialogue;
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
    }
    // Start is called before the first frame update
    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Display(bool show)
    {
        Sequence s = DOTween.Sequence();
        s.AppendInterval(.65f);
        s.Append(canvasGroup.DOFade(show ? 1 : 0, .2f));

        if (show)
        {
            s.Join(canvasGroup.GetComponent<RectTransform>().DOAnchorPos(new Vector3(0, -200), .2f * 2).From().SetEase(Ease.OutCubic));
            //s.AppendCallback(() => animatedText.ReadText(currentVillager.dialogue.conversationBlock[0]));
            
            inDialogue = true;
            canvasGroup.blocksRaycasts = true;
        }
        else
        {
            inDialogue = false;
            canvasGroup.blocksRaycasts = false;
        }
    }

    private void OnGUI()
    {
        if(GUI.Button(new Rect(0, 0, 150, 50), "Toggle Dialogue"))
        {
            Display(!inDialogue);
        }
    }
}
