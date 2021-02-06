using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillsPlayer : MonoBehaviour
{
    InputController inputController;
    bool canPlay = true;

    public Transform myMinoinTarget;

    public Skill basicSkill;
    public Skill ultiSkill;
    public Skill dashSkill;

    public Action<SkillData> onSkillPlayed;

    private void Awake()
    {
        Modules modules = GetComponentInParent<Modules>();
        inputController = modules.GetComponentInChildren<InputController>();
    }

    private void OnEnable()
    {
        if (inputController == null) return;
        inputController.onSkillUseQ += UseBasic;
        inputController.onSkillUltiUsed += UseUlti;
        inputController.onDashUsed += UseDash;
    }

    private void OnDisable()
    {
        if (inputController == null) return;
        inputController.onSkillUseQ -= UseBasic;
        inputController.onSkillUltiUsed -= UseUlti;
        inputController.onDashUsed -= UseDash;
    }


    public void UpdateMyMinoinTarget(Transform minion)
    {
        myMinoinTarget = minion;
    }

    public void ClearMyMinoinTarget()
    {
        myMinoinTarget = null;
    }

    private void UseBasic()
    {
        if (basicSkill == null) return;
        if (!canPlay && basicSkill.mySkillData.manaCost != 0) return;
        basicSkill.onSkillPlayed += InvokeSkillPlayed;
        basicSkill.UpdateMyTarget(myMinoinTarget);
        basicSkill.UseSkillbyPlayer();
        basicSkill.onSkillPlayed -= InvokeSkillPlayed;
    }

    private void UseUlti()
    {
        if (ultiSkill == null) return;
        if (!canPlay && ultiSkill.mySkillData.manaCost != 0) return;
        ultiSkill.onSkillPlayed += InvokeSkillPlayed;
        ultiSkill.UpdateMyTarget(myMinoinTarget);
        ultiSkill.UseSkillbyPlayer();
        ultiSkill.onSkillPlayed -= InvokeSkillPlayed;
    }

    private void UseDash()
    {
        if (dashSkill == null) return;
        dashSkill.onSkillPlayed += InvokeSkillPlayed;
        dashSkill.UpdateMyTarget(myMinoinTarget);
        dashSkill.UseSkillbyPlayer();
        dashSkill.onSkillPlayed -= InvokeSkillPlayed;
    }

    private void InvokeSkillPlayed(SkillData skillData)
    {
        onSkillPlayed?.Invoke(skillData);
    }

    public void PickUpNewSkill(SkillData skill)
    {
        Skill newSkill = null;
        switch (skill.type)
        {
            case Enums.SkillType.Basic:
                if (basicSkill != null)
                    Destroy(basicSkill.gameObject);
                newSkill = Instantiate(skill.skillPrefab, transform).GetComponent<Skill>();
                basicSkill = newSkill;
                break;
            case Enums.SkillType.Dash:
                if (dashSkill != null)
                    Destroy(dashSkill.gameObject);
                newSkill = Instantiate(skill.skillPrefab, transform).GetComponent<Skill>();
                dashSkill = newSkill;
                break;
            case Enums.SkillType.Ulti:
                if (ultiSkill != null)
                    Destroy(ultiSkill.gameObject);
                newSkill = Instantiate(skill.skillPrefab, transform).GetComponent<Skill>();
                ultiSkill = newSkill;
                break;
        }
        newSkill.UpdateMe(skill);
    }

    public bool CheckIfAlreadyHaveThisSkill(SkillData data)
    {
        switch (data.type)
        {
            case Enums.SkillType.Basic:
                if (basicSkill == null)
                    return false;
                else
                {
                    if (basicSkill.mySkillData == data)
                    {
                        return true;
                    }
                }
                break;
            case Enums.SkillType.Dash:
                if (dashSkill == null)
                    return false;
                else
                {
                    if (dashSkill.mySkillData == data)
                    {
                        return true;
                    }
                }
                break;
            case Enums.SkillType.Ulti:
                if (ultiSkill == null)
                    return false;
                else
                {
                    if (ultiSkill.mySkillData == data)
                    {
                        return true;
                    }
                }
                break;
        }
        return false;
    }

    public void DisableSkillsPlayability()
    {
        canPlay = false;
    }
    public void EnableSkillsPlayability()
    {
        canPlay = true;
    }
}