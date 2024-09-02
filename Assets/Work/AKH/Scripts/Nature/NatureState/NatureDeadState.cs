using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NatureDeadState : NatureState
{
    public NatureDeadState(NatureStateMachine stateMachine, Agent agent, string animBoolName) : base(stateMachine, agent, animBoolName)
    {
    }
}
