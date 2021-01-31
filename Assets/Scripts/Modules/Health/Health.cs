using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : Module, IHealth
{
    public HealthInfo health;
    public Image fillBar;

    private void Start()
    {
        UpdateInfo();
    }

    public void TakeDamage(int damage)
    {
        health.currentHealth -= damage;
        UpdateInfo();
    }

    public void TakeHealing(int healing)
    {
        health.currentHealth += healing;
        UpdateInfo();
    }

    public void UpdateInfo()
    {
        fillBar.fillAmount = (float)health.currentHealth / (float)health.maxHealth;
        if (health.currentHealth <= 0)
        {
            gameContext.gameData.PlayerDead(myEntity.GetComponent<Player>().myPlayerInfo);
            Destroy(myEntity.gameObject);
        }
    }
}