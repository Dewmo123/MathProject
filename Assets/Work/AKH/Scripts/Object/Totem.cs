using UnityEngine;
using UnityEngine.InputSystem;

public class Totem : InteractionObject
{
    protected override void HandleInteraction(InputAction.CallbackContext context)
    {
        if (!GameManager.instance.isTotem&& InteractionManager.instance.InteractionUIDic[_type].MoveCnt == 0 && !GameManager.instance.isInteractionUI)
            base.HandleInteraction(context);
        else if(_canInteraction)
            ShowSystemText();
    }

    private void ShowSystemText()
    {
        SystemTxtUI system = PoolManager.instance.Pop("SystemText") as SystemTxtUI;
        system.gameObject.SetActive(true);
    }
}
