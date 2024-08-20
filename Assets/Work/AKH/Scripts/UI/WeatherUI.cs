using SerializableDictionary.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeatherUI : PlayerConnectUI
{
    private NotifyValue<WeatherSO> _curWeather;
    private float _currentTime;
    [SerializeField] private float _hitTime;
    [SerializeField] private Image _weatherImage;
    [SerializeField] private TextMeshProUGUI _weatherTxt;
    private void Awake()
    {
        _curWeather = new NotifyValue<WeatherSO>();
        _curWeather.OnvalueChanged += HandleWeatherChanged;
    }
    protected override void Start()
    {
        base.Start();
        _curWeather.Value = WeatherManager.instance.weatherQueue.Dequeue();
    }
    private void HandleWeatherChanged(WeatherSO prev, WeatherSO next)
    {
        _weatherImage.sprite = next.sprite;
        _weatherTxt.text = next.weatherName;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            _curWeather.Value = WeatherManager.instance.weatherQueue.Dequeue();
        }
        if (_currentTime >= _hitTime&&_player!=null)
        {
            _currentTime = 0;
            _player.healthCompo.ChangeValue(_curWeather.Value.healthPerSec);
            _player.hungryCompo.ChangeValue(_curWeather.Value.hungryPerSec);
            _player.waterCompo.ChangeValue(_curWeather.Value.waterPerSec);
        }
        _currentTime += Time.deltaTime;
    }
    public override void AfterFindPlayer()
    {
    }
}
