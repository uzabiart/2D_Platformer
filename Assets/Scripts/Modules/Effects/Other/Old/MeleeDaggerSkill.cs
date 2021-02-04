using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeDaggerSkill : Skill
{
    public GameObject daggerSlashPrefab;

    public override void UseSkill()
    {
        base.UseSkill();
        Transform newSlash = Instantiate(daggerSlashPrefab).transform;
        newSlash.position = myEntity.transform.position;
        newSlash.GetComponentInChildren<HitDetector>().UpdateMyInfo(myEntity.GetMyEntityId(), mySkillData);

        Transform targetTransform = myTarget;
        Vector2 opponentPosition = Vector2.zero;

        if (targetTransform != null)
            opponentPosition = targetTransform.position;

        if (npcAttack)
        {
            newSlash.localEulerAngles = new Vector3(0f, 0f, coneForNpc.transform.localEulerAngles.z);
        }
        else
        {
            opponentPosition = new Vector2(opponentPosition.x - myEntity.transform.position.x, opponentPosition.y - myEntity.transform.position.y);
            float angle = Mathf.Atan2(opponentPosition.y, opponentPosition.x) * Mathf.Rad2Deg;
            newSlash.localEulerAngles = new Vector3(0f, 0f, angle);
        }
    }

    public override void SetupNpcAttack()
    {
        base.SetupNpcAttack();
        nextPositionForNpc = new Vector2(nextPositionForNpc.x - myEntity.transform.position.x, nextPositionForNpc.y - myEntity.transform.position.y);
        float angle = Mathf.Atan2(nextPositionForNpc.y, nextPositionForNpc.x) * Mathf.Rad2Deg;
        coneForNpc.transform.localEulerAngles = new Vector3(0f, 0f, angle);

    }
}