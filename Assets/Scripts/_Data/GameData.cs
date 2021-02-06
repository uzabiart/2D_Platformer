using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(menuName = "UMI/GameData", fileName = "GameData")]
public class GameData : ScriptableObject
{
    public GameObjectData[] worldObjects;
    public PlayerData[] availablePlayers;
    public List<PlayerData> players = new List<PlayerData>();
    public SkillData[] availableSkills;
    public PlayerData deadPlayer;

    private void OnDisable()
    {
        players.Clear();
        deadPlayer = null;
    }

    private void OnEnable()
    {
        players.Clear();
        deadPlayer = null;
    }

    public void AddPlayer(PlayerInput player)
    {
        PlayerData newPlayer = null;
        Entity playerEntity = player.GetComponentInParent<Entity>();
        if (deadPlayer != null)
        {
            newPlayer = deadPlayer;
            playerEntity.entityId = deadPlayer.playerId;
            newPlayer.playerSceneReference = player.GetComponentInParent<Entity>().transform;
            newPlayer.playerLogic = player.GetComponentInParent<Player>();
        }
        else
        {
            playerEntity.GenerateMyId();
            newPlayer = availablePlayers[players.Count];
            newPlayer.playerScore.ClearMe(newPlayer.playerScore);
            newPlayer.playerLifes = 3;
            newPlayer.playerSceneReference = playerEntity.transform;
            newPlayer.playerId = playerEntity.GetMyEntityId();
            newPlayer.playerLogic = player.GetComponentInParent<Player>();
        }
        players.Add(newPlayer);

        newPlayer.playerLogic.UpdateMyInfo(newPlayer);
        GameplayEventsProvider.onPlayerJoined?.Invoke(newPlayer);
    }

    public PlayerData GetMyOpponentInfo(string playerId)
    {
        foreach (PlayerData player in players)
        {
            if (player.playerId != playerId)
                return player;
        }
        return null;
    }

    public PlayerData GetMyPlayerInfo(string playerId)
    {
        foreach (PlayerData player in players)
        {
            if (player.playerId == playerId)
                return player;
        }
        return null;
    }

    public void PlayerDead(PlayerData player)
    {
        foreach (PlayerData p in players)
        {
            if (p == player)
            {
                deadPlayer = player;
                p.playerScore.deaths++;
            }
            else
            {
                p.playerScore.kills++;
            }
        }
        players.Remove(player);
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
    public int gold;

    public void ClearMe(PlayerScoreInfo scoreInfo)
    {
        scoreInfo.kills = 0;
        scoreInfo.deaths = 0;
        scoreInfo.level = 1;
        scoreInfo.gold = 0;
    }
}