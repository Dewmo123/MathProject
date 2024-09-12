using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour
{
    public static TimeManager instance = null;
    public bool isTimeStop { get; private set; } = false;

    public NotifyValue<int> DayCnt;

    [SerializeField] private TextMeshProUGUI _dayCntTxt;
    [SerializeField] private Image _fadePanel;
    [SerializeField] private Transform _bedPos;

    private WaitForSeconds _waitSleep;
    [SerializeField] private float _sleepTime;

    private void Awake()
    {
        if (instance == null) instance = this;
        DayCnt = new NotifyValue<int>(1);
        _waitSleep = new WaitForSeconds(_sleepTime);
        DayCnt.OnvalueChanged += HandleDayChange;
    }
    private void HandleDayChange(int prev, int next)
    {
        _dayCntTxt.text = $"Day : {next}";
        StartCoroutine(Sleep());
    }
    private IEnumerator Sleep()
    {
        SetTimeStop(true);
        _fadePanel.DOFade(1f, _sleepTime / 3);
        yield return _waitSleep;
        _fadePanel.DOFade(0f, _sleepTime / 2);
        GameManager.instance.Player.transform.position = _bedPos.position;
        SetTimeStop(false);
    }
    public void SetTimeStop(bool value)
    {
        isTimeStop = value;
    }
    private void OnDestroy()
    {
        DayCnt.OnvalueChanged -= HandleDayChange;
    }
}
