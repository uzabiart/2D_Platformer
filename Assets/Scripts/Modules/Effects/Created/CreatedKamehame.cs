using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatedKamehame : Effect
{
    public override void PlayMyEffect(int damage)
    {
        myTarget.GetComponentInParent<Modules>().GetComponentInChildren<Health>().TakeDamage(damage, GetMyEntityId());
    }
}