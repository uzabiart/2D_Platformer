using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillsPlayer : MonoBehaviour
{
    InputController inputController;

    public Skill skillQ;

    private void Awake()
    {
        Modules modules = GetComponentInParent<Modules>();
        inputController = modules.GetComponentInChildren<InputController>();
    }

    private void OnEnable()
    {
        inputController.onSkillUseQ += UseQ;
    }

    private void OnDisable()
    {
        inputController.onSkillUseQ -= UseQ;
    }

    private void UseQ()
    {
        skillQ.UseSkill();
    }
}