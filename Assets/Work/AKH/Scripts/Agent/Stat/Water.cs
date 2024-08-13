using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Water : MonoBehaviour
{
    public UnityEvent OnHitEvent;
    public UnityEvent OnChangeEvent;
    public UnityEvent OnRestoreEvent;
    public UnityEvent OnDeadEvent;

    public int water { get; private set; }
    [SerializeField] private int _maxWater;

    private Agent _agent;
    public void Initialize(Agent agent)
    {
        _agent = agent;
        ResetWater();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            ChangeWater(-10);
        }
    }
    public void ResetWater()
    {
        water = _maxWater;
    }
    public void ChangeWater(int damage)
    {
        water = Mathf.Clamp(water + damage, 0, _maxWater);
        OnChangeEvent?.Invoke();
        if (damage < 0)
            OnHitEvent?.Invoke();
        else if (damage > 0)
            OnRestoreEvent?.Invoke();
        if (water <= 0)
            OnDeadEvent?.Invoke();
    }
    public float GetNormalizedWater()
    {
        return water / (float)_maxWater;
    }
}
