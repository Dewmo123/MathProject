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

    [SerializeField] private int _fireTime;
    public int curFireTime { get; private set; }
    public bool isFire => curFireTime > 0;

    [SerializeField] private TextMeshProUGUI _dayCntTxt;
    [SerializeField] private Image _fadePanel;
    [SerializeField] private Transform _bedPos;

    private WaitForSeconds _waitSleep;
    [SerializeField] public float _sleepTime;

    public NotifyValue<int> min;
    public NotifyValue<int> hour;
    [field: SerializeField] public float changeMinVal { get; private set; } = 0;
    public float curTime { get; private set; } = 0;
    [SerializeField] private TextMeshProUGUI _timeTxt;
    [SerializeField] private TextMeshProUGUI _fireTimeTxt;

    private void Awake()
    {
        if (instance == null) instance = this;
        DayCnt = new NotifyValue<int>(1);
        _waitSleep = new WaitForSeconds(_sleepTime);
        DayCnt.OnvalueChanged += HandleDayChange;
        min.OnvalueChanged += HandleMinChange;
        hour.OnvalueChanged += HandleHourChange;
        hour.Value = 8;
    }
    private void HandleDayChange(int prev, int next)
    {
        _dayCntTxt.text = $"Day : {next}";
        min.Value = 0;
        hour.Value = 8;
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
    private void HandleHourChange(int prev, int next)
    {
        if (next == 24)
        {
            DayCnt.Value++;
            hour.Value = 8;
        }
        _timeTxt.text = (hour.Value < 10 ? "0" : "") + $"{next}:{min.Value}" + (min.Value == 0 ? "0" : "");
    }

    private void HandleMinChange(int prev, int next)
    {
        if (next == 60)
        {
            min.Value = 0;
            hour.Value++;
            next = 0;
        }
        _fireTimeTxt.text = (curFireTime / 60 < 10 ? "0" : "") + $"{curFireTime / 60}:{curFireTime % 60}" + (curFireTime % 60 == 0 ? "0" : "");
        _timeTxt.text = (hour.Value < 10 ? "0" : "") + $"{hour.Value}:{next}" + (next == 0 ? "0" : "");
    }
    private void Update()
    {
        if (!isTimeStop)
        {
            curTime += Time.deltaTime;
            if (curTime >= changeMinVal)
            {
                if (curFireTime > 0) curFireTime -= 10;
                min.Value += 10;
                curTime = 0;
            }
        }
    }
    public void SetTimeStop(bool value)
    {
        isTimeStop = value;
    }
    public void Fire(int cnt)
    {
        curFireTime = cnt * _fireTime;
    }
    private void OnDestroy()
    {
        DayCnt.OnvalueChanged -= HandleDayChange;
    }
}
