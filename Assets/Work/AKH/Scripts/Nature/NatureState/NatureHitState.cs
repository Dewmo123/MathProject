using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NatureHitState : NatureState
{
    public NatureHitState(NatureStateMachine stateMachine, Agent agent, string animBoolName) : base(stateMachine, agent, animBoolName)
    {
    }
    public override void UpdateState()
    {
        Debug.Log("EndTirgger");
        if (_endTriggerCalled)
        {
            _stateMachine.ChangeState(NatureType.Idle);
        }
    }
}
