using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum WordTheme
{
    SENTIMENT,
    ACTION
}

[System.Serializable]
public class WordData
{
    public string word;
    public List<WordTheme> themes;
}

[CreateAssetMenu(fileName = "New Word List", menuName = "Word list")]
public class WordList : ScriptableObject
{
    public List<WordData> wordData;

    public bool IsSameTheme(string word1, string word2)
    {
        WordData word1Data = wordData.Find(item => item.word.ToLower() == word1.ToLower());
        WordData word2Data = wordData.Find(item => item.word.ToLower() == word2.ToLower());

        if (word1Data == null || word2Data == null)
        {
            return false;
        }

        foreach(WordTheme theme in word1Data.themes)
        {
            if (word2Data.themes.Exists(item => item == theme))
            {
                return true;
            }
        }
        
        return false;
    }
}
