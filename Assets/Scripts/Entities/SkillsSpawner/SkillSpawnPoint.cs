using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillSpawnPoint : Module
{
    public SpriteRenderer mySprite;

    private void Start()
    {
        mySprite.enabled = false;
    }
}