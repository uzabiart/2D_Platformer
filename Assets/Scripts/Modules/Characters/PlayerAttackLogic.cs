using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerAttackLogic : Module
{
    Sensor mySensor;
    public GameObject aggroView;
    public SkillsPlayer skillsPlayer;

    public override void Awake()
    {
        base.Awake();
        mySensor = GetComponentInChildren<Sensor>();
    }

    private void OnEnable()
    {
        mySensor.onTargetAdded += StartMinionAggro;
        mySensor.onTargetLost += FinishMinionAggro;
    }

    private void OnDisable()
    {
        mySensor.onTargetAdded -= StartMinionAggro;
        mySensor.onTargetLost -= FinishMinionAggro;
    }

    private void StartMinionAggro(Transform target)
    {
        aggroView.SetActive(true);
        skillsPlayer.UpdateMyMinoinTarget(target);
    }

    private void FinishMinionAggro(Transform target)
    {
        if (mySensor.targets.Count == 0)
        {
            aggroView.SetActive(false);
            skillsPlayer.ClearMyMinoinTarget();
        }
    }
}