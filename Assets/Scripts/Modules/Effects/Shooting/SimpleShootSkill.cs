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
        Vector2 direction = transform.position - Vector3.zero;
        Vector3 slightlyRandomPosition = Vector3.zero;
        if (opponentTransform != null)
            slightlyRandomPosition = new Vector3(opponentTransform.playerSceneReference.position.x + Random.Range(-3f, 3f), opponentTransform.playerSceneReference.position.y + Random.Range(-3f, 3f), 1f);

        direction = transform.position - slightlyRandomPosition;

        rigi.AddForceAtPosition(direction.normalized * 300f * -2f, transform.position);
    }
}