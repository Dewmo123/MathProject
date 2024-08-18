using SerializableDictionary.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherManager : MonoBehaviour
{
    private Player player;
    public SerializableDictionary<WeatherEnum, WeatherSO> weathers;
    private float _currentTime;
    [SerializeField] private float _hitTime;
    private void Awake()
    {
        player = GameManager.instance.Player;
    }
    private void Update()
    {
        if (_currentTime >= _hitTime)
        {
            player.hungryCompo.ChangeValue(weathers.Dictionary[WeatherEnum.Sunny].hungryPerSec);
            _currentTime = 0;
        }
        _currentTime += Time.deltaTime;
    }
}
