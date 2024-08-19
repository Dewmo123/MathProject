using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaterUI : PlayerConnectUI
{
    private Image _barImage;
    private Image _backBarImage;
    private Water _playerWater;

    [SerializeField] private float _waitingTime;
    private float _lastHitTime;
    private bool _ischaseFill;
    public override void AfterFindPlayer()
    {
        _barImage = transform.Find("WaterBar").GetComponent<Image>();
        _backBarImage = transform.Find("BackBar").GetComponent<Image>();
        _playerWater = _player.waterCompo;

        _playerWater.OnChangeEvent.AddListener(HandleHitEvnet);
    }

    private void HandleHitEvnet()
    {
        _barImage.fillAmount = _playerWater.GetNormalizedValue();
        _lastHitTime = Time.time;
    }
    private void Update()
    {
        if (_player == null) return;

        if (!_ischaseFill && _lastHitTime + _waitingTime > Time.time)
        {
            _ischaseFill = true;
            _backBarImage.DOFillAmount(_barImage.fillAmount, 0.5f)
                .SetEase(Ease.InCubic)
                .OnComplete(() => _ischaseFill = false);
        }
    }
}
