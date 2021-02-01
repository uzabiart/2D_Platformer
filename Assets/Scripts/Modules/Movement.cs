using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : Module
{
    public float speed;
    [HideInInspector]
    public float currentSpeed;
    public Animator animator;
    Vector3 movingVector;
    public Action<Vector3> onMove;

    private void Start()
    {
        ResetSpeed();
    }

    public void StopMove()
    {
        if (animator.enabled)
        {
            animator.Play("Ponka_Stoi", -1, 0.0f);
        }
    }

    public void Move(Vector3 input)
    {
        onMove?.Invoke(input);
        movingVector = input;
        if (input.x < 0)
        {
            animator.transform.localScale = new Vector2(-0.7f, 0.7f);
        }
        else
        {
            animator.transform.localScale = new Vector2(0.7f, 0.7f);
        }
        //animator.Play("Ponka_Run", 0, 0.0f);
        myEntity.transform.Translate(input * Time.deltaTime * currentSpeed);
    }

    public Vector3 GetMyMovingVector()
    {
        return movingVector;
    }

    public void ChangeSpeedForATime(float speedMod, float time)
    {
        currentSpeed *= speedMod;
        Invoke(nameof(ResetSpeed), time);
    }

    public void ChangeCurrentSpeed(float speedMod)
    {
        currentSpeed *= speedMod;
    }

    public void ResetSpeed()
    {
        currentSpeed = speed;
    }
}