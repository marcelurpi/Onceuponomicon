using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Gap
{
    [SerializeField] private bool consumes;
    [SerializeField] private Pattern[] patterns;

    public bool DoesConsume() => consumes;
    public Pattern[] GetPatterns() => patterns;
    
    public void FillGap(Word word) 
    {
        foreach (Pattern pattern in patterns)
        {
            if (pattern.Matches(word)) 
            {
                //TODO: Implement respones 
            }
        }
    } 
}
