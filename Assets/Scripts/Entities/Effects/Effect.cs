using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect : Entity
{
    public EffectsReceiver myTarget;

    public void UpdateMyTarget(EffectsReceiver newReceiver)
    {
        myTarget = newReceiver;
    }

    public virtual void PlayMyEffect(int damage)
    {
    }
}