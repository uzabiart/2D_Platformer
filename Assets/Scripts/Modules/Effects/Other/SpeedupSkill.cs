using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedupSkill : Skill
{
    public override void UseSkill()
    {
        GetComponentInParent<Modules>().GetComponentInChildren<Movement>().ChangeSpeedForATime(2f, 2f);
    }
}