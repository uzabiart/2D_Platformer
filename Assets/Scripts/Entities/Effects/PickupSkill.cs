using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupSkill : Entity
{
    public SpriteRenderer rarityGlow;
    public SpriteRenderer skillIcon;
    public Transform skillPrefabParent;
    public SkillData mySkillData;
    public RarityColor[] rarityColors;

    private void Start()
    {
        if (mySkillData == null) return;
        //UpdateMe(mySkillData);
    }

    public void UpdateMe(SkillData skill)
    {
        mySkillData = skill;
        skillIcon.sprite = skill.skillIcon;
        foreach (RarityColor rarity in rarityColors)
        {
            if (skill.type == rarity.skillType)
            {
                rarityGlow.color = rarity.rarityColor;
                break;
            }
        }
    }
}

[System.Serializable]
public class RarityColor
{
    public Enums.SkillType skillType;
    public Color rarityColor;
}