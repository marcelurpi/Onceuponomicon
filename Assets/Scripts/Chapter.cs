using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Chapter : ScriptableObject
{
    [SerializeField] private PageText[] pages;
    [SerializeField] private Chapter nextChapter;

    public PageText[] GetPages() => pages;
    public Chapter GetNextChapter() => nextChapter;
}
