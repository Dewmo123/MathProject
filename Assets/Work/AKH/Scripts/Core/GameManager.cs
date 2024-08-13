using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    [SerializeField] private List<ProblemSO> problems;
    [field:SerializeField]public Player Player { get; private set; }
    public List<ItemSO> items;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            items[Random.Range(0, items.Count)].cnt.Value++;
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            items[Random.Range(0, items.Count)].cnt.Value=0;
        }
    }
    public ProblemSO GetRandomProblem()
    {
        int num = Random.Range(0, problems.Count);
        return problems[num];
    }
}
