using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerRoundLifes : MonoBehaviour
{
    public GameObject[] playerHealths;
    public PlayerData holdedPlayerData;
    public Text killsAmount;
    public Text deathsAmount;

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
    }

    public bool CheckAndUpdatePlayerData(PlayerData newPlayerData)
    {
        if (holdedPlayerData == null)
        {
            holdedPlayerData = newPlayerData;
            return true;
        }
        return false;
    }

    public void UpdateMyScore()
    {
        if (holdedPlayerData == null) return;
        killsAmount.text = holdedPlayerData.playerScore.kills.ToString();
        deathsAmount.text = holdedPlayerData.playerScore.deaths.ToString();
    }
}