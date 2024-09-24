using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    private ParticlePoolItem _item;
    private void Start()
    {
        GameManager.instance.CurWeather.OnvalueChanged += HandleWeatherChanged;
    }

    private void HandleWeatherChanged(WeatherSO prev, WeatherSO next)
    {
        
        if (prev != null)
        {
            _item.gameObject.SetActive(false);
           PoolManager.instance.Push(_item);
        }
        _item = PoolManager.instance.Pop(next.name) as ParticlePoolItem;
        _item.gameObject.SetActive(true);
    }
}
