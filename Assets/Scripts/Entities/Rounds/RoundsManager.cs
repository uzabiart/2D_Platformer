using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundsManager : MonoBehaviour
{
    public List<PlayerRound> players = new List<PlayerRound>();

    public GameObject roundWinnerPanel;
    public GameObject gameWinnerPanel;

    private void OnEnable()
    {
        GameplayEventsProvider.onPlayerJoined += SetupPlayer;
        GameplayEventsProvider.onPlayerDied += RemovePlayer;
    }

    private void OnDisable()
    {
        GameplayEventsProvider.onPlayerJoined -= SetupPlayer;
        GameplayEventsProvider.onPlayerDied -= RemovePlayer;
    }

    private void SetupPlayer(PlayerInfo player)
    {
        foreach (PlayerRound p in players)
        {
            if (p.playerInfo.playerId == "")
            {
                p.playerInfo.playerId = player.playerId;
                p.playerInfo.playerLifes = player.playerLifes;
                p.roundLifes.UpdateMyView(player.playerLifes);
                break;
            }
        }
    }

    void RemovePlayer(PlayerInfo player)
    {
        foreach (PlayerRound p in players)
        {
            if (p.playerInfo.playerId == player.playerId)
            {
                p.playerInfo.playerId = "";
                p.playerInfo.playerLifes = player.playerLifes;
                p.roundLifes.UpdateMyView(player.playerLifes);
                break;
            }
        }
        ShowRoundWinner(player);
    }

    void ShowRoundWinner(PlayerInfo info)
    {
        if (info.playerLifes == 0)
            gameWinnerPanel.SetActive(true);
        else
            roundWinnerPanel.SetActive(true);

        Invoke(nameof(HidePanels), 5f);
    }

    private void HidePanels()
    {
        roundWinnerPanel.SetActive(false);
    }
}

[System.Serializable]
public class PlayerRound
{
    public PlayerInfo playerInfo;
    public PlayerRoundLifes roundLifes;
}