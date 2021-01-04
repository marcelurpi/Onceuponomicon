using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Word : ScriptableObject
{
    [SerializeField] private string word;
    [SerializeField] private Tag[] tags;

    private bool used;

    public bool IsUsed() => used;
    public void SetUsed(bool used) => this.used = used;
    public string GetWord() => word;
    public Tag[] GetTags() => tags;

    private void OnEnable()
    {
        used = false;
    }
}
