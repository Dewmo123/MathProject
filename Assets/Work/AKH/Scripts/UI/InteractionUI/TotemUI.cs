using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TotemUI : InteractionUI
{
    private QuestionUI _question;
    private DifficultEnum _difficult;
    private Dictionary<ItemSO, int> _increasedItem = new Dictionary<ItemSO, int>();
    [SerializeField] private int _defaultRange;
    private SolvedResultUI _resultUI;
    public void Start()
    {
        StartCoroutine(WaitInteractionManager());
    }
    private IEnumerator WaitInteractionManager()
    {
        yield return null;
        _resultUI = InteractionManager.instance.InteractionUIDic[UIType.Solved] as SolvedResultUI;
    }
    public void ShowQuestion(int type)
    {
        IncreaseCnt();
        _difficult = (DifficultEnum)type;
        _question = InteractionManager.instance.InteractionUIDic[UIType.Problem] as QuestionUI;
        _question.Solved += HandleSolved;
        _question.Set(_difficult);
        _question.IncreaseCnt();
    }
    public void HandleSolved(bool val)
    {
        _question.Solved -= HandleSolved;
        string message = "";
        if (val)
        {
            switch (_difficult)
            {
                case DifficultEnum.Easy:
                    GetRandomItem(0);
                    break;
                case DifficultEnum.Medium:
                    GetRandomItem(3);
                    break;
                case DifficultEnum.Hard:
                    GetRandomItem(5);
                    break;
            }
            foreach (var a in _increasedItem)
                message += $"{a.Key.itemName} {a.Value} °³, ";
        }
        _increasedItem.Clear();

        _resultUI.IncreaseCnt();
        _resultUI.SetResultTxt(val);
        _resultUI.SetItemTxt(message);

        GameManager.instance.UseTotem();

        _question = null;
    }

    private void GetRandomItem(int range)
    {
        for (int i = 0; i < range + _defaultRange; i++)
        {
            int val = UnityEngine.Random.Range(_defaultRange, _defaultRange + range);
            ItemSO item = GameManager.instance.GetRandomItem();
            if (_increasedItem.ContainsKey(item))
                _increasedItem[item] += val;
            else
                _increasedItem.Add(item, val);
            item.cnt.Value += val;
        }
    }

    public override void AddDic()
    {
        InteractionManager.instance.InteractionUIDic.Add(MyType, this);
    }
}
