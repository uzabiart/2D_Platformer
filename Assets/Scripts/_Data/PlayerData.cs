using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "UMI/Player Data", fileName = "Player")]
public class PlayerData : ScriptableObject
{
    public string playerId;
    public int playerLifes;
    public ManaInfo playerMana;
    public PlayerScoreInfo playerScore;
    public Color myColor;
    public Transform playerSceneReference;
    public Player playerLogic;
    public int giveGoldAmount;

    public Action onManaChanged;
    public Action onGoldAmountChanged;

    public void SpendMana(int manaSpend)
    {
        playerMana.currentMana -= manaSpend;
        onManaChanged?.Invoke();
    }

    public void AddMana(int addMana)
    {
        playerMana.currentMana += addMana;
        onManaChanged?.Invoke();
    }

    public void ManaRefreshed()
    {
        playerMana.currentMana = playerMana.maxMana;
        onManaChanged?.Invoke();
    }

    public void AddGold(int goldAmount)
    {
        playerScore.gold += goldAmount;
        onGoldAmountChanged?.Invoke();
    }

    public void SpendGold(int goldAmount)
    {
        playerScore.gold -= goldAmount;
        onGoldAmountChanged?.Invoke();
    }
}

[System.Serializable]
public class ManaInfo
{
    public float rechargeTimeOfMaxMana;
    public int maxMana;
    public int currentMana;
}