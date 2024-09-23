using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TotemUI : InteractionUI
{
    private QuestionUI _question;
    private DifficultEnum _difficult;
    private Dictionary<ItemSO, int> _increasedItem = new Dictionary<ItemSO, int>();
    [SerializeField] private int _defaultRange;
    [SerializeField] private GameObject _resultUI;
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
            _resultUI.SetActive(true);
        }
        _question = null;
    }

    private void GetRandomItem(int range)
    {
        for(int i = 0; i < range + _defaultRange; i++)
        {
            int val = UnityEngine.Random.Range(_defaultRange, _defaultRange + range);
            ItemSO item = GameManager.instance.GetRandomItem();
            _increasedItem.Add(item, val);
        }
    }

    public override void AddDic()
    {
        InteractionManager.instance.InteractionUIDic.Add(MyType, this);
    }
}
