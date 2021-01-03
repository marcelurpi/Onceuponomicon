using UnityEngine;

[System.Serializable]
public class PageTextModule
{
    public enum Type
    {
        Text,
        Word,
        Gap,
    }

    [HideInInspector] [SerializeField] private string typeName;
    [SerializeField] private Type type;
    [SerializeField] [TextArea(1, 10)] private string text;
    [SerializeField] private Word word;
    [SerializeField] private Gap gap;

    public Type GetModuleType() => type;
    public string GetText() => text;
    public Word GetWord() => word;
    public Gap GetGap() => gap;

    public void OnValidate()
    {
        typeName = type.ToString();
        gap.OnValidate();
    }
}