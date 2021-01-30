using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    protected GameContext gameContext;
    protected GameData gameData;

    private void Awake()
    {
        gameContext = FindObjectOfType<GameContext>();
        gameData = gameContext.gameData;
    }
}
