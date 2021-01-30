using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleBulletCreated : Effect
{
    public override void PlayMyEffect()
    {
        GetComponentInParent<Modules>().GetComponentInChildren<Health>().TakeDamage(10);
        Invoke(nameof(DestroyAfterDelay), 1f);
    }

    void DestroyAfterDelay()
    {
        Destroy(gameObject);
    }
}