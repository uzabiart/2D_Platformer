using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigShotSkill : Skill
{
    public GameObject bigBulletPrefab;

    public override void UseSkill()
    {
        Transform newBullet = Instantiate(bigBulletPrefab).transform;
        newBullet.GetComponentInChildren<HitDetector>().UpdateMyInfo(GetMyPlayerInfo().playerId, mySkillData);
        newBullet.position = myEntity.transform.position;
        Rigidbody2D rigi = newBullet.GetComponent<Rigidbody2D>();

        PlayerInfo opponentTransform = gameData.GetMyOpponentInfo(GetMyPlayerInfo().playerId);

        Vector2 direction = transform.position - opponentTransform.playerSceneReference.position;

        rigi.AddForceAtPosition(direction.normalized * 180f * -2f, transform.position);
    }
}
