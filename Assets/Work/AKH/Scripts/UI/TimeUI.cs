using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimeUI : MonoBehaviour
{
    public NotifyValue<int> min;
    public NotifyValue<int> hour;
    [field: SerializeField] public float changeMinVal { get; private set; } = 0;
    public float curTime { get; private set; } = 0;
    private TextMeshProUGUI _timeTxt;

    private void Awake()
    {
        _timeTxt = GetComponent<TextMeshProUGUI>();
        min.OnvalueChanged += HandleMinChange;
        hour.OnvalueChanged += HandleHourChange;
        hour.Value = 8;
    }
    private void HandleHourChange(int prev, int next)
    {
        if (next == 24)
        {
            GameManager.instance.DayCnt.Value++;
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
        _timeTxt.text = (hour.Value < 10 ? "0" : "") + $"{hour.Value}:{next}" + (next == 0 ? "0" : "");
    }
    private void Update()
    {
        Debug.Log(curTime);
        curTime += Time.deltaTime;
        if (curTime >= changeMinVal)
        {
            min.Value += 10;
            curTime = 0;
        }
    }
}
