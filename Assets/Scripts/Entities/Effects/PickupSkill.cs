using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupSkill : Entity
{
    public SpriteRenderer skillIcon;
    public Transform skillPrefabParent;

    public void UpdateMe(SkillData skill)
    {
        skillIcon.sprite = skill.skillIcon;
        Instantiate(skill.skillPrefab, skillPrefabParent);
    }
}