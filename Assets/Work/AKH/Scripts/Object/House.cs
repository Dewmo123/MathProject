using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class House : InteractionObject
{
    protected override void HandleInteraction(InputAction.CallbackContext context)
    {
        if (context.performed && _canInteraction && TimeManager.instance.hour.Value > TimeManager.instance.canSleepTime)
        {
            TimeManager.instance.DayCnt.Value++;
            return;
        }
    }
}
