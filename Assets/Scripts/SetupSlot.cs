using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SetupSlot
{
    [SerializeField] private string slotName;
    [SerializeField] private Tag tag;

    public string GetSlotName() => slotName;
    public Tag GetTag() => tag;
}
