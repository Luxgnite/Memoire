using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactible : MonoBehaviour
{
    public DialogueData dialogue;
    public string texteAide;
    public bool fenetre = false;

    private Message instanceTexteAide;
    private void OnMouseDown()
    {
        if (instanceTexteAide != null)
            DestroyImmediate(instanceTexteAide.gameObject);

        if (fenetre && !GameManagerOld._instance.dialogue)
        {
            //GameManagerOld._instance.ChangeScene("Contemplation");
        }
        else
        {
            DialogueManager._instance.ChargerDialogue(dialogue, gameObject);
        }
    }

    private void OnMouseEnter()
    {
        if (DialogueManager._instance.dialogueActif == null)
        {
            Debug.Log("On enter");
            instanceTexteAide = Instantiate(DialogueManager._instance.messagePrefab, new Vector3(0, 0, -1000), Quaternion.identity);
            instanceTexteAide.gameObject.transform.SetParent(DialogueManager._instance.messageCanvas.transform, false);

            instanceTexteAide.typeWrite = false;
            instanceTexteAide.displayText = "<i>" + texteAide + "</i>";

            if (fenetre)
                instanceTexteAide.target = GameManagerOld._instance.player;
            else
                instanceTexteAide.target = gameObject;
            instanceTexteAide.timeToDie = 0f;
        }
    }

    private void OnMouseExit()
    {
        if (instanceTexteAide != null)
        {
            instanceTexteAide.timeToDie = 1f;
            instanceTexteAide.SetAutoDestruction();
        }
    }
}