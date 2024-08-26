using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;

public class WeatherManager : MonoBehaviour
{
    public static WeatherManager instance;

    private Player _player;

    [SerializeField] private float _hitTime;
    [SerializeField] private WeatherUI _coreWeatherCompo;
    [SerializeField] private TextMeshProUGUI _dayCntTxt;

    private WeatherSO _curWeather;

    [field: SerializeField] public List<WeatherSO> weathers { get; private set; }
    public List<WeatherSO> curWeathers;

    private float _currentTime;
    private int cnt = -1;

    private WaitForSeconds _waitSleep;
    [SerializeField] private float _sleepTime;

    private void Awake()
    {
        _waitSleep = new WaitForSeconds(_sleepTime);
        if (instance == null) instance = this;
        StartCoroutine(FindPlayer());
        SetWeathers();
        GameManager.instance.DayCnt.OnvalueChanged += HandleDayChange;
    }

    private void HandleDayChange(int prev, int next)
    {
        StartCoroutine(Sleep());
        cnt = next - 2;
        _coreWeatherCompo.curWeather.Value = curWeathers[GameManager.instance.DayCnt.Value-1 % 57];
        _dayCntTxt.text = $"Day : {next}";
    }

    private IEnumerator Sleep()
    {
        GameManager.instance.SetTimeStop(true);
        yield return _waitSleep;
        GameManager.instance.SetTimeStop(false);
    }

    private void Start()
    {
        _coreWeatherCompo.curWeather.Value = curWeathers[GameManager.instance.DayCnt.Value - 1];
    }
    private IEnumerator FindPlayer()
    {
        yield return new WaitUntil(() => GameManager.instance.Player != null);
        _player = GameManager.instance.Player;
    }

    private void Update()
    {
        if (!GameManager.instance.isTimeStop)
        {
            if (_currentTime >= _hitTime && _player != null)
            {
                _curWeather = _coreWeatherCompo.curWeather.Value;
                _currentTime = 0;
                _player.healthCompo.ChangeValue(_curWeather.healthPerSec);
                _player.hungryCompo.ChangeValue(_curWeather.hungryPerSec);
                _player.waterCompo.ChangeValue(_curWeather.waterPerSec);
            }
            _currentTime += Time.deltaTime;
        }
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
        cnt++;
        return curWeathers[cnt % 57];
    }
    private void OnDestroy()
    {
        GameManager.instance.DayCnt.OnvalueChanged -= HandleDayChange;
    }
}
