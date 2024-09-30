using System.Collections.Generic;
using UnityEngine;

public class WeatherListUI : MonoBehaviour
{
    [SerializeField] private List<WeatherUI> weatherList;
    private int cnt = -1;
    private void Start()
    {
        SetUI(0, 0);
        TimeManager.instance.DayCnt.OnvalueChanged += SetUI;
    }

    public void SetUI(int prev, int next)
    {
        if (next != 0)
            cnt = next - 2;
        foreach (var UI in weatherList)
        {
            UI.curWeather.Value = GameManager.instance.GetWeather(++cnt);
        }
    }

    private void OnDestroy()
    {
        TimeManager.instance.DayCnt.OnvalueChanged -= SetUI;

    }
}