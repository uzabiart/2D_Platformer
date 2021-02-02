using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MapObject
{
    public GameObjectData playerData;
    public SpriteRenderer myView;
    public PlayerInfo myPlayerInfo;

    public void UpdateMyInfo(PlayerInfo playerInfo)
    {
        playerInfo.myColor = new Color(GetRandomFloat(), GetRandomFloat(), GetRandomFloat(), 1f);
        myPlayerInfo = playerInfo;
    }

    public string GetMyPlayerId()
    {
        return myPlayerInfo.playerId;
    }

    private void SetupMyColor()
    {
    }

    float GetRandomFloat()
    {
        return UnityEngine.Random.Range(0f, 1f);
    }

    public void ManagePlayerDed()
    {
        GetComponentInChildren<Movement>().ChangeCurrentSpeed(0f);
        Transform inputTransform = GetComponentInChildren<InputController>().transform;
        inputTransform.SetParent(transform);
        inputTransform.SetAsLastSibling();
        Destroy(transform.GetChild(0).gameObject);
        if (myPlayerInfo.playerLifes == 0) return;
        Invoke(nameof(DestroyPlayerAfterDelay), 5f);
    }

    void DestroyPlayerAfterDelay()
    {
        Destroy(gameObject);
    }
}