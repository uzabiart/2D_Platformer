using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentManager : MonoBehaviour
{
    public GameObject[] pyrtki;
    public GameObject[] pyrtki2;
    public GameObject[] trees;

    public Transform trash;

    private void Start()
    {
        RandomizePyrtki(pyrtki, 50);
        RandomizePyrtki(pyrtki2, 50);
        RandomizePyrtki(trees, 5);
    }

    private void RandomizePyrtki(GameObject[] ktorepyrtki, int howMuch)
    {
        for (int i = 0; i < howMuch; i++)
        {
            int randomPyrtek = UnityEngine.Random.Range(0, ktorepyrtki.Length);
            Transform newPyrtek = Instantiate(ktorepyrtki[randomPyrtek], trash).transform;
            float randomXPos = UnityEngine.Random.Range(-18, 18);
            float randomYPos = UnityEngine.Random.Range(-18, 18);
            newPyrtek.position = new Vector2(randomXPos, randomYPos);
            float randomScale = UnityEngine.Random.Range(newPyrtek.localScale.x * 0.3f, newPyrtek.localScale.y * 0.9f);
            newPyrtek.localScale = Vector2.one * randomScale;
        }
    }
}