using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordBehaviour : MonoBehaviour
{
    [SerializeField] private Word word;

    public Word GetWord() => word;
    public void SetWord(Word word) => this.word = word;
}
