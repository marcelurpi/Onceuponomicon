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
    [SerializeField] private TextMeshProUGUI textMesh;
    [SerializeField] private GameObject pageNavigationArrow;
    [SerializeField] private GameObject wordPrefab;

    private List<IndexModule> wordIndices;
    private List<IndexModule> gapIndices;
    private PageTextModule[] pageTextModules;

    private static readonly string GAP_STRING = "_____";
    private static readonly int WORD_COLLIDE_RANGE = 10;

    public static readonly int GAP_COLLIDE_RANGE = 20;

    public void SetPageText(PageText pageText, bool hasArrow)
    {
        pageTextModules = pageText.GetModules();
        textMesh.alignment = pageText.GetCenterText() ? TextAlignmentOptions.Center : TextAlignmentOptions.TopJustified;

        wordIndices = new List<IndexModule>();
        gapIndices = new List<IndexModule>();

        pageNavigationArrow.SetActive(hasArrow);

        RenderPageText(true, false);
    }

    public void AddPageText(PageText pageText)
    {
        List<PageTextModule> modules = new List<PageTextModule>(pageTextModules);
        modules.Add(new PageTextModule("\n\n"));
        modules.AddRange(pageText.GetModules());
        pageTextModules = modules.ToArray();
        RenderPageText(false, false);
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
        WordInventory.instance.OnInventoryUpdated += () => RenderPageText(false, false);
    }

    private void GenerateWordGameObjects()
    {
        foreach (IndexModule indexModule in wordIndices)
        {
            GameObject wordGameObject = Instantiate(wordPrefab, transform);

            TMP_WordInfo wordInfo = textMesh.GetTextInfo(textMesh.text).wordInfo[indexModule.index];
            Vector3 bottomLeft = textMesh.GetTextInfo(textMesh.text).characterInfo[wordInfo.firstCharacterIndex].bottomLeft - new Vector3(WORD_COLLIDE_RANGE, WORD_COLLIDE_RANGE);
            Vector3 topRight = textMesh.GetTextInfo(textMesh.text).characterInfo[wordInfo.lastCharacterIndex].topRight + new Vector3(WORD_COLLIDE_RANGE, WORD_COLLIDE_RANGE);
            Vector3 size = new Vector2(Mathf.Abs(topRight.x - bottomLeft.x), Mathf.Abs(topRight.y - bottomLeft.y));

            wordGameObject.transform.localPosition = bottomLeft + size / 2;
            wordGameObject.GetComponent<RectTransform>().sizeDelta = size;

            wordGameObject.GetComponent<BoxCollider2D>().size = size;

            wordGameObject.GetComponent<WordBehaviour>().SetInInventory(false);
            wordGameObject.GetComponent<WordBehaviour>().SetWord(indexModule.module.GetWord());
        }
    }

    private void GenerateGapGameObjects()
    {
        foreach (IndexModule indexModule in gapIndices)
        {
            GameObject gapGameObject = new GameObject("Gap", typeof(RectTransform), typeof(BoxCollider2D), typeof(GapBehaviour));

            TMP_WordInfo wordInfo = textMesh.GetTextInfo(textMesh.text).wordInfo[indexModule.index - 1];
            Vector3 bottomLeft = new Vector3()
            {
                x = textMesh.GetTextInfo(textMesh.text).characterInfo[wordInfo.lastCharacterIndex + 2].bottomLeft.x - GAP_COLLIDE_RANGE,
                y = textMesh.GetTextInfo(textMesh.text).characterInfo[wordInfo.lastCharacterIndex].bottomLeft.y - GAP_COLLIDE_RANGE,
            };
            Vector3 topRight = new Vector3()
            {
                x = textMesh.GetTextInfo(textMesh.text).characterInfo[wordInfo.lastCharacterIndex + 2 + GAP_STRING.Length].topRight.x + GAP_COLLIDE_RANGE,
                y = textMesh.GetTextInfo(textMesh.text).characterInfo[wordInfo.lastCharacterIndex].topRight.y + GAP_COLLIDE_RANGE,
            };
            Vector3 size = new Vector2(Mathf.Abs(topRight.x - bottomLeft.x), Mathf.Abs(topRight.y - bottomLeft.y));

            gapGameObject.transform.SetParent(transform);
            gapGameObject.transform.localScale = Vector3.one;
            gapGameObject.transform.localPosition = bottomLeft + size / 2;
            gapGameObject.GetComponent<RectTransform>().sizeDelta = size;

            gapGameObject.GetComponent<BoxCollider2D>().size = size;

            gapGameObject.GetComponent<GapBehaviour>().SetGap(indexModule.module.GetGap());

            indexModule.module.GetGap().OnGapFilled += () => RenderPageText(false, false);
        }
    }

    private void RenderPageText(bool generateGameObjects, bool editor)
    {
        StringBuilder builder = new StringBuilder();
        foreach (PageTextModule module in pageTextModules)
        {
            switch (module.GetModuleType())
            {
                case PageTextModule.Type.Text:
                    builder.Append(module.GetText());
                    break;
                case PageTextModule.Type.Word:
                    if (generateGameObjects)
                    {
                        wordIndices.Add(new IndexModule { index = CountWords(builder.ToString()), module = module });
                    }
                    if (editor || !WordInventory.instance.HasWord(module.GetWord()) && !module.GetWord().IsUsed())
                    {
                        builder.Append("<b>").Append(module.GetWord().GetWord()).Append("</b>");
                    }
                    else
                    {
                        builder.Append(module.GetWord().GetWord());
                    }
                    break;
                case PageTextModule.Type.Gap:
                    if (generateGameObjects)
                    {
                        gapIndices.Add(new IndexModule { index = CountWords(builder.ToString()), module = module });
                    }
                    if (string.IsNullOrEmpty(module.GetGap().GetFillWord()))
                    {
                        builder.Append(GAP_STRING);
                    } 
                    else
                    {
                        builder.Append(module.GetGap().GetFillWord());
                    }
                    break;
                default:
                    break;
            }
        }
        textMesh.text = builder.ToString();
        if (generateGameObjects)
        {
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
