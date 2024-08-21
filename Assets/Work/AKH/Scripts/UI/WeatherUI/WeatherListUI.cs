using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherListUI : MonoBehaviour
{
    [SerializeField]private List<WeatherUI> weatherList;
    private void Start()
    {
        SetUI();
        WeatherManager.instance.DayChangeEvent += SetUI;
    }
    
    public void SetUI()
    {
        foreach(var UI in weatherList)
        {
            UI.curWeather.Value = WeatherManager.instance.GetNextWeather();
        }
    }
}