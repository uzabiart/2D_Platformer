using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AnimationEventSender : MonoBehaviour
{
    public UnityEvent event1;
    public UnityEvent event2;
    public UnityEvent event3;

    public void SendEvent1()
    {
        event1?.Invoke();
    }
}