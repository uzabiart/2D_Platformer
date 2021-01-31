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
    }

    private void OnDisable()
    {
        inputController.onSkillUseQ -= UseBasic;
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
        switch (skill.type)
        {
            case Enums.SkillType.Basic:
                SwapNewSkill(skill, basicSkill);
                break;
            case Enums.SkillType.Dash:
                SwapNewSkill(skill, dashSkill);
                break;
            case Enums.SkillType.Ulti:
                SwapNewSkill(skill, ultiSkill);
                break;
        }
    }

    void SwapNewSkill(SkillData skill, Skill oldSkill)
    {
        if (oldSkill != null)
            Destroy(oldSkill.gameObject);
        Skill newSkill = Instantiate(skill.skillPrefab, transform).GetComponent<Skill>();
        basicSkill = newSkill;
    }
}