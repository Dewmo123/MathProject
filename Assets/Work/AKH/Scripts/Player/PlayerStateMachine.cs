using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : MonoBehaviour
{
    public PlayerState CurrentState { get; private set; }
    public Dictionary<PlayerEnum, PlayerState> stateDictionary = new Dictionary<PlayerEnum, PlayerState>();
    public Player player;

    public void Initialize(PlayerEnum playerEnum, Player player)
    {
        this.player = player;
        CurrentState = stateDictionary[playerEnum];
        CurrentState.Enter();
    }
    public void ChangeState(PlayerEnum newState)
    {
        CurrentState.Exit();
        CurrentState = stateDictionary[newState];
        CurrentState.Enter();
    }
    public void AddState(PlayerEnum newState, PlayerState state)
    {
        stateDictionary.Add(newState, state);
    }
}
