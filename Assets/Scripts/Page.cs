using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

public class Page : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textMesh;
    [SerializeField] private PageText pageText;

    private void Start()
    {
        RenderPageText();
    }

    private void OnValidate()
    {
        pageText.OnValidate();
    }

    [ContextMenu("RenderPageText")]
    private void RenderPageText()
    {
        StringBuilder builder = new StringBuilder();
        foreach (PageTextModule module in pageText.GetModules())
        {
            switch (module.GetModuleType())
            {
                case PageTextModule.Type.Text:
                    builder.Append(module.GetText());
                    break;
                case PageTextModule.Type.Word:
                    builder.Append("<b>").Append(module.GetWord().GetWord()).Append("</b>");
                    break;
                case PageTextModule.Type.Gap:
                    builder.Append("_____");
                    break;
                default:
                    break;
            }
        }
        textMesh.text = builder.ToString();
    }
}
