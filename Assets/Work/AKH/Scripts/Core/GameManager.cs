using SerializableDictionary.Scripts;
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
    public SerializableDictionary<DiffucultEnum, List<ProblemSO>> problemDic;
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
        foreach(var item in problems)
        {
            if (!problemDic.Dictionary[item.diffucult].Contains(item))
                problemDic.Dictionary[item.diffucult].Add(item);
        }
    }
    public ProblemSO GetRandomProblem(DiffucultEnum type)
    {
        int num = UnityEngine.Random.Range(0, problemDic.Dictionary[type].Count);
        return problemDic.Dictionary[type][num];
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
