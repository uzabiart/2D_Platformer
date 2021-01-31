﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleBulletCreated : Effect
{
    private void Start()
    {
        Invoke(nameof(DestroyMeAfterDelay), 3f);
    }

    public override void PlayMyEffect()
    {
        GetComponentInParent<Modules>().GetComponentInChildren<Health>().TakeDamage(10);
    }

    private void DestroyMeAfterDelay()
    {
        Destroy(gameObject);
    }
}