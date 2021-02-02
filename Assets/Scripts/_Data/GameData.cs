using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(menuName = "UMI/GameData", fileName = "GameData")]
public class GameData : ScriptableObject
{
    public GameObjectData[] worldObjects;
    public List<PlayerInfo> players = new List<PlayerInfo>();
    public SkillData[] availableSkills;

    private void OnDisable()
    {
        players.Clear();
    }

    private void OnEnable()
    {
        players.Clear();
    }

    public void AddPlayer(PlayerInput player)
    {
        PlayerInfo newPlayer = new PlayerInfo()
        {
            playerId = "Player#" + UnityEngine.Random.Range(0, 999999).ToString(),
            playerSceneReference = player.GetComponentInParent<Entity>().transform,
            playerLogic = player.GetComponentInParent<Player>(),
        };
        players.Add(newPlayer);
        GameplayEventsProvider.onPlayerJoined?.Invoke(newPlayer);
        newPlayer.playerLogic.UpdateMyInfo(newPlayer);
    }

    public PlayerInfo GetMyOpponentInfo(string playerId)
    {
        foreach (PlayerInfo player in players)
        {
            if (player.playerId != playerId)
                return player;
        }
        return null;
    }

    public PlayerInfo GetMyPlayerInfo(string playerId)
    {
        foreach (PlayerInfo player in players)
        {
            if (player.playerId == playerId)
                return player;
        }
        return null;
    }

    public void PlayerDead(PlayerInfo player)
    {
        foreach (PlayerInfo p in players)
        {
            if (p == player)
            {
                players.Remove(p);
                GameplayEventsProvider.onPlayerDied?.Invoke(player);
                break;
            }
        }
        CheckIfRoundFinished();
    }

    private void CheckIfRoundFinished()
    {
        if (players.Count <= 1)
        {
            GameplayEventsProvider.onRoundFinished?.Invoke();
        }
    }
}

[System.Serializable]
public class PlayerInfo
{
    public string playerId;
    public Color myColor;
    public Transform playerSceneReference;
    public Player playerLogic;
}