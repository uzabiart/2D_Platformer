using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreUIManagerv2 : MonoBehaviour
{
    public List<PlayerRoundScore> players = new List<PlayerRoundScore>();

    public GameObject roundWinnerPanel;
    public GameObject gameWinnerPanel;

    private void OnEnable()
    {
        GameplayEventsProvider.onPlayerJoined += SetupPlayer;
        GameplayEventsProvider.onPlayerDied += UpdateScores;
    }

    private void OnDisable()
    {
        GameplayEventsProvider.onPlayerJoined -= SetupPlayer;
        GameplayEventsProvider.onPlayerDied -= UpdateScores;
    }

    private void SetupPlayer(PlayerInfo player)
    {
        foreach (PlayerRoundScore p in players)
        {
            if (p.playerInfo.playerId == "")
            {
                p.playerInfo.playerId = player.playerId;
                p.roundLifes.UpdateMyView(player.playerLifes);
                break;
            }
        }
    }

    void UpdateScores(PlayerInfo player)
    {
        foreach (PlayerRoundScore p in players)
        {
            if (p.playerInfo.playerId == player.playerId)
            {
                p.playerInfo.playerId = "";
                p.playerInfo.playerLifes = player.playerLifes;
                p.roundLifes.UpdateMyView(player.playerLifes);
                break;
            }
            else
            {

            }
        }
    }
}

[System.Serializable]
public class PlayerRoundScore
{
    public PlayerInfo playerInfo;
    public PlayerRoundLifes roundLifes;
}