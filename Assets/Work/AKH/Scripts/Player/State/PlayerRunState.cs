using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRunState : PlayerState
{
    public PlayerRunState(Player player, PlayerStateMachine stateMachine, string animName) : base(player, stateMachine, animName)
    {
    }
    public override void UpdateState()
    {
        base.UpdateState();
        if (_player.movementCompo.vector2.magnitude <= 0)
        {
            _stateMachine.ChangeState(PlayerEnum.Idle);
        }
    }
}
