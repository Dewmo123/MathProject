using TexDrawLib;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class QuestionUI : MonoBehaviour
{
    public UnityEvent SolveSuccess;
    public UnityEvent SolveFail;
    private ProblemSO _problem;
    [SerializeField] private TEXDraw _problemTxt;
    [SerializeField] private TMP_InputField _answerTxt;

    private void OnEnable()
    {
        _problem = GameManager.instance.GetRandomProblem();
        _problemTxt.text = _problem.Question;
    }
    public void Answer()
    {
        if (_answerTxt.text == _problem.Answer)
            SolveSuccess?.Invoke();
        else
            SolveFail?.Invoke();
    }
}
