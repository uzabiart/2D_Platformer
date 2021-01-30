using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Module : MonoBehaviour
{
    protected Entity myEntity;

    private void Awake()
    {
        myEntity = GetComponentInParent<Entity>();
    }
}