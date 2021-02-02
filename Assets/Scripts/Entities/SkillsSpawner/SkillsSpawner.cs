using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillsSpawner : Entity
{
    public int howMuchSkills;
    public GameObject skillPickupPrefab;
    public Transform pickupsParent;
    List<SkillSpawnPoint> mySpawnPoints = new List<SkillSpawnPoint>();
    List<SkillSpawnPoint> occupiedSpawns = new List<SkillSpawnPoint>();

    private void OnEnable()
    {
        GameplayEventsProvider.onRoundStarted += RespawnSkills;
    }

    private void OnDisable()
    {
        GameplayEventsProvider.onRoundStarted += RespawnSkills;
    }

    private void Start()
    {
        ResetSpawns();
        SpawnRandomSkills();
    }

    void ResetSpawns()
    {
        mySpawnPoints.Clear();
        SkillSpawnPoint[] spawns = GetComponentsInChildren<SkillSpawnPoint>();
        foreach (SkillSpawnPoint sPoint in spawns)
        {
            mySpawnPoints.Add(sPoint);
        }
    }

    private void SpawnRandomSkills()
    {
        for (int i = 0; i < howMuchSkills; i++)
        {
            if (mySpawnPoints.Count == 0) break;
            int choosedRandomSpawn = UnityEngine.Random.Range(0, mySpawnPoints.Count);
            Transform newSkillPickup = Instantiate(skillPickupPrefab, pickupsParent).transform;
            newSkillPickup.position = mySpawnPoints[choosedRandomSpawn].transform.position;
            newSkillPickup.GetComponent<PickupSkill>().UpdateMe(gameData.availableSkills[UnityEngine.Random.Range(0, gameData.availableSkills.Length)]);
            //newSkillPickup.SetParent(pickupsParent);
            occupiedSpawns.Add(mySpawnPoints[choosedRandomSpawn]);
            mySpawnPoints.RemoveAt(choosedRandomSpawn);
        }
    }

    void RespawnSkills()
    {
        foreach (Transform child in pickupsParent)
        {
            Destroy(child.gameObject);
        }
        ResetSpawns();
        SpawnRandomSkills();
    }
}