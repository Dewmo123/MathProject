using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChooseWoodUI : MonoBehaviour
{
    private ItemSO _wood;
    [SerializeField] private TextMeshProUGUI _maxWoodTxt;
    [SerializeField] private TextMeshProUGUI _curWoodTxt;
    [SerializeField] private Slider _slider;
    private int _curCnt;
    private void Awake()
    {
        StartCoroutine(WaitGameManager());
    }

    private IEnumerator WaitGameManager()
    {
        yield return new WaitUntil(() => GameManager.instance != null);
        _wood = GameManager.instance.GetItemSO("³ª¹«");
        _wood.cnt.OnvalueChanged += HandleWoodChanged;
        HandleWoodChanged(0, _wood.cnt.Value);
    }

    private void HandleWoodChanged(int prev, int next)
    {
        _maxWoodTxt.text = next.ToString();
    }
    public void ChangeCurText(float val)
    {
        _curCnt = (int)Mathf.Round(_wood.cnt.Value * val);
        _curWoodTxt.text = _curCnt.ToString();
    }
    public void Fire()
    {
        _wood.cnt.Value -= _curCnt;
        TimeManager.instance.Fire(_curCnt);
        _curCnt = 0;
        _slider.value = 0;
    }
}
