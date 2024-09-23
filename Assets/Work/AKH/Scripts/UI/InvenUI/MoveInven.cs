using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MoveInven : MoveUI
{
    private void Start()
    {
        GameManager.instance.Player.playerInput.Input.UI.performed += HandleInput;
    }
    private void HandleInput(InputAction.CallbackContext context)
    {
        if (GameManager.instance.isInteractionUI) return;
        IncreaseCnt();
    }

    public override void Move(int pos)
    {
        rTransform.DOMoveX(pos,time);
    }
}
