using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitDetector : Module
{
    public Effect[] effects;
    string savedPlayerId;
    public Collider2D myCollider;
    public GameObject hitEffect;
    string avaiodTag = "NonObstacle";
    public bool dieOnImpact;
    public bool doIHitAll;
    bool hitDisabled;
    SkillData mySkillData;

    public override void Awake()
    {
        base.Awake();
        if (myEntity == null) return;
        gameObject.name = myEntity.gameObject.name + "_HitDetector";
    }

    private void Start()
    {
        Invoke(nameof(EnableMyCollider), 0.05f);
    }

    public void EnableMyCollider()
    {
        myCollider.enabled = true;
    }

    public void UpdateMyInfo(string playerId, SkillData newSkillData)
    {
        savedPlayerId = playerId;
        mySkillData = newSkillData;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!doIHitAll)
            CheckHit(collision);
        else
            CheckHitAll(collision);
    }

    void CheckHitAll(Collider2D collision)
    {
        if (collision.tag == avaiodTag) return;
        EffectsReceiver effectReceiver = collision.GetComponentInChildren<EffectsReceiver>();
        IdHolder idHolder = collision.GetComponent<IdHolder>();
        if (idHolder != null)
        {
            if (idHolder.GetMyPlayerId() == savedPlayerId)
                return;
        }
        if (effectReceiver != null)
        {
            foreach (Effect effect in effects)
            {
                effect.UpdateMyTarget(effectReceiver);
                effect.PlayMyEffect(mySkillData.damage);
            }
        }
        if (effectReceiver != null)
        {
            SpawnHitEffectOnTarget(effectReceiver.transform);
        }
        else
        {
            SpawnHitEffectOnTarget(collision.transform);
        }
    }

    void CheckHit(Collider2D collision)
    {
        if (hitDisabled) return;
        if (collision.tag == avaiodTag) return;
        EffectsReceiver effectReceiver = collision.GetComponentInChildren<EffectsReceiver>();
        IdHolder idHolder = collision.GetComponent<IdHolder>();
        if (idHolder != null)
        {
            if (idHolder.GetMyPlayerId() == savedPlayerId)
                return;
        }
        if (effectReceiver != null)
        {
            myEntity.transform.SetParent(effectReceiver.transform);
            foreach (Effect effect in effects)
            {
                effect.PlayMyEffect(mySkillData.damage);
            }
        }
        SpawnHitEffect();
        myCollider.enabled = false;
        hitDisabled = true;
        if (!dieOnImpact) return;
        Destroy(myEntity.gameObject);
    }

    void SpawnHitEffect()
    {
        Transform newEffect = Instantiate(hitEffect).transform;
        newEffect.position = transform.position;
    }

    void SpawnHitEffectOnTarget(Transform receiver)
    {
        Transform newEffect = Instantiate(hitEffect).transform;
        newEffect.position = receiver.transform.position;
    }
}