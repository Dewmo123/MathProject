using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Cinemachine;

public class ManageElectric : MonoBehaviour
{
    [SerializeField] private int _percent;
    [SerializeField] private float _activeTime;
    [SerializeField] private float _effectTime;
    [SerializeField] private float _power;
    [SerializeField] private SoundSO _thunder;

    private WaitForSeconds _activeDelay;
    private WaitForSeconds _effectDelay;
    private Image _fadeImage;
    private CinemachineImpulseSource _source;

    private void Awake()
    {
        _activeDelay = new WaitForSeconds(_activeTime);
        _effectDelay = new WaitForSeconds(_effectTime);
        _source = GetComponent<CinemachineImpulseSource>();
    }
    private void Start()
    {
        _fadeImage = GetComponentInChildren<Image>();
        _fadeImage.gameObject.SetActive(false);
        TimeManager.instance.min.OnvalueChanged += CheckElectric;
    }

    private void CheckElectric(int prev, int next)
    {
        if (GameManager.instance.CurWeather.Value.weatherEnum != WeatherEnum.Thunderstorm) return;
        int num = UnityEngine.Random.Range(1, 101);
        if (num < _percent)
        {
            StartCoroutine(Electric());
        }
    }

    private IEnumerator Electric()
    {
        _fadeImage.gameObject.SetActive(true);
        yield return _activeDelay;
        _fadeImage.gameObject.SetActive(false);
        yield return _effectDelay;
        var player = PoolManager.instance.Pop("SoundPlayer") as SoundPlayer;
        player.PlaySound(_thunder);
        _source.GenerateImpulse(_power);
    }
}
