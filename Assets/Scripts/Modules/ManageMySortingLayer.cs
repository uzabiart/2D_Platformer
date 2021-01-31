using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageMySortingLayer : MonoBehaviour
{
    public SpriteRenderer mySprite;
    public int orderMod;

    void Update()
    {
        mySprite.sortingOrder = (int)(transform.position.y * -100) + orderMod;
    }
}
