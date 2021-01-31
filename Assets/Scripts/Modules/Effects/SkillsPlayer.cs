using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillsPlayer : MonoBehaviour
{
    InputController inputController;

    public Skill basicSkill;
    public Skill ultiSkill;
    public Skill dashSkill;

    private void Awake()
    {
        Modules modules = GetComponentInParent<Modules>();
        inputController = modules.GetComponentInChildren<InputController>();
    }

    private void OnEnable()
    {
        inputController.onSkillUseQ += UseBasic;
        inputController.onSkillUltiUsed += UseUlti;
        inputController.onDashUsed += UseDash;
    }

    private void OnDisable()
    {
        inputController.onSkillUseQ -= UseBasic;
        inputController.onSkillUltiUsed -= UseUlti;
        inputController.onDashUsed -= UseDash;
    }

    private void UseBasic()
    {
        if (basicSkill == null) return;
        basicSkill.useSkillIfAble();
    }

    private void UseUlti()
    {
        if (ultiSkill == null) return;
        ultiSkill.useSkillIfAble();
    }

    private void UseDash()
    {
        if (dashSkill == null) return;
        dashSkill.useSkillIfAble();
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
            case Enums.SkillType.Ulti:
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
        }
        return false;
    }
}