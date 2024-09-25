using TMPro;
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
        system.GetComponent<TextMeshProUGUI>().text = "오늘은 더이상 토템을 사용할 수 없습니다.";
        system.gameObject.SetActive(true);
    }
}
