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
    public Transform sensor;
    InputController inputController;
    bool doubleClick;
    bool pressing;

    public override void Awake()
    {
        base.Awake();
        inputController = GetComponentInParent<Modules>().GetComponentInChildren<InputController>();
        mySensor = GetComponentInChildren<Sensor>();
    }

    private void OnEnable()
    {
        mySensor.onTargetAdded += StartMinionAggro;
        mySensor.onTargetLost += FinishMinionAggro;
        inputController.onIncreaseRange += IncreaseSensorRange;
    }

    private void OnDisable()
    {
        mySensor.onTargetAdded -= StartMinionAggro;
        mySensor.onTargetLost -= FinishMinionAggro;
        inputController.onIncreaseRange -= IncreaseSensorRange;
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

    private void IncreaseSensorRange()
    {
        if (doubleClick) { doubleClick = false; return; }
        doubleClick = true;
        if (pressing) { pressing = false; DecreaseRange(); return; }
        pressing = true;
        sensor.localScale *= 5f;
    }

    private void DecreaseRange()
    {
        sensor.localScale /= 5f;
    }
}