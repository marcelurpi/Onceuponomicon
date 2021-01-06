using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Chapter : ScriptableObject
{
    [SerializeField] private PageText[] pages;
    [SerializeField] private SetupPage setupPage;
    [SerializeField] private FightStatsPage fightStatsPage;
    [SerializeField] private AudioClip chapterMusic;
    [SerializeField] private Chapter nextChapter;

    public PageText[] GetPages() => pages;
    public SetupPage GetSetupPage() => setupPage;
    public FightStatsPage GetFightStatsPage() => fightStatsPage;
    public AudioClip GetChapterMusic() => chapterMusic;
    public Chapter GetNextChapter() => nextChapter;
}
