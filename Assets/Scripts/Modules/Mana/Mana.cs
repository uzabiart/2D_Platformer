using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mana : Module
{
    PlayerData myPlayerData;
    bool recharging;
    SkillsPlayer skillsPlayer;

    public override void Awake()
    {
        base.Awake();
        myPlayerData = GetComponentInParent<Player>().GetMyPlayerData();
        skillsPlayer = GetComponentInParent<Modules>().GetComponentInChildren<SkillsPlayer>();
    }

    private void OnEnable()
    {
        ResetPlayerMana();
        myPlayerData.onManaChanged += CheckIfCanPlaySkills;
        skillsPlayer.onSkillPlayed += SpendMana;
    }

    private void OnDisable()
    {
        myPlayerData.onManaChanged -= CheckIfCanPlaySkills;
        skillsPlayer.onSkillPlayed -= SpendMana;
    }

    private void SpendMana(SkillData skill)
    {
        myPlayerData.SpendMana(skill.manaCost);
    }

    private void ResetPlayerMana()
    {
        myPlayerData.playerMana.currentMana = myPlayerData.playerMana.maxMana;
        myPlayerData.SpendMana(0);
    }

    public void CheckIfCanPlaySkills()
    {
        if (myPlayerData.playerMana.currentMana <= 0)
        {
            recharging = true;
            skillsPlayer.DisableSkillsPlayability();
        }
        if (myPlayerData.playerMana.currentMana >= myPlayerData.playerMana.maxMana)
        {
            skillsPlayer.EnableSkillsPlayability();
        }
    }
}