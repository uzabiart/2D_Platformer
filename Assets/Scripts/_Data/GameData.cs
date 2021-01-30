using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "UMI/GameData", fileName = "GameData")]
public class GameData : ScriptableObject
{
    public GameObjectData[] players;
    public GameObjectData[] worldObjects;
}