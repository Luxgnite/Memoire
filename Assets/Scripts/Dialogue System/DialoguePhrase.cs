using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialoguePhrase
{
    public enum Emetteur
    {
        PNJ,
        JOUEUR
    }

    public Emetteur emetteurValue = Emetteur.JOUEUR;

    [TextArea(3, 10)]
    public string texte;
}
