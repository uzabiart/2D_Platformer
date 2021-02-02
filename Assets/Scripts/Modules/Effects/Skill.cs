using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : Module
{
    PlayerData myPlayerInfo;
    public SkillData mySkillData;
    protected bool isCd;
    protected float cooldown = 5f;
    protected float cooldownLeft;
    protected float currentCooldown;

    bool pressing;
    bool skipDoubleClick;

    public override void Awake()
    {
        base.Awake();
        UpdateMyCooldown(mySkillData.cooldown);
        Player player = GetComponentInParent<Player>();
        if (player == null) return;
        myPlayerInfo = player.myPlayerData;
    }

    public void UpdateMe(SkillData data)
    {
        mySkillData = data;
        UpdateMyCooldown(data.cooldown);
    }

    public PlayerData GetMyPlayerInfo()
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
        if (skipDoubleClick) { skipDoubleClick = false; return; }
        skipDoubleClick = true;
        if (pressing) { StopUsingSkill(); pressing = false; }
        else { pressing = true; }
        if (isCd) return;
        isCd = true;
        UseSkill();
        StartCoroutine(CdOff(currentCooldown));
        StartCoroutine(PressingStart());
    }

    public virtual void UseSkill()
    {
    }

    public virtual void StopUsingSkill()
    {
    }

    private IEnumerator PressingStart()
    {
        yield return new WaitForSeconds(0.1f);
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

        //if (cooldown <= 1)
        //{
        //    currentCooldown = 0.2f;
        //}
        //else if (cooldown <= 2)
        //{
        //    currentCooldown = 0.5f;
        //}

        Invoke(nameof(ResetCooldowns), time);
    }

    void ResetCooldowns()
    {
        currentCooldown = cooldown;
    }
}