﻿using System.Collections;
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
        Vector2 direction = transform.position - Vector3.zero;
        Vector3 slightlyRandomPosition = Vector3.zero;
        if (opponentTransform != null)
            slightlyRandomPosition = new Vector3(opponentTransform.playerSceneReference.position.x + Random.Range(-1f, 1f), opponentTransform.playerSceneReference.position.y + Random.Range(-1f, 1f), 1f);

        direction = transform.position - slightlyRandomPosition;

        rigi.AddForceAtPosition(direction.normalized * 180f * -2f, transform.position);
    }
}