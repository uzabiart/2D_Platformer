using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MapObject
{
    public GameObjectData playerData;
    public string playerId;
    public SpriteRenderer myView;
    public Color myColor;
    public Color p1;
    public Color p2;

    private void Start()
    {
        playerId = "Player#" + UnityEngine.Random.Range(0, 999999).ToString();
        SetupMyColor();
    }

    public string GetMyPlayerId()
    {
        return playerId;
    }

    private void SetupMyColor()
    {
        Player[] playersInScene = FindObjectsOfType<Player>();

        //myColor = new Color(GetRandomFloat(), GetRandomFloat(), GetRandomFloat(), 1f);

        if (playersInScene.Length == 1)
            myColor = p1;
        else
            myColor = p2;

        myView.color = myColor;
    }

    float GetRandomFloat()
    {
        return UnityEngine.Random.Range(0f, 1f);
    }
}