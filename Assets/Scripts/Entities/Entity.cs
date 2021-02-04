using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    protected GameContext gameContext;
    protected GameData gameData;
    public string entityId;

    private void Awake()
    {
        GenerateMyId();
        gameContext = FindObjectOfType<GameContext>();
        gameData = gameContext.gameData;
    }

    public string GetMyEntityId()
    {
        return entityId;
    }

    public void GenerateMyId()
    {
        if (entityId != "") return;
        entityId = "Entity_" + UnityEngine.Random.Range(0, 99999999);
    }
}
