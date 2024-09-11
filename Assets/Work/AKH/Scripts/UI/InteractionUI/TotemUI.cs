using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TotemUI : InteractionUI
{
    private QuestionUI _question;
    public void ShowQuestion(int type)
    {
        IncreaseCnt();
        _question = InteractionManager.instance.InteractionUIDic[UIType.Problem] as QuestionUI;
        _question.Solved += HandleSolved;
        _question.Set((DiffucultEnum)type);
        _question.IncreaseCnt();
    }
    public void HandleSolved(bool val)
    {
        _question.Solved -= HandleSolved;
        _question = null;
    }
    public override void AddDic()
    {
        InteractionManager.instance.InteractionUIDic.Add(MyType, this);
    }
}
