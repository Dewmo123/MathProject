using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class House : InteractionObject
{
    protected override void HandleInteraction(InputAction.CallbackContext context)
    {
        if(_canInteraction&&TimeManager.instance.hour.Value >= TimeManager.instance.canSleepTime)
        {
            TimeManager.instance.DayCnt.Value++;
        }
        else if (_canInteraction)
        {
            var text = PoolManager.instance.Pop("SystemText") as SystemTxtUI;
            text.GetComponent<TextMeshProUGUI>().text = "18시 이전엔 잘 수 없습니다.";
            text.gameObject.SetActive(true);
        }
    }
}
