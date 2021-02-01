using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class HealPotionSkill : Skill
{
    public GameObject healingBar;
    public Image healingFillBar;

    float timeNeededToHeal = 2f;
    int healAmount = 10;

    Modules modules;
    Health myHealth;
    Movement myMovement;
    string animationRandomId;
    bool healed;

    private void Start()
    {
        modules = GetComponentInParent<Modules>();
        myHealth = modules.GetComponentInChildren<Health>();
        myMovement = modules.GetComponentInChildren<Movement>();
        animationRandomId = UnityEngine.Random.Range(0, 999999).ToString();
    }

    public override void UseSkill()
    {
        healed = false;
        DOTween.Kill("HealingSelf_" + animationRandomId);
        healingBar.SetActive(true);
        healingFillBar.fillAmount = 0f;
        healingFillBar.DOFillAmount(1f, timeNeededToHeal).OnComplete(ApplyHealing).SetEase(Ease.Linear).SetId("HealingSelf_" + animationRandomId);
        myMovement.ChangeCurrentSpeed(0f);
    }

    public override void StopUsingSkill()
    {
        if (healed) return;
        DOTween.Kill("HealingSelf_" + animationRandomId);
        myMovement.ResetSpeed();
        cooldownLeft = 0f;
        healingBar.SetActive(false);
    }

    public void ApplyHealing()
    {
        healed = true;
        myHealth.TakeHealing(healAmount);
        healingBar.SetActive(false);
        myMovement.ResetSpeed();
    }
}