using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeDaggerSkill : Skill
{
    public GameObject daggerSlashPrefab;

    public override void UseSkill()
    {
        Transform newSlash = Instantiate(daggerSlashPrefab).transform;
        newSlash.position = myEntity.transform.position;
        newSlash.GetComponentInChildren<HitDetector>().UpdateMyInfo(GetMyPlayerInfo().playerId, mySkillData);

        PlayerData opponentTransform = gameData.GetMyOpponentInfo(GetMyPlayerInfo().playerId);
        Vector2 opponentPosition = Vector2.zero;

        if (opponentTransform != null)
            opponentPosition = opponentTransform.playerSceneReference.position;

        opponentPosition = new Vector2(opponentPosition.x - myEntity.transform.position.x, opponentPosition.y - myEntity.transform.position.y);
        float angle = Mathf.Atan2(opponentPosition.y, opponentPosition.x) * Mathf.Rad2Deg;
        newSlash.localEulerAngles = new Vector3(0f, 0f, angle);
    }
}