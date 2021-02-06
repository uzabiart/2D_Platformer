using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatedMissile : Effect
{
    public Rigidbody2D rigi;
    float randomizedDirection = 0;

    private void Start()
    {
        Invoke(nameof(DestroyMeAfterDelay), 6f);
        StartCoroutine(MissileSequences());
    }

    private void Update()
    {
        rigi.AddForce(transform.up * randomizedDirection);
        float angle = Mathf.Atan2(rigi.velocity.y, rigi.velocity.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    public override void PlayMyEffect(int damage)
    {
        GetComponentInParent<Modules>().GetComponentInChildren<Health>().TakeDamage(damage, GetMyEntityId());
    }

    private IEnumerator MissileSequences()
    {
        if (randomizedDirection == 0)
            randomizedDirection = 2;
        yield return new WaitForSeconds(0.2f);
        if (randomizedDirection == -4) randomizedDirection = 4;
        else randomizedDirection = -4;
        //randomizedDirection = UnityEngine.Random.Range(-2f, 2f);
        StartCoroutine(MissileSequences());
    }

    private void DestroyMeAfterDelay()
    {
        StopAllCoroutines();
        Destroy(gameObject);
    }
}
