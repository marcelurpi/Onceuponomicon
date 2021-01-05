using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    [SerializeField] private bool nextPage;
    [SerializeField] private BookBehaviour bookBehaviour;

    private Vector3 baseScale;

    private void Start()
    {
        baseScale = transform.localScale;
    }

    private void OnMouseEnter()
    {
        transform.localScale = baseScale * 1.2f;
    }

    private void OnMouseExit()
    {
        transform.localScale = baseScale;
    }

    private void OnMouseDown()
    {
        if (nextPage)
        {
            bookBehaviour.NextPage();
        } 
        else
        {
            bookBehaviour.PreviousPage();
        }
    }
}
