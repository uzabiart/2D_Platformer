using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Module : MonoBehaviour
{
    protected Entity myEntity;
    protected GameContext gameContext;
    protected GameData gameData;

    public virtual void Awake()
    {
        myEntity = GetComponentInParent<Entity>();
        gameContext = FindObjectOfType<GameContext>();
        gameData = gameContext.gameData;
    }
}