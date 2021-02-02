﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(menuName = "UMI/GameData", fileName = "GameData")]
public class GameData : ScriptableObject
{
    public GameObjectData[] worldObjects;
    public List<PlayerInfo> players = new List<PlayerInfo>();
    public List<PlayerInfo> savedPlayers = new List<PlayerInfo>();
    public SkillData[] availableSkills;
    public PlayerInfo deadPlayer;

    private void OnDisable()
    {
        players.Clear();
        deadPlayer.playerId = "";
    }

    private void OnEnable()
    {
        players.Clear();
        deadPlayer.playerId = "";
    }

    public void AddPlayer(PlayerInput player)
    {
        PlayerInfo newPlayer = new PlayerInfo()
        {
            playerId = "Player#" + UnityEngine.Random.Range(0, 999999).ToString(),
            playerLifes = 3,
            playerSceneReference = player.GetComponentInParent<Entity>().transform,
            playerLogic = player.GetComponentInParent<Player>(),
        };

        if (deadPlayer.playerId != "")
        {
            newPlayer.playerId = deadPlayer.playerId;
            newPlayer.playerLifes = deadPlayer.playerLifes;
            newPlayer.playerScore = deadPlayer.playerScore;
        }

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
                deadPlayer = player;
                p.playerScore.deaths++;
                players.Remove(p);
            }
            else
            {
                p.playerScore.kills++;
            }
        }
        GameplayEventsProvider.onPlayerDied?.Invoke(player);
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
    public int playerLifes;
    public PlayerScoreInfo playerScore;
    public Color myColor;
    public Transform playerSceneReference;
    public Player playerLogic;
}

[System.Serializable]
public class PlayerScoreInfo
{
    public int kills;
    public int deaths;
    public int level;

    public void ClearMe(PlayerScoreInfo scoreInfo)
    {
        scoreInfo.kills = 0;
        scoreInfo.deaths = 0;
        scoreInfo.level = 1;
    }
}