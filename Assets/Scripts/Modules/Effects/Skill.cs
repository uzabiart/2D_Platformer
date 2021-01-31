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
        myPlayerInfo = GetComponentInParent<Player>().myPlayerInfo;
        if (myPlayerInfo == null) return;
    }

    public void UpdateMe(SkillData data)
    {
        mySkillData = data;
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