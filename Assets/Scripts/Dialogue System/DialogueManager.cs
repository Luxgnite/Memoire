using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager _instance = null;

    public Canvas messageCanvas;
    public Message dialogueActif;
    public Message messagePrefab;
    [Header("Vitesse de l'effet machine à écrire. Caractère par secondes")]
    [Range(1, 200)]
    public int typeSpeed;

    private GameObject pnjActuel;
    private Queue<string> phrases;

    // Start is called before the first frame update
    void Awake()
    {
        //Singleton Pattern
        if (_instance == null)
            _instance = this;
        else if (_instance != this)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        phrases = new Queue<string>();
    }

    public void ChargerDialogue(DialogueData dialogue, GameObject pnj)
    {
        phrases.Clear();
        GameManagerOld._instance.dialogue = true;

        pnjActuel = pnj;
        foreach (string phrase in dialogue.dialogueBlocks)
        {
            phrases.Enqueue(phrase);
        }

        AfficherProchainePhrase();
    }

    public void AfficherProchainePhrase()
    {
        if (phrases.Count != 0)
        {
            DetruirePhrasePrecedente();

            string phrase = phrases.Dequeue();

            dialogueActif = Instantiate(messagePrefab, new Vector3(0, 0, -1000), Quaternion.identity);
            dialogueActif.gameObject.transform.SetParent(messageCanvas.transform, false);

            dialogueActif.displayText = phrase;

            /*if (phrase.emetteurValue == DialoguePhrase.Emetteur.JOUEUR)
                dialogueActif.target = GameManagerOld._instance.player;
            else if (phrase.emetteurValue == DialoguePhrase.Emetteur.PNJ)
                dialogueActif.target = pnjActuel;*/
            dialogueActif.timeToDie = 0f;
        }
        else
        {
            GameManagerOld._instance.dialogue = false;
        }
    }

    //A changer comme système
    public void DetruirePhrasePrecedente()
    {
        if (dialogueActif != null)
            Destroy(dialogueActif.gameObject);
    }
}