using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordInventory : MonoBehaviour
{
    public static WordInventory instance;

    private List<GameObject> wordObjects;

    private void Awake()
    {
        instance = this;
        wordObjects = new List<GameObject>();
    }

    public bool HasWord(Word word)
    {
        foreach (GameObject wordObject in wordObjects)
        {
            if (wordObject.GetComponent<WordBehaviour>().GetWord() == word)
            {
                return true;
            }
        }
        return false;
    }

    public void AddWord(Word word)
    {
        GameObject wordObject = new GameObject(word.GetWord(), typeof(WordBehaviour));
        wordObject.transform.SetParent(transform);
        wordObject.GetComponent<WordBehaviour>().SetWord(word);
        wordObjects.Add(wordObject);
    }

    public void UseWord(Word word)
    {
        foreach (GameObject wordObject in wordObjects)
        {
            if (wordObject.GetComponent<WordBehaviour>().GetWord() == word)
            {
                wordObjects.Remove(wordObject);
                Destroy(wordObject);
                break;
            }
        }
    }
}
