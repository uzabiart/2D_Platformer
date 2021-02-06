using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdHolder : Module
{
    public string thisObjectId;

    public void Start()
    {
        if (thisObjectId == "")
            thisObjectId = myEntity.GetMyEntityId();
    }

    public string GetMyEntityId()
    {
        return thisObjectId;
    }

    public void UpdateMyInfo(string newEntityId)
    {
        thisObjectId = newEntityId;
    }
}