using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum NatureType
{
    Idle,
    Hit,
    Dead
}
public class NatureStateMachine
{
    private Dictionary<NatureType, NatureState> _stateDictinary = new Dictionary<NatureType, NatureState>();
    public NatureState currentState { get; private set; }
    public void Init(NatureType type)
    {
        ChangeState(type);
    }
    public void Add(NatureType type,NatureState state)
    {
        _stateDictinary.Add(type, state);
    }
    public void ChangeState(NatureType type)
    {
        if (currentState != null) currentState.Exit();
        currentState = _stateDictinary[type];
        currentState.Enter();
    }
}
