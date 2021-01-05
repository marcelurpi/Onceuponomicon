using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookBehaviour : MonoBehaviour
{
    [SerializeField] private Page leftPage;
    [SerializeField] private Page rightPage;
    [SerializeField] private Chapter firstChapter;
    [SerializeField] private AudioClip pageFlipClip;
    [SerializeField] private Animator animator;

    private Chapter currentChapter;
    private int currentPageIndex;

    private void Start()
    {
        currentChapter = firstChapter;
        AudioManager.instance.SetMusic(currentChapter.GetChapterMusic());
        RenderCurrentPages();
    }

    public void NextPage()
    {
        AudioManager.instance.PlaySound(0.05f, pageFlipClip);
        currentPageIndex++;
        animator.SetFloat("PageFlipDir", 1.0f);
        animator.SetTrigger("PageFlip");
    }

    public void PreviousPage()
    {
        AudioManager.instance.PlaySound(0.05f, pageFlipClip);
        currentPageIndex--;
        animator.SetFloat("PageFlipDir", -1.0f);
        animator.SetTrigger("PageFlip");
    }

    private void RenderCurrentPages()
    {
        leftPage.SetPrevPageText(currentChapter.GetPages()[currentPageIndex * 2], currentPageIndex > 0);
        rightPage.SetPrevPageText(currentChapter.GetPages()[currentPageIndex * 2 + 1], (currentPageIndex + 1) * 2 < currentChapter.GetPages().Length);

        if ((currentPageIndex + 1) * 2 < currentChapter.GetPages().Length)
        {
            leftPage.SetNextPageText(currentChapter.GetPages()[(currentPageIndex + 1) * 2], true);
        }
        if ((currentPageIndex + 1) * 2 + 1 < currentChapter.GetPages().Length)
        {
            rightPage.SetNextPageText(currentChapter.GetPages()[(currentPageIndex + 1) * 2 + 1], (currentPageIndex + 2) * 2 < currentChapter.GetPages().Length);
        }
    }
}
