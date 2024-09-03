using System;
using TexDrawLib;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class QuestionUI : MonoBehaviour
{
    public UnityEvent SolveSuccess;
    public UnityEvent SolveFail;
    private ProblemSO _problem;
    [SerializeField] private Image _problemImage;
    [SerializeField] private TMP_InputField _answerTxt;

    private void OnEnable()
    {
        _problem = GameManager.instance.GetRandomProblem();
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
            SolveSuccess?.Invoke();
        else
            SolveFail?.Invoke();
    }
}
