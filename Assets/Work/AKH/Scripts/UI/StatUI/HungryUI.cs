using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HungryUI : PlayerConnectUI
{
    private Image _barImage;
    private Image _backBarImage;
    private Hungry _playerHungry;

    [SerializeField] private float _waitingTime;
    private float _lastHitTime;
    private bool _ischaseFill;
    public override void AfterFindPlayer()
    {
        _barImage = transform.Find("HungryBar").GetComponent<Image>();
        _backBarImage = transform.Find("BackBar").GetComponent<Image>();
        _playerHungry = _player.hungryCompo;

        _playerHungry.OnChangeEvent.AddListener(HandleHitEvnet);
    }

    private void HandleHitEvnet()
    {
        _barImage.fillAmount = _playerHungry.GetNormalizedValue();
        _lastHitTime = Time.time;
        transform.DOShakePosition(0.3f, 1f, 100);
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
