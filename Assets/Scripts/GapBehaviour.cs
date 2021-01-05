using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GapBehaviour : MonoBehaviour
{
    private bool mouseOver;
    private Gap gap;
    private Camera mainCamera;
    public Gap GetGap() => gap;
    public void SetGap(Gap gap) => this.gap = gap;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        if (string.IsNullOrEmpty(gap.GetFillWord()))
        {
            bool wasOver = mouseOver;
            mouseOver = IsMouseOver();
            if (!wasOver && mouseOver)
            {
                OnMouseEnter();
            }
            else if (wasOver && !mouseOver)
            {
                OnMouseExit();
            }
        }
    }

    private void OnMouseEnter()
    {
        if (WordBehaviour.draggedWord != null)
        {
            WordBehaviour.draggedWord.SnapWord(this);
        }
    }

    private void OnMouseExit()
    {
        if (WordBehaviour.draggedWord != null)
        {
            WordBehaviour.draggedWord.SnapWord(null);
        }
    }

    private bool IsMouseOver()
    {
        Vector2 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D[] hits = Physics2D.RaycastAll(mousePosition, Vector2.zero);
        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider.gameObject == gameObject)
            {
                return true;
            }
        }
        return false;
    }
}
