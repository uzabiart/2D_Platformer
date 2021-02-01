using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedupSkill : Skill
{
    float cooldownMod = 0.6f;
    float cooldownSpeedTime = 1.5f;
    float speedMod = 2f;
    float speedTime = 2f;

    public override void UseSkill()
    {
        Modules myModules = GetComponentInParent<Modules>();
        myModules.GetComponentInChildren<Movement>().ChangeSpeedForATime(speedMod, speedTime);
        Skill[] allMySkills = myModules.GetComponentInChildren<SkillsPlayer>().GetComponentsInChildren<Skill>();
        foreach (Skill skill in allMySkills)
        {
            if (skill != this)
                skill.ShortenMyCooldownForDuration(cooldownMod, cooldownSpeedTime);
        }
    }
}