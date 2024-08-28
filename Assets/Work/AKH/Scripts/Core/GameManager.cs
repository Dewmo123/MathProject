using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    [SerializeField] private List<ProblemSO> problems;
    public bool isUI { get; private set; } = false;

    [field: SerializeField] public Player Player { get; private set; }

    public NotifyValue<int> DayCnt;

    [field: SerializeField] public List<ItemSO> items { get; private set; }


    private void Awake()
    {
        DayCnt.Value = 1;
    }



    public ProblemSO GetRandomProblem()
    {
        int num = UnityEngine.Random.Range(0, problems.Count);
        return problems[num];
    }
    public void SetUI(bool value)
    {
        isUI = value;
    }

}
