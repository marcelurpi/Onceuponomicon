using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SetupPage : ScriptableObject
{
    [SerializeField] private bool center;
    [SerializeField] private SetupSlot[] setupSlots;

    public bool GetCenterText() => center;

    public PageTextModule[] GetPageTextModules()
    {
        PageTextModule[] modules = new PageTextModule[setupSlots.Length * 2 + 1];
        modules[0] = new PageTextModule("<size=100>Fight Setup</size>\n");
        for (int i = 0; i < setupSlots.Length; i++)
        {
            modules[(i * 2) + 1] = new PageTextModule("\n" + setupSlots[i].GetSlotName() + ": ");
            Gap gap = new Gap(true);
            modules[(i * 2) + 2] = new PageTextModule(gap);
        }
        return modules;
    }
}
