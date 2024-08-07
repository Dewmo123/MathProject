using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    private PlayerStateMachine StateMachine;

    public PlayerInput playerInput;

    [SerializeField]private float _speed;
    protected override void Awake()
    {
        base.Awake();
        playerInput = new();
        playerInput.Enable();
        StateMachine = new PlayerStateMachine();

        StateMachine.AddState(PlayerEnum.Idle, new PlayerIdleState(this, StateMachine, "Idle"));
        StateMachine.AddState(PlayerEnum.Run, new PlayerRunState(this, StateMachine, "Run"));

        StateMachine.Initialize(PlayerEnum.Idle,this);
    }
    private void Update()
    {
        Vector2 vector2 = playerInput.Input.Move.ReadValue<Vector2>();
        movementCompo.SetMovement(vector2*_speed);
        HandleSpriteFlip(vector2);
        StateMachine.CurrentState.UpdateState();
    }
    public override void HandleSpriteFlip(Vector2 vector2)
    {
        bool isRight = IsFacingRight();
        if (vector2.x < 0 && isRight)
        {
            transform.eulerAngles = new Vector3(0, -180f, 0);
        }
        else if (vector2.x > 0 && !isRight)
        {
            transform.eulerAngles = Vector3.zero;
        }
    }
}
