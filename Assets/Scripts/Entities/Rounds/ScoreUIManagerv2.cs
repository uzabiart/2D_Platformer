using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreUIManagerv2 : MonoBehaviour
{
    //public List<PlayerRoundScore> players = new List<PlayerRoundScore>();
    public PlayerRoundLifes[] scorePanels;
    //public GameObject roundWinnerPanel;
    //public GameObject gameWinnerPanel;

    private void OnEnable()
    {
        ResetScore();
        GameplayEventsProvider.onPlayerJoined += SetupPlayer;
        GameplayEventsProvider.onPlayerDied += OnPlayerDed;
    }

    private void OnDisable()
    {
        GameplayEventsProvider.onPlayerJoined -= SetupPlayer;
        GameplayEventsProvider.onPlayerDied -= OnPlayerDed;
    }

    private void ResetScore()
    {
        foreach (PlayerRoundLifes p in scorePanels)
        {
            p.ResetScore();
        }
    }

    private void SetupPlayer(PlayerData player)
    {
        foreach (PlayerRoundLifes p in scorePanels)
        {
            if (p.CheckAndUpdatePlayerData(player)) break;
        }
    }

    void OnPlayerDed(PlayerData player)
    {
        foreach (PlayerRoundLifes p in scorePanels)
        {
            p.UpdateMyScore();
        }
    }
}