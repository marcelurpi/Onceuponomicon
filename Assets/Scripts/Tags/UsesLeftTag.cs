using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class UsesLeftTag : Tag
{
    [SerializeField] private int baseUsesLeft;

    public int GetBaseUsesLeft() => baseUsesLeft;
}
