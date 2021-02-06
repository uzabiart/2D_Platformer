using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : Module, IHealth
{
    public HealthInfo health;
    public Image fillBar;
    string latestDamageSourceId;

    private void Start()
    {
        UpdateInfo();
    }

    public void TakeDamage(int damage, string damageSourceId)
    {
        latestDamageSourceId = damageSourceId;
        health.currentHealth -= damage;
        UpdateInfo();
    }

    public void TakeHealing(int healing)
    {
        health.currentHealth += healing;
        UpdateInfo();
    }

    public void TakePercentageHealing(float percentageHealing)
    {
        health.currentHealth += (int)(health.maxHealth * percentageHealing);
        //health.currentHealth += healing;
        UpdateInfo();
    }

    public void UpdateInfo()
    {
        fillBar.fillAmount = (float)health.currentHealth / (float)health.maxHealth;
        if (health.currentHealth <= 0)
        {
            Player getplayer = GetComponentInParent<Player>();
            if (getplayer != null)
            {
                PlayerData targetData = gameData.GetMyPlayerInfo(latestDamageSourceId);
                if(targetData!= null)
                {
                    targetData.AddGold(getplayer.GetMyPlayerData().giveGoldAmount);
                }
                gameContext.gameData.PlayerDead(getplayer.myPlayerData);
                getplayer.ManagePlayerDed();
            }
            else
            {
                Enemy getenemy = GetComponentInParent<Enemy>();
                if (getenemy != null)
                {
                    PlayerData targetdata = gameData.GetMyPlayerInfo(latestDamageSourceId);
                    if (targetdata != null)
                        targetdata.AddGold(getenemy.myEnemyData.giveGoldAmount);
                }
                Destroy(myEntity.gameObject);
            }
        }
    }
}