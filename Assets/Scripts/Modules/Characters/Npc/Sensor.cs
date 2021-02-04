using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Sensor : MonoBehaviour
{
    public Action<Transform> onTargetAdded;
    public Action<Transform> onTargetLost;
    public List<Transform> targets = new List<Transform>();
    public List<string> lookForTag = new List<string>();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (lookForTag.Contains(collision.tag) && !targets.Contains(collision.transform))
        {
            targets.Add(collision.transform);
            onTargetAdded?.Invoke(collision.transform);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (lookForTag.Contains(collision.tag) && targets.Contains(collision.transform))
        {
            targets.Remove(collision.transform);
            onTargetLost?.Invoke(collision.transform);
        }
    }
}