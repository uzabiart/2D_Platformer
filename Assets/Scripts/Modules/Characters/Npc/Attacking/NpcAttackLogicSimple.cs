using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class NpcAttackLogicSimple : Module
{
    Sensor mySensor;
    public float attackCooldown;
    public GameObject aggroView;
    public UnityEvent onTargetAdded;
    public UnityEvent onNoTargets;

    public Skill[] mySkills;
    Skill nextSkillToUse;
    bool attacking;

    public override void Awake()
    {
        base.Awake();
        mySensor = GetComponentInChildren<Sensor>();
    }

    private void OnEnable()
    {
        mySensor.onTargetAdded += StartAggro;
        mySensor.onTargetLost += FinishAggro;
    }

    private void OnDisable()
    {
        mySensor.onTargetAdded -= StartAggro;
        mySensor.onTargetLost -= FinishAggro;
    }

    void StartAggro(Transform target)
    {
        aggroView.SetActive(true);
        onTargetAdded?.Invoke();
        StopAllCoroutines();
        if (nextSkillToUse != null)
            nextSkillToUse.HideNpcAttack();
        StartCoroutine(FirstAttackDelay());
        attacking = true;
    }

    void FinishAggro(Transform target)
    {
        if (mySensor.targets.Count == 0)
        {
            aggroView.SetActive(false);
            StopAttack();
            onNoTargets?.Invoke();
        }
    }

    IEnumerator FirstAttackDelay()
    {
        yield return null;
        StartCoroutine(AttackCooldown());
    }

    IEnumerator AttackCooldown()
    {
        yield return new WaitForSeconds(attackCooldown * 0.4f);
        UpdateMyTarget();
        yield return new WaitForSeconds(attackCooldown);
        Attack();
    }

    void Attack()
    {
        nextSkillToUse.UseNpcSkill();
        nextSkillToUse.HideNpcAttack();
        if (!attacking) return;
        StartCoroutine(AttackCooldown());
    }

    void UpdateMyTarget()
    {
        if (mySensor.targets.Count <= 0) return;
        int randomSkillId = UnityEngine.Random.Range(0, mySkills.Length);
        nextSkillToUse = mySkills[randomSkillId];
        Transform choosedTarget = mySensor.targets[UnityEngine.Random.Range(0, mySensor.targets.Count)];
        nextSkillToUse.UpdateMyTarget(choosedTarget);
        nextSkillToUse.SetupNpcAttack();
    }

    void StopAttack()
    {
        attacking = false;
    }
}