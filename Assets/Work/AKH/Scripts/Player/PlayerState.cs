using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerEnum
{
    Idle,
    Axe,
    Hammer,
    Dead,
    Run
}
public class PlayerState
{
    protected Player _player;
    protected PlayerStateMachine _stateMachine;

    protected int _animBoolHash;
    protected bool _endTriggerCalled;

    public PlayerState(Player player, PlayerStateMachine stateMachine, string animName)
    {
        _player = player;
        _stateMachine = stateMachine;
        _animBoolHash = Animator.StringToHash(animName);
    }
    public virtual void UpdateState()
    {

    }
    public virtual void Enter()
    {
        _player.animatorCompo.SetBool(_animBoolHash, true);
        _endTriggerCalled = false;
    }
    public virtual void Exit()
    {
        _player.animatorCompo.SetBool(_animBoolHash,false);
    }
    public virtual void AnimationEndTrigger()
    {
        _endTriggerCalled = true;
    }
}
