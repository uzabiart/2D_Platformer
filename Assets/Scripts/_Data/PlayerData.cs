using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "UMI/Player Data", fileName = "Player")]
public class PlayerData : ScriptableObject
{
    public string playerId;
    public int playerLifes;
    public PlayerScoreInfo playerScore;
    public Color myColor;
    public Transform playerSceneReference;
    public Player playerLogic;
}