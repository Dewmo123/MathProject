using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeMood : MonoBehaviour
{
    private Image _moodPenel;

    [Tooltip("숫자는 10분에 1틱이며, 몇개의 틱이 지나야 이 색에 도달할 지를 정합니다.\n틱은 그 전 값이 끝났을 때 처음부터 틱을 셉니다.")]
    [SerializeField] private List<int> _ticks;
    [SerializeField] private List<Color> _colors;

    private Color _prevColor;
    private int _currentTicks = 0;
    private int _nowCount = 0;

    private int _currentTick;

    private void Awake()
    {
        _moodPenel = transform.GetComponent<Image>();
        _moodPenel.color = _colors[0];
        _prevColor = _moodPenel.color;
    }

    private void Start()
    {
        TimeManager.instance.min.OnvalueChanged += HandleTickChange;
        TimeManager.instance.DayCnt.OnvalueChanged += HandleDayChangeSet;
    }

    public void HandleDayChangeSet(int prev, int next)
    {
        _moodPenel.color = _colors[0];
        _prevColor = _colors[0];
        _currentTicks = 0;
        _currentTick = 0;
        _nowCount = 0;
    }

    public void HandleTickChange(int prev, int next)
    {
        if (--_currentTick <= 0)
        {
            _prevColor = _colors[_nowCount++];
            _currentTicks = _ticks[_nowCount];
            _currentTick = _ticks[_nowCount];
        }

        Color color = _moodPenel.color;

        color.r += (_colors[_nowCount].r - _prevColor.r) / _currentTicks;
        color.g += (_colors[_nowCount].g - _prevColor.g) / _currentTicks;
        color.b += (_colors[_nowCount].b - _prevColor.b) / _currentTicks;
        color.a += (_colors[_nowCount].a - _prevColor.a) / _currentTicks;

        _moodPenel.color = color;
    }
}
