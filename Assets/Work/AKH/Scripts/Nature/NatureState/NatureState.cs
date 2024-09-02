using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NatureState
{
    protected NatureStateMachine _stateMachine;
    protected Agent _agent;
    protected int _animBoolHash;
    protected bool _endTriggerCalled;
    public NatureState(NatureStateMachine stateMachine, Agent agent, string animBoolName)
    {
        _stateMachine = stateMachine;
        _agent = agent;
        _animBoolHash = Animator.StringToHash(animBoolName);
    }
    public virtual void EndTriggerCalled()
    {
        _endTriggerCalled = true;
    }
    public virtual void Enter()
    {
        _agent.animatorCompo.SetBool(_animBoolHash, true);
        _endTriggerCalled = false;
    }
    public virtual void Exit()
    {
        _agent.animatorCompo.SetBool(_animBoolHash, false);
    }
    public virtual void UpdateState()
    {

    }
}
