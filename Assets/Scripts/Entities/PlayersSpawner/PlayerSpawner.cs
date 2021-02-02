using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : Entity
{
    public Transform spawnPointsParent;
    List<Transform> playerSpawnPoints = new List<Transform>();
    int playersCount;

    private void OnEnable()
    {
        GameplayEventsProvider.onPlayerJoined += SetupPlayerSpawnPoint;
        GameplayEventsProvider.onPlayerDied += OnPlayerDied;
    }

    private void OnDisable()
    {
        GameplayEventsProvider.onPlayerJoined -= SetupPlayerSpawnPoint;
        GameplayEventsProvider.onPlayerDied -= OnPlayerDied;
    }

    private void Start()
    {
        SetupSpawnPoints();
    }

    private void SetupSpawnPoints()
    {
        playerSpawnPoints.Clear();
        foreach (Transform child in spawnPointsParent)
        {
            playerSpawnPoints.Add(child);
        }
    }

    public void SetupPlayerSpawnPoint(PlayerData player)
    {
        int choosedSpawn = UnityEngine.Random.Range(0, playerSpawnPoints.Count);
        player.playerSceneReference.position = playerSpawnPoints[choosedSpawn].position;
        playerSpawnPoints.RemoveAt(choosedSpawn);
        playersCount++;
        if (playersCount != 2) return;
        GameplayEventsProvider.onRoundStarted?.Invoke();
        SetupSpawnPoints();
    }

    public void OnPlayerDied(PlayerData player)
    {
        playersCount--;
    }
}