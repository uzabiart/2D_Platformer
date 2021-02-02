using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class NpcMoveLogic : Module
{
    public GameObject fetchBallPrefab;
    public Movement myMovement;
    Transform fetchBall;

    private void Start()
    {
        StartCoroutine(ThrowFetchBall());
    }

    private IEnumerator ThrowFetchBall()
    {
        fetchBall = Instantiate(fetchBallPrefab).transform;
        fetchBall.position = myEntity.transform.position;
        Rigidbody2D rigi = fetchBall.GetComponent<Rigidbody2D>();

        Vector3 randomPosition = new Vector3(transform.position.x + UnityEngine.Random.Range(-50f, 50f), transform.position.y + UnityEngine.Random.Range(-50f, 50f), 1f);

        Vector3 direction = transform.position - randomPosition;

        float radomStrength = UnityEngine.Random.Range(50f, 100f);

        rigi.AddForceAtPosition(direction.normalized * radomStrength * -2f, transform.position);
        yield return new WaitForSeconds(0.4f);
        MoveToBall();
    }

    private void MoveToBall()
    {
        float getDistance = Vector2.Distance(myEntity.transform.position, fetchBall.position);
        myEntity.transform.DOMove(fetchBall.position, 1.5f * getDistance).SetEase(Ease.Linear).OnComplete(() => StartCoroutine(ThrowFetchBall()));
        Destroy(fetchBall.gameObject);
    }
}