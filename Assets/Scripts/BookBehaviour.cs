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
    [SerializeField] private PageText emptyPage;

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
        currentPageIndex += 2;
        animator.SetFloat("PageFlipDir", 1.0f);
        animator.SetTrigger("PageFlip");
    }

    public void PreviousPage()
    {
        AudioManager.instance.PlaySound(0.05f, pageFlipClip);
        currentPageIndex -= 2;
        animator.SetFloat("PageFlipDir", -1.0f);
        animator.SetTrigger("PageFlip");
    }

    private void RenderCurrentPages()
    {
        if (currentPageIndex == currentChapter.GetPages().Length + 1)
        {
            leftPage.SetPrevPageTextModules(currentChapter.GetFightStatsPage().GetPageTextModules(), currentChapter.GetFightStatsPage().GetCenterText(), currentPageIndex > 0);
        }
        else if (currentPageIndex == currentChapter.GetPages().Length)
        {
            leftPage.SetPrevPageTextModules(currentChapter.GetSetupPage().GetPageTextModules(), currentChapter.GetSetupPage().GetCenterText(), currentPageIndex > 0);
        }
        else
        {
            leftPage.SetPrevPageTextModules(currentChapter.GetPages()[currentPageIndex].GetModules(), currentChapter.GetPages()[currentPageIndex].GetCenterText(), currentPageIndex > 0);
            if (currentPageIndex + 2 == currentChapter.GetPages().Length + 1)
            {
                leftPage.SetNextPageTextModules(currentChapter.GetFightStatsPage().GetPageTextModules(), currentChapter.GetFightStatsPage().GetCenterText(), true);
            }
            else if (currentPageIndex + 2 == currentChapter.GetPages().Length)
            {
                leftPage.SetNextPageTextModules(currentChapter.GetSetupPage().GetPageTextModules(), currentChapter.GetSetupPage().GetCenterText(), true);
            } 
            else
            {
                leftPage.SetNextPageTextModules(currentChapter.GetPages()[currentPageIndex + 2].GetModules(), currentChapter.GetPages()[currentPageIndex + 2].GetCenterText(), true);
            }
        }

        if (currentPageIndex + 1 == currentChapter.GetPages().Length + 1)
        {
            rightPage.SetPrevPageTextModules(currentChapter.GetFightStatsPage().GetPageTextModules(), currentChapter.GetFightStatsPage().GetCenterText(), false);
        }
        else if (currentPageIndex + 1 == currentChapter.GetPages().Length)
        {
            rightPage.SetPrevPageTextModules(currentChapter.GetSetupPage().GetPageTextModules(), currentChapter.GetSetupPage().GetCenterText(), false);
        }
        else if (currentPageIndex + 1 < currentChapter.GetPages().Length)
        {
            rightPage.SetPrevPageTextModules(currentChapter.GetPages()[currentPageIndex + 1].GetModules(), currentChapter.GetPages()[currentPageIndex + 1].GetCenterText(), 
                currentPageIndex + 2 <= currentChapter.GetPages().Length + 1);

            if (currentPageIndex + 3 == currentChapter.GetPages().Length + 1)
            {
                rightPage.SetNextPageTextModules(currentChapter.GetFightStatsPage().GetPageTextModules(), currentChapter.GetFightStatsPage().GetCenterText(), false);
            }
            else if (currentPageIndex + 3 == currentChapter.GetPages().Length)
            {
                rightPage.SetNextPageTextModules(currentChapter.GetSetupPage().GetPageTextModules(), currentChapter.GetSetupPage().GetCenterText(), false);
            }
            else if (currentPageIndex + 3 < currentChapter.GetPages().Length)
            {
                rightPage.SetNextPageTextModules(currentChapter.GetPages()[currentPageIndex + 3].GetModules(), currentChapter.GetPages()[currentPageIndex + 3].GetCenterText(), 
                    currentPageIndex + 4 <= currentChapter.GetPages().Length + 1);
            } 
            else
            {
                rightPage.SetNextPageTextModules(emptyPage.GetModules(), false, false);
            }
        } 
        else
        {
            rightPage.SetPrevPageTextModules(emptyPage.GetModules(), false, false);
        }
    }
}
