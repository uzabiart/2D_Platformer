using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class FetchBall : MonoBehaviour
{
    public Collider2D myCollider;

    void Start()
    {
        Invoke(nameof(EnableMyCollider), 0.1f);
    }

    private void EnableMyCollider()
    {
        myCollider.enabled = true;
    }
}
