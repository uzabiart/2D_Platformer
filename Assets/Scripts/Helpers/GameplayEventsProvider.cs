using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayEventsProvider : MonoBehaviour
{
    public static Action<PlayerData> onPlayerDied;
    public static Action<PlayerData> onPlayerJoined;
    public static Action onRoundFinished;
    public static Action onRoundStarted;
}
