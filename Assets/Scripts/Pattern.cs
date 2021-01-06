using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Pattern
{
    [HideInInspector] [SerializeField] private string tagName;
    [SerializeField] private Tag tag;
    [SerializeField] private PageText response;

    public PageText GetResponse() => response;

    public void OnValidate()
    {
        tagName = tag.ToString();
    }

    public bool Matches(Word word) 
    {
        foreach (Tag tag in word.GetTags())
        {
            if (tag == this.tag)
            {
                return true;
            }
        }
        return false;
    }
}