using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class HealTag : Tag
{
    [SerializeField] private int baseHealAmount;

    public int GetBaseHealAmount() => baseHealAmount;
}
