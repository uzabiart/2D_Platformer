using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "UMI/Effect Data", fileName = "EffectData")]
public class EffectData : ScriptableObject
{
    public string effectName;
    [TextArea]
    public string effectDesription;
    public GameObject effectPrefab;
}