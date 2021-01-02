using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ArmorTag : Tag
{
    [SerializeField] private int baseArmor;

    public int GetBaseArmor() => baseArmor;
}
