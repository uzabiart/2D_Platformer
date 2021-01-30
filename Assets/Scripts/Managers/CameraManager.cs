using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using DG.Tweening;

public class CameraManager : MonoBehaviour
{
    public int minimumCameraDistance;
    public int minimalPlayerRangeToZoomOut;
    public float rateOfZoom;

    public Camera mainCamera;
    public Transform cameraT;

    List<Transform> players = new List<Transform>();
    Vector3 playerPositions;
    float screenRatio;

    private void Start()
    {
        screenRatio = (float)Screen.height / (float)Screen.width;
        mainCamera.orthographicSize = minimumCameraDistance;
    }

    public void OnPlayerJoin(PlayerInput player)
    {
        players.Add(player.GetComponentInParent<Entity>().transform);
    }

    private void Update()
    {
        CenterCameraOnPlayers();
        ManageCameraDistanceView();
    }

    void CenterCameraOnPlayers()
    {
        if (players.Count == 0) return;
        playerPositions = Vector2.zero;
        foreach (Transform player in players)
        {
            playerPositions += player.position;
        }
        playerPositions = new Vector2(playerPositions.x / players.Count, playerPositions.y / players.Count);
        cameraT.DOMove(playerPositions, 1.3f);
    }

    void ManageCameraDistanceView()
    {
        float distance = 0;
        float longestDistance = 0;
        for (int i = 0; i < players.Count; i++)
        {
            if (i != 0)
                distance = Vector3.Distance(new Vector3(players[i - 1].position.x * screenRatio, players[i - 1].position.y, 0), new Vector3(players[i].position.x * screenRatio, players[i].position.y, 0));
            else
                distance = Vector3.Distance(new Vector3(players[i].position.x * screenRatio, players[i].position.y, 0), new Vector3(players[players.Count - 1].position.x * screenRatio, players[players.Count - 1].position.y, 0));

            if (distance > longestDistance)
                longestDistance = distance;
        }
        if (longestDistance > minimalPlayerRangeToZoomOut)
            mainCamera.DOOrthoSize((minimumCameraDistance + ((longestDistance - minimalPlayerRangeToZoomOut) / rateOfZoom)), 2f);

    }
}