using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Word
{
    public string text;
    public Vector2 position;
    public GameObject node;
    public Word linkedWord;

    public Word(string text, Vector2 position)
    {
        this.text = text;
        this.position = position;
    }
}
