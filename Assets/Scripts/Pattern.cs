using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Pattern
{
    private enum Type
    {
        HasAttack,
        HasArmor,
        HasHeal,
        Over10Attack,
    };

    [HideInInspector] [SerializeField] private string typeName;
    [SerializeField] private Type type;
    [SerializeField] private PageText response;

    public PageText GetResponse() => response;

    public void OnValidate()
    {
        typeName = type.ToString();
    }

    public bool Matches(Word word) 
    {
        switch (type) 
        {
            case Type.HasAttack:
                return WordHasTag<AttackTag>(word);
            case Type.HasArmor:
                return WordHasTag<ArmorTag>(word);
            case Type.HasHeal:
                return WordHasTag<HealTag>(word);
            case Type.Over10Attack:
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