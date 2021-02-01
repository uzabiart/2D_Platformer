using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DashSkill : Skill
{
    Modules modules;
    Movement movement;

    private void Start()
    {
        modules = GetComponentInParent<Modules>();
        movement = modules.GetComponentInChildren<Movement>();
    }

    public override void UseSkill()
    {
        if (movement.GetMyMovingVector() == Vector3.zero) return;
        myEntity.transform.DOMove(myEntity.transform.position + (movement.GetMyMovingVector() * 5f), 0.2f);
    }
}