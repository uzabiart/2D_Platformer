using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using DG.Tweening;

public class CameraManager : Entity
{
    public int minimumCameraDistance;
    public int minimalPlayerRangeToZoomOut;
    public float rateOfZoom;

    public Camera mainCamera;
    public Transform cameraT;

    Vector3 playerPositions;
    float screenRatio;

    private void Start()
    {
        screenRatio = (float)Screen.height / (float)Screen.width;
        mainCamera.orthographicSize = minimumCameraDistance;
    }

    private void Update()
    {
        CenterCameraOnPlayers();
        if (gameData.players.Count > 1)
            ManageCameraDistanceView();
        else
            mainCamera.DOOrthoSize(minimumCameraDistance, 2f);
    }

    void CenterCameraOnPlayers()
    {
        if (gameData.players.Count == 0) return;
        playerPositions = Vector2.zero;
        foreach (PlayerData player in gameData.players)
        {
            playerPositions += player.playerSceneReference.position;
        }
        playerPositions = new Vector2(playerPositions.x / gameData.players.Count, playerPositions.y / gameData.players.Count);
        cameraT.DOMove(playerPositions, 1.3f);
    }

    void ManageCameraDistanceView()
    {
        float distance = 0;
        float longestDistance = 0;
        for (int i = 0; i < gameData.players.Count; i++)
        {
            if (i != 0)
                distance = Vector3.Distance(new Vector3(gameData.players[i - 1].playerSceneReference.position.x * screenRatio, gameData.players[i - 1].playerSceneReference.position.y, 0), new Vector3(gameData.players[i].playerSceneReference.position.x * screenRatio, gameData.players[i].playerSceneReference.position.y, 0));
            else
                distance = Vector3.Distance(new Vector3(gameData.players[i].playerSceneReference.position.x * screenRatio, gameData.players[i].playerSceneReference.position.y, 0), new Vector3(gameData.players[gameData.players.Count - 1].playerSceneReference.position.x * screenRatio, gameData.players[gameData.players.Count - 1].playerSceneReference.position.y, 0));

            if (distance > longestDistance)
                longestDistance = distance;
        }
        if (longestDistance > minimalPlayerRangeToZoomOut)
            mainCamera.DOOrthoSize((minimumCameraDistance + ((longestDistance - minimalPlayerRangeToZoomOut) / rateOfZoom)), 2f);

    }
}