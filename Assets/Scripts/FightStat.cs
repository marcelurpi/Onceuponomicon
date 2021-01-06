using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FightStat
{
    public enum Type
    {
        Attack,
        Defense,
        Healing,
        Health,
    }

    [SerializeField] private Type type;
    [SerializeField] private int statNumber;

    public Type GetStatType() => type;

    public int GetStatNumber() => statNumber;
}
