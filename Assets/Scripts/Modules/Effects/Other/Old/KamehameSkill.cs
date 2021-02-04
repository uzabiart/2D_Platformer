using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KamehameSkill : Skill
{
    public GameObject kamehamePrefab;

    public GameObject loadingKameHameBar;
    public Image loadingFill;

    Modules modules;
    Movement myMovement;
    string animationRandomId;
    Transform newKamehame;

    bool canSteerKamehame;

    private void Start()
    {
        modules = GetComponentInParent<Modules>();
        myMovement = modules.GetComponentInChildren<Movement>();
        myMovement.onMove += SteerKamehame;
    }

    private void OnDisable()
    {
        myMovement.onMove -= SteerKamehame;
    }

    public override void UseSkill()
    {
        base.UseSkill();
        StopAllCoroutines();
        canSteerKamehame = true;
        myMovement.ChangeCurrentSpeed(0f);
        SetupKamehamePosition();
        StartCoroutine(KamehameSequence());
    }

    private void SetupKamehamePosition()
    {
        newKamehame = Instantiate(kamehamePrefab).transform;
        newKamehame.position = myEntity.transform.position;
        newKamehame.GetComponentInChildren<HitDetector>().UpdateMyInfo(myEntity.GetMyEntityId(), mySkillData);

        PlayerData opponentTransform = gameData.GetMyOpponentInfo(myEntity.GetMyEntityId());
        Vector2 opponentPosition = Vector2.zero;

        if (opponentTransform != null)
            opponentPosition = opponentTransform.playerSceneReference.position;

        opponentPosition = new Vector2(opponentPosition.x - myEntity.transform.position.x, opponentPosition.y - myEntity.transform.position.y);
        float angle = Mathf.Atan2(opponentPosition.y, opponentPosition.x) * Mathf.Rad2Deg;
        newKamehame.localEulerAngles = new Vector3(0f, 0f, angle);
    }

    private void SteerKamehame(Vector3 input)
    {
        if (newKamehame == null || !canSteerKamehame) return;
        float rotationSpeed = 0f;
        if (input.x > 0)
            rotationSpeed = -1;
        else if (input.x < 0)
            rotationSpeed = 1f;
        newKamehame.Rotate(new Vector3(0, 0, rotationSpeed * Time.deltaTime * 50f));
    }

    private IEnumerator KamehameSequence()
    {
        yield return new WaitForSeconds(2.5f);
        canSteerKamehame = false;
        myMovement.ResetSpeed();
        yield return new WaitForSeconds(6f);
        Destroy(newKamehame.gameObject);
    }
}