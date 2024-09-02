using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class Stat : MonoBehaviour
{
    public UnityEvent OnDeadEvent;
    public UnityEvent OnChangeEvent;
    public UnityEvent OnDecEvent;
    public UnityEvent OnIncEvent;

    [field:SerializeField]public float value {  get; private set; }
    [SerializeField] private int _maxValue;

    private Agent _agent;

    public void Initialize(Agent agent)
    {
        _agent = agent;
        ResetValue();
    }
    public void ResetValue()
    {
        value = _maxValue;
    }
    public void ChangeValue(float damage)
    {
        value = Mathf.Clamp(value + damage, 0, _maxValue);
        OnChangeEvent?.Invoke();
        if (damage < 0)
            OnDecEvent?.Invoke();
        else if (damage > 0)
            OnIncEvent?.Invoke();
        if (value <= 0)
            OnDeadEvent?.Invoke();
    }
    public float GetNormalizedValue()
    {
        return value / _maxValue;
    }
}
