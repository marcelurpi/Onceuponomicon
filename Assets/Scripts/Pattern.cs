using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Pattern
{
    private enum PatternType
    {
        HasAttack,
        HasArmor,
        HasHeal,
        Over10Attack,
    };

    [SerializeField] private PatternType patternType;
    [SerializeField] private PageText response;

    public PageText GetResponse() => response;

    public bool Matches(Word word) 
    {
        switch (patternType) 
        {
            case PatternType.HasAttack:
                return WordHasTag<AttackTag>(word);
            case PatternType.HasArmor:
                return WordHasTag<ArmorTag>(word);
            case PatternType.HasHeal:
                return WordHasTag<HealTag>(word);
            case PatternType.Over10Attack:
                AttackTag tag = WordGetTag<AttackTag>(word);
                return tag != null && tag.GetBaseDamage() > 10;
        }
        return false;
    }

    private bool WordHasTag<T>(Word word) where T : Tag
    {
        foreach (Tag tag in word.GetTags()) 
        {
            if (tag is T) 
            {
                return true;
            }
        }
        return false;
    }

    private T WordGetTag<T>(Word word) where T : Tag
    {
        foreach (Tag tag in word.GetTags()) 
        {
            if (tag is T) 
            {
                return (T)tag;
            }
        }
        return null;
    }
}