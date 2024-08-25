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
    private Player _player;
    public Player Player
    {
        get
        {
            if (_player == null)
                _player = FindObjectOfType<Player>();
            if (_player == null)
                Debug.LogWarning("There is no player in scene, but still try access it");
            return _player;
        }
    }
    public NotifyValue<int> DayCnt;
    public bool isTimeStop { get; private set; } = false;

    [field: SerializeField] public List<ItemSO> items { get; private set; }


    private void Awake()
    {
        if (instance == null)
            instance = this;
        DayCnt.Value = 1;
    }
    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            SetTimeStop(true);
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            SetTimeStop(false);
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
    public void SetTimeStop(bool value)
    {
        isTimeStop = value;
    }
}
