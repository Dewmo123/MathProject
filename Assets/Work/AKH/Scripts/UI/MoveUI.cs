using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public abstract class MoveUI : MonoBehaviour
{
    protected NotifyValue<int> moveCnt;
    public int MoveCnt => moveCnt.Value;

    protected float time = 0.3f;
    protected RectTransform rTransform;

    [SerializeField] protected int _firstPos;
    [SerializeField] protected int _secondPos;
    protected virtual void Awake()
    {
        moveCnt = new NotifyValue<int>();
        moveCnt.Value = 0;
        moveCnt.OnvalueChanged += HandleCnt;

        rTransform = GetComponent<RectTransform>();
    }
    protected virtual void HandleCnt(int prev, int next)
    {
        if (next == 1)
        {
            Move(_firstPos);
            GameManager.instance.SetUI(true);
        }
        else if (next == 2)
        {
            Move(_secondPos);
            moveCnt.Value = 0;
            GameManager.instance.SetUI(false);
        }
    }
    public abstract void Move(int pos);
    private void OnDestroy()
    {
        moveCnt.OnvalueChanged -= HandleCnt;
    }
    public void IncreaseCnt()
    {
        moveCnt.Value++;
    }
}
