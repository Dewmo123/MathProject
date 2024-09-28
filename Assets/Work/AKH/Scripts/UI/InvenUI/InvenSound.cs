using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InvenSound : MoveInven
{
    [SerializeField] private SoundSO _open;
    [SerializeField] private SoundSO _close;
    private void Start()
    {
        GameManager.instance.Player.playerInput.Input.UI.performed += HandleInput;
    }
    private void HandleInput(InputAction.CallbackContext context)
    {
        if (GameManager.instance.isInteractionUI) return;
        IncreaseCnt();
    }
    protected override void HandleCnt(int prev, int next)
    {
        if (next == 1)
        {
            SoundPlayer player = PoolManager.instance.Pop("SoundPlayer") as SoundPlayer;
            player.PlaySound(_open);
        }
        else if (next == 2)
        {
            SoundPlayer player = PoolManager.instance.Pop("SoundPlayer") as SoundPlayer;
            player.PlaySound(_close);
            moveCnt.Value = 0;
        }
    }
}
