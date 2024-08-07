using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuestionUI : MonoBehaviour
{
    private ProblemSO _problem;
    [SerializeField] private TextMeshProUGUI _problemTxt;
    [SerializeField] private TextMeshProUGUI _answerTxt;
    
    private void OnEnable() 
    {
        _problem = GameManager.instance.GetRandomProblem();
        _problemTxt.text = _problem.Question;
    }
    public void Answer()
    {
        if (_answerTxt.text == _problem.Answer)
        {
            Debug.Log("good");
        }
        else
        {
            Debug.Log("Bad");
        }
    }
}
