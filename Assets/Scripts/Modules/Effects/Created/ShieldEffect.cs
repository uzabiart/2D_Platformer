using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ShieldEffect : Effect
{
    public Image hpFill;

    public void UpdateMe(float shieldFadeOutTime)
    {
        hpFill.DOFillAmount(0f, shieldFadeOutTime).SetEase(Ease.Linear);
    }
}