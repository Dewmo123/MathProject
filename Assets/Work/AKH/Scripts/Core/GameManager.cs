using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField] private List<ProblemSO> problems;
    public bool isUI { get; private set; } = false;

    [field: SerializeField] public Player Player { get; private set; }

    public NotifyValue<int> DayCnt;

    [field: SerializeField] public List<ItemSO> items { get; private set; }


    private void Awake()
    {
        DayCnt.Value = 1;
        if (instance == null)
            instance = this;
    }
    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
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
