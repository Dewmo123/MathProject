using System;
using TexDrawLib;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class QuestionUI : InteractionUI
{
    public event Action<bool> Solved;
    private ProblemSO _problem;
    [SerializeField] private Image _problemImage;
    [SerializeField] private TMP_InputField _answerTxt;


    public void Start()
    {
        Solved += HandleSolved;
    }

    private void HandleSolved(bool value)
    {
        IncreaseCnt();
    }

    public void Set(DifficultEnum type)
    {
        _problem = GameManager.instance.GetRandomProblem(type);
        SetProblem();
    }
    private void SetProblem()
    {
        _problemImage.rectTransform.sizeDelta = new Vector2(_problem.Width, _problem.Height);
        _problemImage.sprite = _problem.Question;
    }

    public void Answer()
    {
        if (_answerTxt.text == _problem.Answer)
            Solved?.Invoke(true);
        else
            Solved?.Invoke(false);
        _answerTxt.DeactivateInputField();
        _answerTxt.text = "";
    }
    public override void AddDic()
    {
        InteractionManager.instance.InteractionUIDic.Add(MyType, this);
    }
}
