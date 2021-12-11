using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Word : MonoBehaviour
{
    public string text;
    public GameObject node;
    public Word linkedWord;
    public GameObject linkLine;

    public Word(string text)
    {
        this.text = text;
    }

    public void Update()
    {
       if(linkLine != null && linkedWord != null)
        {
            linkLine.GetComponent<LineRenderer>().SetPositions(new Vector3[] { this.node.transform.position, linkedWord.node.transform.position });
        }
    }

    public void resetLine()
    {
        linkedWord = null;
        Destroy(linkLine);
        linkLine = null;
    }
}
