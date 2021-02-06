using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DestroyMeAfterTime : MonoBehaviour
{
    public float time;
    public UnityEvent iGotCreated;

    void Start()
    {
        iGotCreated?.Invoke();
        Invoke(nameof(DestroyMe), time);
    }

    void DestroyMe()
    {
        Destroy(gameObject);
    }
}
