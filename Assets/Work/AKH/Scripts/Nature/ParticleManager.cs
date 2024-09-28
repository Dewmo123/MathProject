using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    private ParticlePoolItem _item;
    private SoundPlayer _itemSound;
    private void Start()
    {
        GameManager.instance.CurWeather.OnvalueChanged += HandleWeatherChanged;
        HandleWeatherChanged(null, GameManager.instance.CurWeather.Value);
    }

    private void HandleWeatherChanged(WeatherSO prev, WeatherSO next)
    {

        if (_item != null)
        {
            if (_itemSound != null)
                _itemSound.StopAndGoToPool();
            _item.gameObject.SetActive(false);
            PoolManager.instance.Push(_item);
        }
        _item = PoolManager.instance.Pop(next.name) as ParticlePoolItem;
        _item.gameObject.SetActive(true);

        if (next.sound == null) return;
        _itemSound = PoolManager.instance.Pop("SoundPlayer") as SoundPlayer;
        _itemSound.PlaySound(next.sound);
    }
}