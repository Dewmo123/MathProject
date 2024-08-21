using SerializableDictionary.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class WeatherManager : MonoSingleton<WeatherManager>
{
    public event Action DayChangeEvent;

    private Player _player;

    [SerializeField] private float _hitTime;
    [SerializeField] private WeatherUI _coreWeather;

    private WeatherSO _curWeather;

    [field: SerializeField] public List<WeatherSO> weathers { get; private set; }
    public List<WeatherSO> curWeathers;

    private float _currentTime;
    private int cnt = -1;
    public NotifyValue<int> DayCnt;
    private void Awake()
    {
        DayCnt = new NotifyValue<int>();
        DayCnt.Value = 1;
        DayCnt.OnvalueChanged += HandleDayChanged;
        SetWeathers();
        _coreWeather.curWeather.Value = curWeathers[DayCnt.Value++];
        StartCoroutine(FindPlayer());
    }

    private void HandleDayChanged(int prev, int next)
    {
        cnt = DayCnt.Value - 2;
        DayChangeEvent?.Invoke();
    }

    private IEnumerator FindPlayer()
    {
        yield return new WaitUntil(() => GameManager.instance.Player != null);
        _player = GameManager.instance.Player;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            _coreWeather.curWeather.Value = curWeathers[DayCnt.Value++];
        }
        if (_currentTime >= _hitTime && _player != null)
        {
            _curWeather = _coreWeather.curWeather.Value;
            _currentTime = 0;
            _player.healthCompo.ChangeValue(_curWeather.healthPerSec);
            _player.hungryCompo.ChangeValue(_curWeather.hungryPerSec);
            _player.waterCompo.ChangeValue(_curWeather.waterPerSec);
        }
        _currentTime += Time.deltaTime;
    }
    private void SetWeathers()
    {
        curWeathers = new List<WeatherSO>();
        for (int i = 1; i <= 57; i++)
        {
            WeatherSO weather = weathers[UnityEngine.Random.Range(0, weathers.Count)];
            curWeathers.Add(weather);
        }
    }
    public WeatherSO GetNextWeather()
    {
        if (cnt < curWeathers.Count)
            cnt++;
        else cnt = -1;
        return curWeathers[cnt];
    }
}
