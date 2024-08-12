using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class MoveUI : MonoBehaviour
{
    private NotifyValue<int> moveCnt;
    private float time = 0.3f;
    private RectTransform rTransform;
    [SerializeField] private int _firstPos;
    [SerializeField] private int _secondPos;
    private void Awake()
    {
        moveCnt = new NotifyValue<int>();
        moveCnt.Value = 0;
        moveCnt.OnvalueChanged += HandleCnt;

        rTransform = GetComponent<RectTransform>();
    }
    private void Start()
    {
        GameManager.instance.Player.playerInput.Input.UI.performed += (InputAction.CallbackContext context) => { moveCnt.Value++; };
    }

    private void HandleCnt(int prev, int next)
    {
        if (next == 1)
        {
            rTransform.DOMoveX(_firstPos, time).SetEase(Ease.OutQuad);
        }
        else if (next == 2)
        {
            rTransform.DOMoveX(_secondPos, time);
            moveCnt.Value = 0;
        }
    }
}
