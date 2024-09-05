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
    public bool isInteractionUI { get; private set; } = false;
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

    [field: SerializeField] public List<ItemSO> items { get; private set; }


    private void Awake()
    {
        if (instance == null)
            instance = this;
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
    public void SetInteractionUI(bool value)
    {
        isInteractionUI = value;
    }
}
