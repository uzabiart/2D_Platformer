using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomSkill : Skill
{
    public GameObject mushroomPrefab;

    public override void UseSkill()
    {
        Transform newMushroom = Instantiate(mushroomPrefab).transform;
        newMushroom.position = myEntity.transform.position;
        MushroomBlowup mushroomLogic = newMushroom.GetComponentInChildren<MushroomBlowup>();
        Player myPlayer = GetComponentInParent<Player>();
        mushroomLogic.UpdateControllingPlayerId(myPlayer.GetMyPlayerId());
        mushroomLogic.UpdateMe(mySkillData.damage);
    }
}