using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdHolder : MonoBehaviour
{
    string thisObjectId;

    private void Awake()
    {
        Player myPlayer = GetComponentInParent<Player>();
        if (myPlayer == null)
            myPlayer = GetComponent<Player>();

        if (myPlayer == null) return;
        thisObjectId = myPlayer.myPlayerInfo.playerId;
    }

    public string GetMyPlayerId()
    {
        return thisObjectId;
    }
}