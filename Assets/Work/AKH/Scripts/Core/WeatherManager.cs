using SerializableDictionary.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class WeatherManager : MonoSingleton<WeatherManager>
{
    private float _currentTime;
    private Player _player;
    [SerializeField] private float _hitTime;
    [SerializeField] private WeatherUI _coreWeather;
    private WeatherSO _curWeather;

    [field:SerializeField]public List<WeatherSO> weathers { get; private set; }
    public List<WeatherSO> curWeathers;
    private int cnt = -1;
    private void Awake()
    {
        SetWeathers();
        StartCoroutine(FindPlayer());
    }

    private IEnumerator FindPlayer()
    {
        yield return new WaitUntil(() => GameManager.instance.Player!=null);
        _player = GameManager.instance.Player;
    }

    private void Update()
    {
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
            WeatherSO weather = weathers[Random.Range(0, weathers.Count)];
            curWeathers.Add(weather);
        }
    }
    public List<WeatherSO> GetWeekWeather()
    {
        List<WeatherSO> weekWeather = new List<WeatherSO>();
        for(int i = 1; i <= 7; i++)
        {
            weekWeather.Add(weathers[i+cnt]);
        }
        return weekWeather;
    }
    public WeatherSO GetNextWeather()
    {
        if (cnt < curWeathers.Count)
            cnt++;
        else cnt = -1;
        return weathers[cnt];
    }
}
