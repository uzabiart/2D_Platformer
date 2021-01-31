using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MushroomBlowup : Module
{
    string myPlayerId;
    bool safetyOff;
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

    public void UpdateMyColor(Color newColor)
    {
        //mySprite.color = newColor;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player collidedPlayer = collision.GetComponent<Player>();
        if (collidedPlayer != null)
        {
            if (collidedPlayer.GetMyPlayerId() == myPlayerId) return;
            BlowUp(collidedPlayer.GetComponentInChildren<Health>());
        }
    }

    private void BlowUp(Health health)
    {
        if (!safetyOff) return;
        print("-> MUSHROOM EFFECT BLOWUP");
        health.TakeDamage(10);
        Transform hitT = Instantiate(hitEffect).transform;
        hitT.position = transform.position;
        Destroy(myEntity.gameObject);
    }
}
