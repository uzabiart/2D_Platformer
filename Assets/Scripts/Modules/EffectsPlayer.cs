using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectsPlayer : Module
{
    public void PlayEffect(EffectData effectData)
    {
        Instantiate(effectData.effectPrefab, transform);
    }
}