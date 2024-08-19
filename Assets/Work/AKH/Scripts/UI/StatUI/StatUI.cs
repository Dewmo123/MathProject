using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Experimental.GraphView.GraphView;

public abstract class StatUI : PlayerConnectUI
{
    private Image _barImage;
    private Image _backBarImage;
    protected Stat _playerStat;

    [SerializeField] private string _barName;
    [SerializeField] private float _waitingTime;
    private float _lastHitTime;
    private bool _ischaseFill;
    public override void AfterFindPlayer()
    {
        _barImage = transform.Find(_barName+"Bar").GetComponent<Image>();
        _backBarImage = transform.Find("BackBar").GetComponent<Image>();

        _playerStat.OnChangeEvent.AddListener(HandleHitEvnet);
    }

    private void HandleHitEvnet()
    {
        _barImage.fillAmount = _playerStat.GetNormalizedValue();
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
