using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatedMissile : Effect
{
    public Rigidbody2D rigi;
    float randomizedDirection;

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
        GetComponentInParent<Modules>().GetComponentInChildren<Health>().TakeDamage(damage);
    }

    private IEnumerator MissileSequences()
    {
        yield return new WaitForSeconds(0.2f);
        randomizedDirection = UnityEngine.Random.Range(-4.5f, 4.5f);
        StartCoroutine(MissileSequences());
    }

    private void DestroyMeAfterDelay()
    {
        StopAllCoroutines();
        Destroy(gameObject);
    }
}
