using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickMeUp : Module
{
    public GameObject pickupEffect;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();
        if (player == null) return;
        SkillsPlayer skillsPlayer = player.GetComponentInChildren<SkillsPlayer>();
        PickupSkill myPickupSkill = GetComponentInParent<PickupSkill>();
        skillsPlayer.PickUpNewSkill(myPickupSkill.mySkillData);
        Transform newPickupEffect = Instantiate(pickupEffect).transform;
        newPickupEffect.position = transform.position;
        Destroy(myEntity.gameObject);
    }
}