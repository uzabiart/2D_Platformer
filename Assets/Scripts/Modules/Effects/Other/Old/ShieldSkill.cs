using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldSkill : Skill
{
    public GameObject shieldPrefab;
    Transform createdShield;
    float shieldDisableTime = 10f;
    Player myPlayer;

    private void Start()
    {
        myPlayer = GetComponentInParent<Player>();
    }

    public override void UseSkill()
    {
        if (createdShield != null)
            DestroyShield();

        createdShield = Instantiate(shieldPrefab, transform).transform;
        createdShield.localPosition = Vector3.zero;
        createdShield.GetComponent<ShieldEffect>().UpdateMe(shieldDisableTime);

        myPlayer.gameObject.tag = "NonObstacle";

        Invoke(nameof(DestroyShield), shieldDisableTime);
    }

    private void DestroyShield()
    {
        myPlayer.gameObject.tag = "Player";
        Destroy(createdShield.gameObject);
    }

    private void OnDestroy()
    {
        myPlayer.gameObject.tag = "Player";
    }
}