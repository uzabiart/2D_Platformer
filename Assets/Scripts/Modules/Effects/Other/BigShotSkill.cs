using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigShotSkill : Skill
{
    public GameObject bigBulletPrefab;

    public override void UseSkill()
    {
        base.UseSkill();
        Transform newBullet = Instantiate(bigBulletPrefab).transform;
        newBullet.GetComponentInChildren<HitDetector>().UpdateMyInfo(GetMyPlayerInfo().playerId, mySkillData);
        newBullet.position = myEntity.transform.position;
        newBullet.GetComponentInChildren<Entity>().entityId = myEntity.GetMyEntityId();
        Rigidbody2D rigi = newBullet.GetComponent<Rigidbody2D>();

        Vector3 direction = transform.position - Vector3.zero;
        if (myTarget != null)
            direction = transform.position - myTarget.position;

        rigi.AddForceAtPosition(direction.normalized * 230f * -2f, transform.position);
    }
}
