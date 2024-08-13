using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerState
{
    public PlayerIdleState(Player player, PlayerStateMachine stateMachine, string animName) : base(player, stateMachine, animName)
    {
    }
    public override void Enter()
    {
        base.Enter();
        _player.movementCompo.StopImmediately();
    }
    public override void UpdateState()
    {
        base.UpdateState();
        if (_player.movementCompo.vector2.magnitude > 0)
        {
            _stateMachine.ChangeState(PlayerEnum.Run);
        }
    }
}
