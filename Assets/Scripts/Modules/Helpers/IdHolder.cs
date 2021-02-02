using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdHolder : MonoBehaviour
{
    public string thisObjectId;

    private void Awake()
    {
        Player myPlayer = GetComponentInParent<Player>();
        if (myPlayer == null)
            myPlayer = GetComponent<Player>();

        if (myPlayer == null) return;
        thisObjectId = myPlayer.myPlayerData.playerId;
    }

    public string GetMyPlayerId()
    {
        return thisObjectId;
    }

    public void UpdateMyInfo(string playerId)
    {
        thisObjectId = playerId;
    }
}