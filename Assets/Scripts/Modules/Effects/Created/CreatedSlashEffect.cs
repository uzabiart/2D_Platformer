using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatedSlashEffect : Effect
{
    public Animator myAnimator;

    private void Start()
    {
        myAnimator.enabled = true;
        int randomId = UnityEngine.Random.Range(0, 2);
        if (randomId == 0)
            myAnimator.Play("SimpleSlashAnimation");
        else
            myAnimator.Play("SimpleSlashAnimationBackwards");
    }

    public override void PlayMyEffect(int damage)
    {
        GetComponentInParent<Modules>().GetComponentInChildren<Health>().TakeDamage(damage);
    }
}