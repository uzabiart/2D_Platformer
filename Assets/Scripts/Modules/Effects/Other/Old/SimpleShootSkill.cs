using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleShootSkill : Skill
{
    public GameObject bulletPrefab;

    public override void UseSkill()
    {
        base.UseSkill();
        Transform newBullet = Instantiate(bulletPrefab).transform;
        newBullet.GetComponentInChildren<HitDetector>().UpdateMyInfo(GetMyPlayerInfo().playerId, mySkillData);
        newBullet.GetComponent<Entity>().entityId = myEntity.GetMyEntityId();
        newBullet.position = myEntity.transform.position;
        Rigidbody2D rigi = newBullet.GetComponent<Rigidbody2D>();

        Vector2 direction = transform.position - Vector3.zero;
        Vector3 slightlyRandomPosition = Vector3.zero;
        if (myTarget != null)
            slightlyRandomPosition = new Vector3(myTarget.position.x + Random.Range(-3f, 3f), myTarget.position.y + Random.Range(-3f, 3f), 1f);

        direction = transform.position - slightlyRandomPosition;

        rigi.AddForceAtPosition(direction.normalized * 300f * -2f, transform.position);
    }
}