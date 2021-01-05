using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookBehaviour : MonoBehaviour
{
    [SerializeField] private Page leftPage;
    [SerializeField] private Page rightPage;
    [SerializeField] private Chapter firstChapter;

    private Chapter currentChapter;
    private int currentPageIndex;

    private void Start()
    {
        currentChapter = firstChapter;
        RenderCurrentPages();
    }

    public void NextPage()
    {
        currentPageIndex++;
        RenderCurrentPages();
    }

    public void PreviousPage()
    {
        currentPageIndex--;
        RenderCurrentPages();
    }

    private void RenderCurrentPages()
    {
        leftPage.SetPageText(currentChapter.GetPages()[currentPageIndex * 2], currentPageIndex > 0);
        rightPage.SetPageText(currentChapter.GetPages()[currentPageIndex * 2 + 1], (currentPageIndex + 1) * 2 < currentChapter.GetPages().Length);
    }
}
