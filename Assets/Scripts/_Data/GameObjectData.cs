using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "UMI/ObjectData", fileName = "ObjectData")]
public class GameObjectData : ScriptableObject
{
    public HealthInfo health;
}

[System.Serializable]
public class HealthInfo
{
    public int maxHealth;
    public int currentHealth;
}