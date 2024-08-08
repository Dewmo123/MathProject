using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAxeState : PlayerState
{
    public PlayerAxeState(Player player, PlayerStateMachine stateMachine, string animName) : base(player, stateMachine, animName)
    {
    }
    public override void Enter()
    {
        base.Enter();
        _player.isStop = true;
    }
    public override void UpdateState()
    {
        base.UpdateState();
        _player.movementCompo.StopImmediately();
        if (_player.playerInput.Input.Axe.IsPressed()&&_endTriggerCalled)
        {
            _stateMachine.ChangeState(PlayerEnum.Axe);
        }
        if (_endTriggerCalled)
        {
            _stateMachine.ChangeState(PlayerEnum.Idle);
        }
    }
    public override void Exit()
    {
        base.Exit();
        _player.isStop = false;
    }
}
