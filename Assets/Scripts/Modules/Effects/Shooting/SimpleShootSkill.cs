using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleShootSkill : Skill
{
    public GameObject bulletPrefab;

    private void Start()
    {
        UpdateMyCooldown(0.5f);
    }

    public override void UseSkill()
    {
        Transform newBullet = Instantiate(bulletPrefab).transform;
        newBullet.GetComponentInChildren<HitDetector>().UpdateMyInfo(GetMyPlayerInfo().playerId);
        newBullet.position = myEntity.transform.position;
        Rigidbody2D rigi = newBullet.GetComponent<Rigidbody2D>();

        PlayerInfo opponentTransform = gameData.GetMyOpponentInfo(GetMyPlayerInfo().playerId);
        Vector3 direction = transform.position - Vector3.zero;

        if (opponentTransform != null)
            direction = transform.position - opponentTransform.playerSceneReference.position;

        rigi.AddForceAtPosition(direction.normalized * 300f * -5f, transform.position);
    }
}