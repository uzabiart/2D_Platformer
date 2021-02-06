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
    public Skill nextSkillToUse;
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
        if (nextSkillToUse != null)
            nextSkillToUse.HideNpcAttack();
        if (nextSkillToUse == null)
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
        if (nextSkillToUse == null) return;
        nextSkillToUse.UseNpcSkill();
        nextSkillToUse.HideNpcAttack();
        nextSkillToUse = null;
        if (!attacking) { return; }
        StartCoroutine(AttackCooldown());
    }

    void UpdateMyTarget()
    {
        if (mySensor.targets.Count <= 0) { nextSkillToUse = null; return; }
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