using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillsSpawner : Entity
{
    public int howMuchSkills;
    public GameObject skillPickupPrefab;
    List<SkillSpawnPoint> mySpawnPoints = new List<SkillSpawnPoint>();
    List<SkillSpawnPoint> occupiedSpawns = new List<SkillSpawnPoint>();

    private void Start()
    {
        SkillSpawnPoint[] spawns = GetComponentsInChildren<SkillSpawnPoint>();
        foreach (SkillSpawnPoint sPoint in spawns)
        {
            mySpawnPoints.Add(sPoint);
        }
        SpawnRandomSkills();
    }

    private void SpawnRandomSkills()
    {
        for (int i = 0; i < howMuchSkills; i++)
        {
            if (mySpawnPoints.Count == 0) break;
            int choosedRandomSpawn = UnityEngine.Random.Range(0, mySpawnPoints.Count);
            Transform newSkillPickup = Instantiate(skillPickupPrefab).transform;
            newSkillPickup.localPosition = mySpawnPoints[choosedRandomSpawn].transform.position;
            newSkillPickup.GetComponent<PickupSkill>().UpdateMe(gameData.availableSkills[UnityEngine.Random.Range(0, gameData.availableSkills.Length)]);
            occupiedSpawns.Add(mySpawnPoints[choosedRandomSpawn]);
            mySpawnPoints.RemoveAt(choosedRandomSpawn);
        }
    }
}