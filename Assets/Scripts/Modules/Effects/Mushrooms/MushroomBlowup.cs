using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MushroomBlowup : Module
{
    string myPlayerId;
    bool safetyOff;
    int myDamage;
    public GameObject hitEffect;
    public SpriteRenderer mySprite;
    public SpriteRenderer[] shroomViewGroup;

    private void Start()
    {
        Invoke(nameof(SafetyOffDelay), 0.4f);
        Invoke(nameof(HideShroomAfterDelay), 1f);
    }

    private void HideShroomAfterDelay()
    {
        foreach (SpriteRenderer sprite in shroomViewGroup)
        {
            sprite.DOFade(0f, 1.4f);
        }
    }

    public void UpdateControllingPlayerId(string playerId)
    {
        myPlayerId = playerId;
    }

    private void SafetyOffDelay()
    {
        safetyOff = true;
    }

    public void UpdateMe(int damage)
    {
        myDamage = damage;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IdHolder idHolder = collision.GetComponent<IdHolder>();
        if (idHolder != null)
        {
            if (idHolder.GetMyEntityId() == myPlayerId || idHolder.GetMyEntityId() == "") return;
            BlowUp(idHolder.GetComponentInChildren<Health>());
        }
    }

    private void BlowUp(Health health)
    {
        if (!safetyOff) return;
        if (health != null)
            health.TakeDamage(myDamage);
        Transform hitT = Instantiate(hitEffect).transform;
        hitT.position = transform.position;
        Destroy(myEntity.gameObject);
    }
}
