using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MoveInven : MoveUI
{
    private void Start()
    {
        GameManager.instance.Player.playerInput.Input.UI.performed += (InputAction.CallbackContext context) => { moveCnt.Value++; };
    }
    

    public override void Move(int pos)
    {
        rTransform.DOMoveX(pos,time);
    }
}
