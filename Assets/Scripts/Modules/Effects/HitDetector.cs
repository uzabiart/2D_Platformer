using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitDetector : Module
{
    public string[] lookForATag;
    public Effect[] effects;
    string savedPlayerId;
    public Collider2D myCollider;
    public GameObject hitEffect;

    private void Start()
    {
        Invoke(nameof(EnableMyCollider), 0.1f);
    }

    public void EnableMyCollider()
    {
        myCollider.enabled = true;
    }

    public void UpdateMyInfo(string playerId)
    {
        savedPlayerId = playerId;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (lookForATag.Length != 0) return;
        EffectsReceiver effectReceiver = collision.GetComponentInChildren<EffectsReceiver>();
        Player hitPlayer = collision.GetComponent<Player>();
        if (hitPlayer != null)
        {
            if (hitPlayer.GetMyPlayerId() == savedPlayerId)
                return;
        }
        if (effectReceiver != null)
        {
            myEntity.transform.SetParent(effectReceiver.transform);
            foreach (Effect effect in effects)
            {
                effect.PlayMyEffect();
            }
        }
        Transform newEffect = Instantiate(hitEffect).transform;
        newEffect.position = transform.position;
        Destroy(myEntity.gameObject);
    }
}