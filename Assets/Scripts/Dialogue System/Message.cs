using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class Message : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] public Vector3 offset = new Vector3(0,0,0);
    public GameObject target;
    public float timeToDie = 5f;
    public string displayText = "...";
    public int taillePolice = 12;
    public bool typeWrite = true;

    private SpriteRenderer spriteTarget;
    private TextMeshProUGUI text;
    //Index du "curseur" pour l'effet machine à écrire
    private float textIndex = 0;

    private void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        spriteTarget = target.GetComponent<SpriteRenderer>();

        text.fontSize = taillePolice;
        SetAutoDestruction();
    }
    //Update is called once per frame
    void FixedUpdate()
    {
        if(typeWrite)
        {
            //On récupère l'ancienne valeur du curseur
            float oldTextIndex = textIndex;
            //On récupère le texte à écrire sous forme de tableau de caractères pour le parcourir
            char[] lettres = displayText.ToCharArray();
            //Si il y a encore des lettres à écrire, on enclenche le processus
            if (textIndex < lettres.Length)
            {
                textIndex += Time.deltaTime * DialogueManager._instance.typeSpeed;
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
                            text.text += balise;
                            textIndex = i;
                        }
                        else
                        {
                            text.text += lettres[i];
                        }
                    }
                }
            }

        }
        else
        {
            text.text = displayText;
        }
        
        Vector3 pos = Camera.main.WorldToScreenPoint(target.transform.position + offset + new Vector3(0, spriteTarget.bounds.extents.y, 0));
        if (transform.position != pos)
            transform.position = pos;
    }

    void DestroyMessage()
    {
        Debug.Log("Destroy Message");
        //NotificationManager.instance.notifications.Find(
        DestroyImmediate(this.gameObject);
    }

    public void SetAutoDestruction()
    {
        if (timeToDie > 0f)
        {
            StopCoroutine("FadeOut");
            StartCoroutine("FadeOut");
        }
    }

    IEnumerator FadeOut()
    {
        yield return new WaitForSeconds(timeToDie - 1f);

        text.CrossFadeAlpha(0f, 1f, false);

        StartCoroutine(DestroyMsg());
    }

    IEnumerator DestroyMsg()
    {
        yield return new WaitForSeconds(1);

        DestroyMessage();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Left)
        {
            DialogueManager._instance.AfficherProchainePhrase();
        }
    }
}
