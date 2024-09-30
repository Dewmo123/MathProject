using DG.Tweening;
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
        float cWid = Screen.width;
        rTransform.DOMoveX(pos*((float)cWid/1920), time);
    }
}
