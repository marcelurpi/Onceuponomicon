using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

public class Page : MonoBehaviour
{
    public static Page current;

    [System.Serializable]
    private struct IndexModule
    {
        public int index;
        public PageTextModule module;
    }

    [SerializeField] private bool right;
    [SerializeField] private TextMeshProUGUI prevTextMesh;
    [SerializeField] private TextMeshProUGUI nextTextMesh;
    [SerializeField] private GameObject prevPageNavigationArrow;
    [SerializeField] private GameObject nextPageNavigationArrow;
    [SerializeField] private GameObject wordPrefab;
    [SerializeField] private Transform wordGapParent;

    private List<IndexModule> wordIndices;
    private List<IndexModule> gapIndices;
    private PageTextModule[] prevPageTextModules;
    private PageTextModule[] nextPageTextModules;

    private static readonly string GAP_STRING = "_____";
    private static readonly int WORD_COLLIDE_RANGE = 10;

    public static readonly int GAP_COLLIDE_RANGE = 10;

    public void SetPrevPageTextModules(PageTextModule[] pageTextModules, bool centerText, bool hasArrow)
    {
        prevPageTextModules = pageTextModules;
        prevTextMesh.alignment = centerText ? TextAlignmentOptions.Center : TextAlignmentOptions.TopJustified;

        prevPageNavigationArrow.SetActive(hasArrow);

        RenderPageText(prevPageTextModules, true, prevTextMesh);
    }

    public void SetNextPageTextModules(PageTextModule[] pageTextModules, bool centerText, bool hasArrow)
    {
        nextPageTextModules = pageTextModules;
        nextTextMesh.alignment = centerText ? TextAlignmentOptions.Center : TextAlignmentOptions.TopJustified;

        nextPageNavigationArrow.SetActive(hasArrow);

        RenderPageText(nextPageTextModules, false, nextTextMesh);
    }

    public void AddPageText(PageText pageText)
    {
        List<PageTextModule> modules = new List<PageTextModule>(prevPageTextModules);
        modules.Add(new PageTextModule("\n\n"));
        modules.AddRange(pageText.GetModules());
        prevPageTextModules = modules.ToArray();
        RenderPageText(prevPageTextModules, false, prevTextMesh);
    }

    private void Awake()
    {
        if (right)
        {
            current = this;
        }
    }

    private void Start()
    {
        WordInventory.instance.OnInventoryUpdated += () => RenderPageText(prevPageTextModules, true, prevTextMesh);
    }

    private Rect GetRectFromWordIndex(int wordIndex, int wordLength, int collideRange)
    {
        TMP_WordInfo wordInfo = prevTextMesh.GetTextInfo(prevTextMesh.text).wordInfo[wordIndex - 1];
        Vector3 bottomLeft = new Vector3()
        {
            x = prevTextMesh.GetTextInfo(prevTextMesh.text).characterInfo[wordInfo.lastCharacterIndex].topRight.x + 16,
            y = prevTextMesh.GetTextInfo(prevTextMesh.text).characterInfo[wordInfo.lastCharacterIndex].bottomLeft.y + 3 - collideRange,
        };
        Vector3 topRight = new Vector3()
        {
            x = prevTextMesh.GetTextInfo(prevTextMesh.text).characterInfo[wordInfo.lastCharacterIndex].topRight.x + 16 + 15 * wordLength,
            y = prevTextMesh.GetTextInfo(prevTextMesh.text).characterInfo[wordInfo.lastCharacterIndex].topRight.y + collideRange,
        };
        Vector3 size = new Vector2(Mathf.Abs(topRight.x - bottomLeft.x), Mathf.Abs(topRight.y - bottomLeft.y));
        return new Rect(bottomLeft, size);
    }

    private void GenerateWordGameObjects()
    {
        foreach (IndexModule indexModule in wordIndices)
        {
            GameObject wordGameObject = Instantiate(wordPrefab, wordGapParent);

            Rect rect = GetRectFromWordIndex(indexModule.index, indexModule.module.GetWord().GetWord().Length, WORD_COLLIDE_RANGE);

            wordGameObject.transform.localPosition = rect.position + rect.size / 2;
            wordGameObject.GetComponent<RectTransform>().sizeDelta = rect.size;

            wordGameObject.GetComponent<BoxCollider2D>().offset = new Vector2(rect.size.x / 2, 0);
            wordGameObject.GetComponent<BoxCollider2D>().size = rect.size;

            wordGameObject.GetComponent<TextMeshProUGUI>().text = indexModule.module.GetWord().GetWord();

            wordGameObject.GetComponent<WordBehaviour>().SetInInventory(false);
            wordGameObject.GetComponent<WordBehaviour>().SetWord(indexModule.module.GetWord());
        }
    }

