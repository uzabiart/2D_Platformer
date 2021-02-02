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

    List<SkillData> basicSkills = new List<SkillData>();
    List<SkillData> dashSkills = new List<SkillData>();
    List<SkillData> ultiSkills = new List<SkillData>();
    List<List<SkillData>> listOfSkillsList = new List<List<SkillData>>();

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
        SetupSkillTypes();
        ResetSpawns();
        SpawnRandomSkills();
    }

    private void SetupSkillTypes()
    {
        listOfSkillsList.Clear();
        basicSkills.Clear();
        ultiSkills.Clear();
        dashSkills.Clear();
        foreach (SkillData skill in gameData.availableSkills)
        {
            switch (skill.type)
            {
                case Enums.SkillType.Basic:
                    basicSkills.Add(skill);
                    break;
                case Enums.SkillType.Ulti:
                    ultiSkills.Add(skill);
                    break;
                case Enums.SkillType.Dash:
                    dashSkills.Add(skill);
                    break;
            }
        }
        listOfSkillsList.Add(basicSkills);
        listOfSkillsList.Add(ultiSkills);
        listOfSkillsList.Add(dashSkills);
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
        int j = 0;
        for (int i = 0; i < howMuchSkills; i++)
        {
            if (mySpawnPoints.Count == 0) break;
            int choosedRandomSpawn = UnityEngine.Random.Range(0, mySpawnPoints.Count);
            Transform newSkillPickup = Instantiate(skillPickupPrefab, pickupsParent).transform;
            newSkillPickup.position = mySpawnPoints[choosedRandomSpawn].transform.position;

            if (j >= listOfSkillsList.Count)
            {
                j = 0;
            }
            newSkillPickup.GetComponent<PickupSkill>().UpdateMe(listOfSkillsList[j][UnityEngine.Random.Range(0, listOfSkillsList[j].Count)]);
            j++;
            //newSkillPickup.GetComponent<PickupSkill>().UpdateMe(gameData.availableSkills[UnityEngine.Random.Range(0, gameData.availableSkills.Length)]);

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