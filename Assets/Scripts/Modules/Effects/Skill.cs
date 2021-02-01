using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : Module
{
    PlayerInfo myPlayerInfo;
    public SkillData mySkillData;
    protected bool isCd;
    protected float cooldown = 5f;

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
    }

    public void useSkillIfAble()
    {
        if (isCd) return;
        isCd = true;
        UseSkill();
        StartCoroutine(CdOff(cooldown));
    }

    public virtual void UseSkill()
    {
    }

    protected IEnumerator CdOff(float cdTime)
    {
        yield return new WaitForSeconds(cdTime);
        isCd = false;
    }
}