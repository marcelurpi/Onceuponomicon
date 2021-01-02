using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Word : ScriptableObject
{
    [SerializeField] private bool singleUse;
    [SerializeField] private string word;
    [SerializeField] private Tag[] tags;

    public bool IsSingleUse() => singleUse;
    public string GetWord() => word;
    public Tag[] GetTags() => tags;
}
