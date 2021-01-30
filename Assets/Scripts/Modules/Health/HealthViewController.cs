using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthViewController : Module, IViewController
{
    public Image hpFill;

    public void UpdateMyView(object desiredObject)
    {
        HealthInfo health = (HealthInfo)desiredObject;
        hpFill.fillAmount = (float)health.currentHealth / (float)health.maxHealth;
    }
}