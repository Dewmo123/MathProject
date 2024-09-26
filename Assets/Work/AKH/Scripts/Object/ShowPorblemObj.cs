using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShowPorblemObj : InteractionObject
{
    [SerializeField] private ItemSO _rewardItem;
    [SerializeField] private int _minCnt, _maxCnt;
    private QuestionUI _question;
    protected override void HandleInteraction(InputAction.CallbackContext context)
    {
        if (_canInteraction && InteractionManager.instance.InteractionUIDic[_type].MoveCnt == 0 && !GameManager.instance.isInteractionUI)
        {
            _question = InteractionManager.instance.InteractionUIDic[_type] as QuestionUI;
            _question.IncreaseCnt();
            if (_question == null) throw new System.Exception("Type is not problem");
            _question.Set((DifficultEnum)Random.Range(0, 3));
            _question.Solved += HandleSolved;
        }
    }

    private void HandleSolved(bool val)
    {
        _question.Solved -= HandleSolved;
        if (val)
        {
            int num = Random.Range(_minCnt, _maxCnt);
            _rewardItem.cnt.Value += num;
            SystemTxtUI item = PoolManager.instance.Pop("SystemText") as SystemTxtUI;
            item.GetComponent<TextMeshProUGUI>().text = $"{_rewardItem.itemName} + {num}";
            item.gameObject.SetActive(true);
        }
    }
}
