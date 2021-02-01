using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : Module
{
    PlayerInfo myPlayerInfo;
    public SkillData mySkillData;
    protected bool isCd;
    protected float cooldown = 5f;
    float cooldownLeft;
    float currentCooldown;

    public override void Awake()
    {
        base.Awake();
        UpdateMyCooldown(mySkillData.cooldown);
        Player player = GetComponentInParent<Player>();
        if (player == null) return;
        myPlayerInfo = player.myPlayerInfo;
    }

    public void UpdateMe(SkillData data)
    {
        mySkillData = data;
        UpdateMyCooldown(data.cooldown);
    }

    public PlayerInfo GetMyPlayerInfo()
    {
        return myPlayerInfo;
    }

    protected void UpdateMyCooldown(float cd)
    {
        cooldown = cd;
        currentCooldown = cooldown;
    }

    public void useSkillIfAble()
    {
        if (isCd) return;
        isCd = true;
        UseSkill();
        StartCoroutine(CdOff(currentCooldown));
    }

    public virtual void UseSkill()
    {
    }

    protected IEnumerator CdOff(float cdTime)
    {
        cooldownLeft = cdTime;
        while (cooldownLeft > 0)
        {
            cooldownLeft -= 0.1f;
            yield return new WaitForSeconds(0.1f);
        }
        isCd = false;
    }

    public void ShortenMyCooldownForDuration(float cdMod, float time)
    {
        cooldownLeft -= (cooldown * cdMod);
        currentCooldown = cooldown - (cooldown * cdMod);

        //if (cooldown < 3)
        //{
        //    currentCooldown = 0.1f;
        //}

        Invoke(nameof(ResetCooldowns), time);
    }

    void ResetCooldowns()
    {
        currentCooldown = cooldown;
    }
}