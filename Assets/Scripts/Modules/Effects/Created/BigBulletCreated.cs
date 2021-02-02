using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigBulletCreated : Effect
{
    private void Start()
    {
        Invoke(nameof(DestroyMeAfterDelay), 6f);
    }

    public override void PlayMyEffect(int damage)
    {
        Modules modules = myTarget.GetComponentInParent<Modules>();
        if (modules == null) return;
        myTarget.GetComponentInParent<Modules>().GetComponentInChildren<Health>().TakeDamage(damage);
    }

    private void DestroyMeAfterDelay()
    {
        Destroy(gameObject);
    }
}
