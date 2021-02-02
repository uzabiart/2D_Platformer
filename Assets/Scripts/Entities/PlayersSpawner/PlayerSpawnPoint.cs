using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawnPoint : Module
{
    public SpriteRenderer mySprite;

    private void Start()
    {
        mySprite.enabled = false;
    }
}
