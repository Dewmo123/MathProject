using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherListUI : MonoBehaviour
{
    [SerializeField]private List<WeatherUI> weatherList;
    private void Start()
    {
        SetUI(0,0);
        GameManager.instance.DayCnt.OnvalueChanged += SetUI;
    }
    
    public void SetUI(int prev, int next)
    {
        foreach(var UI in weatherList)
        {
            UI.curWeather.Value = WeatherManager.instance.GetNextWeather();
        }
    }
}