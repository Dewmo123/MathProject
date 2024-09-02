using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NatureIdleState : NatureState
{
    public NatureIdleState(NatureStateMachine stateMachine, Agent agent, string animBoolHash) : base(stateMachine, agent, animBoolHash)
    {
    }
}
