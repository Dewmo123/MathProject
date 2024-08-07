using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    [SerializeField] private List<ProblemSO> problems;
    public ProblemSO GetRandomProblem()
    {
        int num = Random.Range(0, problems.Count);
        return problems[num];
    }
}
