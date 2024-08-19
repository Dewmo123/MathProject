using SerializableDictionary.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherManager : MonoSingleton<WeatherManager>
{
    public List<WeatherSO> weathers;
    public Queue<WeatherSO> weatherQueue;
    private void Awake()
    {
        weatherQueue = new Queue<WeatherSO>();
        for(int i = 1; i <= 57; i++)
        {
            WeatherSO weather = weathers[Random.Range(0, weathers.Count)];
            weatherQueue.Enqueue(weather);
        }
    }
}
