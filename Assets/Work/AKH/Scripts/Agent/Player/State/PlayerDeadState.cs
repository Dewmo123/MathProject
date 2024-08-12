using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeadState : PlayerState
{
    public PlayerDeadState(Player player, PlayerStateMachine stateMachine, string animName) : base(player, stateMachine, animName)
    {
    }
    public override void Enter()
    {
        base.Enter();
        _player.SetDead();
    }
    public override void UpdateState()
    {
        base.UpdateState();
        _player.movementCompo.StopImmediately();
        if (_endTriggerCalled)
        {
            Debug.Log("asd");
            GameObject.Destroy(_player.gameObject);
        }
    }
}
