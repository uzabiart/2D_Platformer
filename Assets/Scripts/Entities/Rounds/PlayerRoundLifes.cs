using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerRoundLifes : MonoBehaviour
{
    public GameObject[] playerHealths;

    public void UpdateMyView(int playerLifes)
    {
        foreach (GameObject health in playerHealths)
        {
            health.SetActive(false);
        }
        for (int i = 0; i < playerLifes; i++)
        {
            playerHealths[i].SetActive(true);
        }
    }

    public void UpdateMyScore()
    {
    }
}