    private void GenerateGapGameObjects()
    {
        foreach (IndexModule indexModule in gapIndices)
        {
            GameObject gapGameObject = new GameObject("Gap", typeof(RectTransform), typeof(BoxCollider2D), typeof(GapBehaviour));

            Rect rect = GetRectFromWordIndex(indexModule.index, GAP_STRING.Length, GAP_COLLIDE_RANGE);

            gapGameObject.transform.SetParent(wordGapParent);
            gapGameObject.transform.localScale = Vector3.one;
            gapGameObject.transform.localRotation = Quaternion.identity;
            gapGameObject.transform.localPosition = rect.position + rect.size / 2;
            gapGameObject.GetComponent<RectTransform>().anchorMin = new Vector2(0, 0.5f);
            gapGameObject.GetComponent<RectTransform>().anchorMax = new Vector2(0, 0.5f);
            gapGameObject.GetComponent<RectTransform>().pivot = new Vector2(0, 0.5f);
            gapGameObject.transform.localPosition = rect.position + rect.size / 2;

            gapGameObject.GetComponent<RectTransform>().sizeDelta = rect.size;

            gapGameObject.GetComponent<BoxCollider2D>().offset = new Vector2(rect.size.x / 2, 0);
            gapGameObject.GetComponent<BoxCollider2D>().size = rect.size;

            gapGameObject.GetComponent<GapBehaviour>().SetGap(indexModule.module.GetGap());

            indexModule.module.GetGap().OnGapFilled += () => RenderPageText(prevPageTextModules, false, prevTextMesh);
        }
    }

    private void RenderPageText(PageTextModule[] modules, bool generateGameObjects, TextMeshProUGUI textMesh)
    {
        if (generateGameObjects)
        {
            wordIndices = new List<IndexModule>();
            gapIndices = new List<IndexModule>();
        }
        StringBuilder builder = new StringBuilder();
        int gaps = 0;
        foreach (PageTextModule module in modules)
        {
            switch (module.GetModuleType())
            {
                case PageTextModule.Type.Text:
                    builder.Append(module.GetText());
                    break;
                case PageTextModule.Type.Word:
                    if (!WordInventory.instance.HasWord(module.GetWord()) && !module.GetWord().IsUsed())
                    {
                        if (generateGameObjects)
                        {
                            wordIndices.Add(new IndexModule { index = CountWords(builder.ToString()) - gaps, module = module });
                        }
                        for (int i = 0; i < module.GetWord().GetWord().Length * 1.5f; i++)
                        {
                            builder.Append(" ");
                        }
                    }
                    else
                    {
                        builder.Append(module.GetWord().GetWord());
                    }
                    break;
                case PageTextModule.Type.Gap:
                    if (module.GetGap().GetFillWord() == null || string.IsNullOrEmpty(module.GetGap().GetFillWord().GetWord()))
                    {
                        if (generateGameObjects)
                        {
                            gapIndices.Add(new IndexModule { index = CountWords(builder.ToString()) - gaps, module = module });
                        }
                        gaps++;
                        builder.Append(GAP_STRING);
                    } 
                    else
                    {
                        builder.Append(module.GetGap().GetFillWord().GetWord());
                    }
                    break;
                default:
                    break;
            }
        }
        textMesh.text = builder.ToString();
        if (generateGameObjects)
        {
            List<Transform> existingGapsWords = new List<Transform>(wordGapParent.childCount);
            for (int i = 0; i < wordGapParent.childCount; i++)
            {
                existingGapsWords.Add(wordGapParent.GetChild(i));
            }
            foreach (Transform gapWord in existingGapsWords)
            {
                Destroy(gapWord.gameObject);
            }
            GenerateWordGameObjects();
            GenerateGapGameObjects();
        }
    }

    private int CountWords(string str)
    {
        str = str.Trim(' ', '\n');
        while (str.Contains("  "))
        {
            str = str.Replace("  ", " ");
        }
        while (str.Contains("\n\n"))
        {
            str = str.Replace("\n\n", "\n");
        }
        return str.Split(' ', '\n').Length;
    }
}
