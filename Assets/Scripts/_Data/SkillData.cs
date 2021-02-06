using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "UMI/New Skill", fileName = "Skill")]
public class SkillData : ScriptableObject
{
    public Sprite skillIcon;
    public int damage;
    public float cooldown;
    public int manaCost;
    public Enums.SkillType type;
    public GameObject skillPrefab;
}