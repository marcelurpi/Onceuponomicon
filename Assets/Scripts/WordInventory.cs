using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WordInventory : MonoBehaviour
{
    public static WordInventory instance;

    public System.Action OnInventoryUpdated;

    [SerializeField] private float positionX;
    [SerializeField] private float maxPositionY;

    private List<WordBehaviour> wordObjects;

    private void Awake()
    {
        instance = this;
        wordObjects = new List<WordBehaviour>();
    }

    public bool HasWord(Word word)
    {
        foreach (WordBehaviour wordBehaviour in wordObjects)
        {
            if (wordBehaviour.GetWord() == word)
            {
                return true;
            }
        }
        return false;
    }

    public void AddWord(WordBehaviour wordBehaviour)
    {
        wordBehaviour.transform.SetParent(transform);
        wordBehaviour.transform.localPosition = new Vector3
        {
            x = (Random.Range(0, 2) == 0 ? 1 : -1) * positionX,
            y = Random.Range(-maxPositionY, maxPositionY)
        };

        wordBehaviour.SetInInventory(true);

        wordBehaviour.GetComponent<BoxCollider2D>().size += new Vector2(Page.GAP_COLLIDE_RANGE * 2, Page.GAP_COLLIDE_RANGE * 2);

        wordObjects.Add(wordBehaviour);
        OnInventoryUpdated?.Invoke();
    }

    public void UseWord(Word word)
    {
        foreach (WordBehaviour wordBehaviour in wordObjects)
        {
            if (wordBehaviour.GetWord() == word)
            {
                wordObjects.Remove(wordBehaviour);
                Destroy(wordBehaviour.gameObject);
                OnInventoryUpdated?.Invoke();
                break;
            }
        }
    }
}
