using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Gap
{
    public System.Action OnGapFilled;

    [SerializeField] private Pattern[] patterns;

    private bool setup;
    private Word fillWord;

    public Word GetFillWord() => fillWord;
    public Pattern[] GetPatterns() => patterns;

    public Gap(bool setup)
    {
        this.setup = setup;
    }

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
        fillWord = word;
        if (!setup)
        {
            foreach (Pattern pattern in patterns)
            {
                if (pattern.Matches(word))
                {
                    Page.current.AddPageText(pattern.GetResponse());
                    break;
                }
            }
        }
        OnGapFilled?.Invoke();
    } 
}
