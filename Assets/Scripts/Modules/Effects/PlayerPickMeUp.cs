using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickMeUp : Module
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();
        if (player == null) return;
        SkillsPlayer skillsPlayer = player.GetComponentInChildren<SkillsPlayer>();
        PickupSkill myPickupSkill = GetComponentInParent<PickupSkill>();
        skillsPlayer.PickUpNewSkill(myPickupSkill.mySkillData);
        Destroy(myEntity.gameObject);
    }
}