using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PageText
{
    [SerializeField] private PageTextModule[] modules;

    public PageTextModule[] GetModules() => modules;

    public void OnValidate()
    {
        foreach (PageTextModule module in modules)
        {
            module.OnValidate();
        }
    }
}
