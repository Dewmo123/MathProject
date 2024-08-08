using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    private NotifyValue<int> invenCnt;
    private float time = 0.3f;
    private RectTransform rTransform;

    private void Awake()
    {
        invenCnt = new NotifyValue<int>();
        invenCnt.Value = 0;
        invenCnt.OnvalueChanged += HandleCnt;

        rTransform = GetComponent<RectTransform>();
    }
    private void Start()
    {
        GameManager.instance.Player.playerInput.Input.UI.performed += (InputAction.CallbackContext context) => { invenCnt.Value++; };
    }

    private void HandleCnt(int prev, int next)
    {
        if (next == 1)
        {
            rTransform.DOMoveX(450, time).SetEase(Ease.OutQuad);
        }
        else if (next == 2)
        {
            rTransform.DOMoveX(-800, time);
            invenCnt.Value = 0;
        }
    }
}
