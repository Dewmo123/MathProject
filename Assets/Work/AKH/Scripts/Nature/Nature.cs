using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nature : Agent
{
    private NatureStateMachine _stateMachine;
    protected override void Awake()
    {
        base.Awake();
        _stateMachine = new NatureStateMachine();
        _stateMachine.Add(NatureType.Idle, new NatureIdleState(_stateMachine, this, "Idle"));
        _stateMachine.Add(NatureType.Hit, new NatureHitState(_stateMachine, this, "Hit"));
        _stateMachine.Add(NatureType.Dead, new NatureDeadState(_stateMachine, this, "Dead"));
        _stateMachine.Init(NatureType.Idle);
    }
    private void Start()
    {
        TimeManager.instance.DayCnt.OnvalueChanged += Revive;
    }

    private void Revive(int prev, int next)
    {
        healthCompo.ResetValue();
        SetDead(false);
        _stateMachine.ChangeState(NatureType.Idle);
    }

    private void Update()
    {
        _stateMachine.currentState.UpdateState();
        if (Input.GetKeyDown(KeyCode.P)) healthCompo.ChangeValue(-10);
    }
    public void ChangeHitState()
    {
        if (isDead) return;
        _stateMachine.ChangeState(NatureType.Hit);
    }
    public void ChangeDeadState()
    {
        _stateMachine.ChangeState(NatureType.Dead);
        SetDead(true);
    }
    public override void EndTriggerCall()
    {
        _stateMachine.currentState.EndTriggerCalled();
    }
}
