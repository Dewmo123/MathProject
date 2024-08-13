using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Hungry : MonoBehaviour
{
    public UnityEvent OnDeadEvent;
    public UnityEvent OnHitEvent;
    public UnityEvent OnChangeEvent;
    public UnityEvent OnRestoreEvent;

    public int hungry { get; private set; }
    [SerializeField] private int _maxHungry;

    private Agent _agent;
    public void Initialize(Agent agent)
    {
        _agent = agent;
        ResetHungry();
    }
    public void ResetHungry()
    {
        hungry = _maxHungry;
    }
    public void ChangeHungry(int damage)
    {
        hungry += damage;
        OnChangeEvent.Invoke();
        if (damage < 0)
            OnHitEvent?.Invoke();
        else if(damage > 0) 
            OnRestoreEvent?.Invoke();
        if (hungry <= 0)
            OnDeadEvent?.Invoke();
    }
    public float GetNormalizedHungry()
    {
        return hungry / (float)_maxHungry;
    }
}
