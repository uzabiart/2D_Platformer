using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayEventsProvider : MonoBehaviour
{
    public static Action<PlayerInfo> onPlayerDied;
    public static Action<PlayerInfo> onPlayerJoined;
    public static Action onRoundFinished;
    public static Action onRoundStarted;
}
