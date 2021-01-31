using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputController : Module
{
    private Vector2 movementInput;
    public Movement movementModule;

    public Action onSkillUseQ;
    public Action onSkillUltiUsed;

    void Update()
    {
        movementModule.Move(new Vector3(movementInput.x, movementInput.y, 0));
        if (movementInput == Vector2.zero)
            movementModule.StopMove();
    }

    public void OnMove(InputAction.CallbackContext ctx)
    {
        movementInput = ctx.ReadValue<Vector2>();
    }

    public void BasicSkill()
    {
        onSkillUseQ?.Invoke();
    }

    public void UltiSkill()
    {
        onSkillUltiUsed?.Invoke();
    }
}
