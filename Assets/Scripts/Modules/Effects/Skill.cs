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

    protected Transform myTarget;
    protected Vector3 nextPositionForNpc;
    public GameObject coneForNpc;
    protected bool npcAttack;

    public Action<SkillData> onSkillPlayed;

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

    public void UseSkillbyPlayer()
    {
        if (skipDoubleClick) { skipDoubleClick = false; return; }
        skipDoubleClick = true;
        if (pressing) { StopUsingSkill(); pressing = false; }
        else { pressing = true; }
        UseSkillIfCdOff();
        StartCoroutine(PressingStart());
    }

    private void CheckMyTarget()
    {
        if (myTarget == null && gameData.players.Count > 1)
            myTarget = gameData.GetMyOpponentInfo(myEntity.GetMyEntityId()).playerSceneReference.transform;
    }

    public void UseSkillIfCdOff()
    {
        if (isCd) return;
        isCd = true;
        onSkillPlayed?.Invoke(mySkillData);
        UseSkill();
        StartCoroutine(CdOff(currentCooldown));
    }

    public void UseNpcSkill()
    {
        UseSkill();
    }

    public void UpdateMyTarget(Transform target)
    {
        myTarget = target;
    }

    public virtual void UseSkill()
    {
        CheckMyTarget();
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

        Invoke(nameof(ResetCooldowns), time);
    }

    void ResetCooldowns()
    {
        currentCooldown = cooldown;
    }

    public virtual void SetupNpcAttack()
    {
        if (coneForNpc == null) return;
        npcAttack = true;
        nextPositionForNpc = myTarget.position;
        coneForNpc.SetActive(true);
    }

    public virtual void HideNpcAttack()
    {
        if (coneForNpc == null) return;
        npcAttack = false;
        coneForNpc.SetActive(false);
    }
}