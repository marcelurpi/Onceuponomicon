using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class PageText : ScriptableObject
{
    [SerializeField] private bool centerText;
    [SerializeField] private PageTextModule[] modules;

    public bool GetCenterText() => centerText;
    public PageTextModule[] GetModules() => modules;

    public void OnDisable()
    {
        if (modules == null) return;
        foreach (PageTextModule module in modules)
        {
            module.OnDisable();
        }
    }

    public void OnValidate()
    {
        foreach (PageTextModule module in modules)
        {
            module.OnValidate();
        }
    }

    public PageText(PageTextModule[] modules)
    {
        this.modules = modules;
    }
}
