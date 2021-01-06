using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordBehaviour : MonoBehaviour
{
    public static WordBehaviour draggedWord;

    [SerializeField] private AudioClip clickSound;
    [SerializeField] private AudioClip dropSound;

    private bool inInventory;
    private Word word;
    private Vector3 baseScale;
    private Vector3 lastValidPos;
    private GapBehaviour snappedGap;

    

    public Word GetWord() => word;
    public void SetWord(Word word) => this.word = word;
    
    public void SetInInventory(bool inInventory) 
    {
        this.inInventory = inInventory;
        lastValidPos = transform.localPosition;
    }

    public void SnapWord(GapBehaviour snappedGap)
    {
        this.snappedGap = snappedGap;
        if (snappedGap != null)
        {
            transform.position = snappedGap.transform.position;
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y + 5);
        }
    }

    private void OnMouseEnter()
    {
        
    }

    private void OnMouseDown()
    {
        if (inInventory)
        {
            draggedWord = this;
        }
        else
        {
            AudioManager.instance.PlaySound(0.05f, clickSound);
            word.SetUsed(true);
            WordInventory.instance.AddWord(this);
        }
    }

    private void OnMouseUp()
    {
        if (inInventory)
        {
            draggedWord = null;
            transform.localPosition = lastValidPos;
            if (snappedGap != null)
            {
                AudioManager.instance.PlaySound(0.5f, dropSound);
                snappedGap.GetGap().FillGap(word);
                WordInventory.instance.UseWord(word);
            }
        }
    }

    private void Update()
    {
        if (draggedWord == this && snappedGap == null)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector3(mousePosition.x, mousePosition.y);
        }
    }
}
