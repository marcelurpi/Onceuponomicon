using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class AttackTag : Tag
{
    [SerializeField] private int baseDamage;

    public int GetBaseDamage() => baseDamage;
}
