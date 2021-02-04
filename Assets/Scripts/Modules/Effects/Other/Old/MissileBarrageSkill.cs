using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileBarrageSkill : Skill
{
    public GameObject missilePrefab;
    int homManyMissiles = 25;

    public override void UseSkill()
    {
        StartCoroutine(SpawnMissilesDelay());
    }

    private IEnumerator SpawnMissilesDelay()
    {
        for (int i = 0; i < homManyMissiles; i++)
        {
            SpawnRandomMissle();
            yield return new WaitForSeconds(0.05f);
        }
    }

    private void SpawnRandomMissle()
    {
        base.UseSkill();
        Transform newBullet = Instantiate(missilePrefab).transform;
        newBullet.GetComponentInChildren<HitDetector>().UpdateMyInfo(myEntity.GetMyEntityId(), mySkillData);
        newBullet.position = myEntity.transform.position;
        Rigidbody2D rigi = newBullet.GetComponent<Rigidbody2D>();

        Vector2 direction = transform.position - Vector3.zero;
        Vector3 slightlyRandomPosition = new Vector3(UnityEngine.Random.Range(-50f, 50f), UnityEngine.Random.Range(-50f, 50f), 1f);
        if (myTarget != null)
            slightlyRandomPosition = new Vector3(myTarget.position.x + UnityEngine.Random.Range(-7f, 7f), myTarget.position.y + UnityEngine.Random.Range(-7f, 7f), 1f);

        direction = transform.position - slightlyRandomPosition;

        rigi.AddForceAtPosition(direction.normalized * 240f * -2f, transform.position);
    }
}
