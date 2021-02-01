using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldSkill : Skill
{
    public GameObject shieldPrefab;
    Transform createdShield;
    float shieldDisableTime = 10f;

    public override void UseSkill()
    {
        createdShield = Instantiate(shieldPrefab, transform).transform;
        createdShield.localPosition = Vector3.zero;
        createdShield.GetComponent<ShieldEffect>().UpdateMe(shieldDisableTime);
        Invoke(nameof(DestroyShield), shieldDisableTime);
    }

    private void DestroyShield()
    {
        Destroy(createdShield.gameObject);
    }
}