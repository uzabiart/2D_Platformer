using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : Module
{
    public int speed;
    public Animator animator;

    public void StopMove()
    {
        if (animator.enabled)
        {
            animator.Play("Ponka_Stoi", -1, 0.0f);
        }
    }

    public void Move(Vector3 input)
    {
        if (input.x < 0)
        {
            animator.transform.localScale = new Vector2(-0.7f, 0.7f);
        }
        else
        {
            animator.transform.localScale = new Vector2(0.7f, 0.7f);
        }
        //animator.Play("Ponka_Run", 0, 0.0f);
        myEntity.transform.Translate(input * Time.deltaTime * speed);
    }
}