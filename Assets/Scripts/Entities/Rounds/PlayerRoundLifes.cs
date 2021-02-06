using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PlayerRoundLifes : MonoBehaviour
{
    public GameObject[] playerHealths;
    public PlayerData holdedPlayerData;
    public Text killsAmount;
    public Text deathsAmount;
    public Text goldGathered;
    public Image manaFill;
    public GameObject reachargingMana;
    bool recharging;

    private void OnDisable()
    {
        if (holdedPlayerData == null) return;
        holdedPlayerData.onManaChanged -= UpdateMyManaView;
        holdedPlayerData.onGoldAmountChanged -= UpdateMyGoldView;
    }

    public void UpdateMyView(int playerLifes)
    {
        foreach (GameObject health in playerHealths)
        {
            health.SetActive(false);
        }
        for (int i = 0; i < playerLifes; i++)
        {
            playerHealths[i].SetActive(true);
        }
    }

    public void ResetScore()
    {
        holdedPlayerData = null;
        killsAmount.text = "0";
        deathsAmount.text = "0";
        goldGathered.text = "0";
    }

    public bool CheckAndUpdatePlayerData(PlayerData newPlayerData)
    {
        if (holdedPlayerData == null)
        {
            holdedPlayerData = newPlayerData;
            holdedPlayerData.onManaChanged += UpdateMyManaView;
            holdedPlayerData.onGoldAmountChanged += UpdateMyGoldView;
            UpdateMyManaView();
            return true;
        }
        return false;
    }

    private void UpdateMyManaView()
    {
        if (recharging) return;

            manaFill.fillAmount = (float)holdedPlayerData.playerMana.currentMana / (float)holdedPlayerData.playerMana.maxMana;
        if (holdedPlayerData.playerMana.currentMana <= 0)
        {
            recharging = true;
            reachargingMana.SetActive(true);
            manaFill.DOFillAmount(1f, holdedPlayerData.playerMana.rechargeTimeOfMaxMana + holdedPlayerData.playerMana.rechargeTimeOfMaxMana * (Mathf.Abs(holdedPlayerData.playerMana.currentMana) / holdedPlayerData.playerMana.maxMana)).SetEase(Ease.Linear).OnComplete(OnManaRecharged);
        }
    }

    private void UpdateMyGoldView()
    {
        goldGathered.text = holdedPlayerData.playerScore.gold.ToString();
    }

    void OnManaRecharged()
    {
        recharging = false;
        reachargingMana.SetActive(false);
        holdedPlayerData.ManaRefreshed();
    }

    public void UpdateMyScore()
    {
        if (holdedPlayerData == null) return;
        killsAmount.text = holdedPlayerData.playerScore.kills.ToString();
        deathsAmount.text = holdedPlayerData.playerScore.deaths.ToString();
    }
}