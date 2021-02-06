using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Entity : MonoBehaviour
{
    protected GameContext gameContext;
    protected GameData gameData;
    public string entityId;

    public UnityEvent onCreation;

    private void Awake()
    {
        GenerateMyId();
        gameContext = FindObjectOfType<GameContext>();
        gameData = gameContext.gameData;
        onCreation?.Invoke();
    }

    public string GetMyEntityId()
    {
        return entityId;
    }

    public void GenerateMyId()
    {
        Entity myEntity = null;
        if (transform.parent != null)
            myEntity = transform.parent.GetComponentInParent<Entity>();
        if (myEntity != null)
            entityId = myEntity.GetMyEntityId();
        if (entityId != "") return;
        entityId = "Entity_" + UnityEngine.Random.Range(0, 99999999);
    }
}
