using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomSkill : Skill
{
    public GameObject mushroomPrefab;
    bool isCd = false;
    float cooldown = 5f;

    public override void UseSkill()
    {
        if (isCd) return;
        isCd = true;
        Transform newMushroom = Instantiate(mushroomPrefab).transform;
        newMushroom.position = myEntity.transform.position;
        MushroomBlowup mushroomLogic = newMushroom.GetComponentInChildren<MushroomBlowup>();
        Player myPlayer = GetComponentInParent<Player>();
        mushroomLogic.UpdateControllingPlayerId(myPlayer.GetMyPlayerId());
        mushroomLogic.UpdateMyColor(myPlayer.myColor);
        Invoke(nameof(CdOff), cooldown);
    }

    void CdOff()
    {
        isCd = false;
    }
}