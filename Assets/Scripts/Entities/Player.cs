using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MapObject
{
    public GameObjectData playerData;
    public string playerId;
    public SpriteRenderer myView;
    public PlayerInfo myPlayerInfo;

    public void UpdateMyInfo(PlayerInfo playerInfo)
    {
        playerInfo.myColor = new Color(GetRandomFloat(), GetRandomFloat(), GetRandomFloat(), 1f);
        myPlayerInfo = playerInfo;
    }

    public string GetMyPlayerId()
    {
        return playerId;
    }

    private void SetupMyColor()
    {
    }

    float GetRandomFloat()
    {
        return UnityEngine.Random.Range(0f, 1f);
    }
}