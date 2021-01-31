using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyMeAfterTime : MonoBehaviour
{
    public float time;

    void Start()
    {
        Invoke(nameof(DestroyMe), time);
    }

    void DestroyMe()
    {
        Destroy(gameObject);
    }
}
