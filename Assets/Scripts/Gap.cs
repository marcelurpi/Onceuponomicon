using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Gap
{
    public System.Action OnGapFilled;

    [SerializeField] private string fillWord;
    [SerializeField] private Pattern[] patterns;

    public string GetFillWord() => fillWord;
    public Pattern[] GetPatterns() => patterns;

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
                //TODO: Implement respones 
                break;
            }
        }
        OnGapFilled?.Invoke();
    } 
}
