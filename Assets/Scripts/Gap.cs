using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Gap
{
    public System.Action OnGapFilled;

    [SerializeField] private Pattern[] patterns;

    private string fillWord;

    public string GetFillWord() => fillWord;
    public Pattern[] GetPatterns() => patterns;

    public void OnDisable()
    {
        fillWord = null;
    }

    public void OnValidate()
    {
        foreach (Pattern pattern in patterns)
        {
            pattern.OnValidate();
        }
    }
    
    public void FillGap(Word word) 
    {
        fillWord = word.GetWord();
        foreach (Pattern pattern in patterns)
        {
            if (pattern.Matches(word)) 
            {
                Page.current.AddPageText(pattern.GetResponse());
                break;
            }
        }
        OnGapFilled?.Invoke();
    } 
}
