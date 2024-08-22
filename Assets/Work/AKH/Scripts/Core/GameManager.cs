using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    [SerializeField] private List<ProblemSO> problems;

    public bool isUI { get; private set; } = false;

    [field:SerializeField]public Player Player { get; private set; }

    public NotifyValue<int> DayCnt;
    private void Awake()
    {
        DayCnt = new NotifyValue<int>();
        DayCnt.Value = 1;
    }
    public List<ItemSO> items;
    public ProblemSO GetRandomProblem()
    {
        int num = UnityEngine.Random.Range(0, problems.Count);
        return problems[num];
    }
    public void SetUI(bool value)
    {
        isUI = value;
    }
    #if UNITY_EDITOR
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
#endif
}
