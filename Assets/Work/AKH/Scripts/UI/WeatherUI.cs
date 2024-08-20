using SerializableDictionary.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeatherUI : PlayerConnectUI
{
    public NotifyValue<WeatherSO> curWeather { get; private set; }

    [SerializeField] private Image _weatherImage;
    [SerializeField] private TextMeshProUGUI _weatherTxt;
    private void Awake()
    {
        curWeather = new NotifyValue<WeatherSO>();
        curWeather.OnvalueChanged += HandleWeatherChanged;
    }
    protected override void Start()
    {
        base.Start();
        curWeather.Value = WeatherManager.instance.GetNextWeather();
    }
    private void HandleWeatherChanged(WeatherSO prev, WeatherSO next)
    {
        _weatherImage.sprite = next.sprite;
        _weatherTxt.text = next.weatherName;
    }

    public override void AfterFindPlayer()
    {
    }
}
