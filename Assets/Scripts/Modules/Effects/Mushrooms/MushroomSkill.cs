using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomSkill : Skill
{
    public GameObject mushroomPrefab;

    private void Start()
    {
        UpdateMyCooldown(2.5f);
    }

    public override void UseSkill()
    {
        Transform newMushroom = Instantiate(mushroomPrefab).transform;
        newMushroom.position = myEntity.transform.position;
        MushroomBlowup mushroomLogic = newMushroom.GetComponentInChildren<MushroomBlowup>();
        Player myPlayer = GetComponentInParent<Player>();
        mushroomLogic.UpdateControllingPlayerId(myPlayer.GetMyPlayerId());
        mushroomLogic.UpdateMyColor(myPlayer.myPlayerInfo.myColor);
    }
}