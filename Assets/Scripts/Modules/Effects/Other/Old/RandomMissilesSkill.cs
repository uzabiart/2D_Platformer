using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMissilesSkill : Skill
{
    public GameObject missilePrefab;
    int homManyMissiles = 3;

    public override void UseSkill()
    {
        StartCoroutine(SpawnMissilesDelay());
    }

    private IEnumerator SpawnMissilesDelay()
    {
        for (int i = 0; i < homManyMissiles; i++)
        {
            SpawnRandomMissle();
            yield return new WaitForSeconds(0.2f);
        }
    }

    private void SpawnRandomMissle()
    {
        Transform newBullet = Instantiate(missilePrefab).transform;
        newBullet.GetComponentInChildren<HitDetector>().UpdateMyInfo(GetMyPlayerInfo().playerId, mySkillData);
        newBullet.position = myEntity.transform.position;
        Rigidbody2D rigi = newBullet.GetComponent<Rigidbody2D>();

        PlayerData opponentTransform = gameData.GetMyOpponentInfo(GetMyPlayerInfo().playerId);
        Vector2 direction = transform.position - Vector3.zero;
        Vector3 slightlyRandomPosition = Vector3.zero;
        if (opponentTransform != null)
            slightlyRandomPosition = new Vector3(opponentTransform.playerSceneReference.position.x + UnityEngine.Random.Range(-2f, 2f), opponentTransform.playerSceneReference.position.y + UnityEngine.Random.Range(-2f, 2f), 1f);

        direction = transform.position - slightlyRandomPosition;

        rigi.AddForceAtPosition(direction.normalized * 350f * -2f, transform.position);
    }
}