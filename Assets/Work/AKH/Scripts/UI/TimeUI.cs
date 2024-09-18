using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class TimeUI : MoveUI
{
    private void Start()
    {
        GameManager.instance.Player.playerInput.Input.UI.performed += (InputAction.CallbackContext context) => { moveCnt.Value++; };
    }


    public override void Move(int pos)
    {
        rTransform.DOMoveX(pos, time);
    }
}
