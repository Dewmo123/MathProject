using UnityEngine;
using UnityEngine.InputSystem;

public class Player : Character
{
    private PlayerStateMachine StateMachine;

    public PlayerInput playerInput;

    [SerializeField] private float _speed;
    protected override void Awake()
    {
        base.Awake();
        playerInput = new();
        playerInput.Enable();
        playerInput.Input.Axe.performed += ChangeAxeState;
        StateMachine = new PlayerStateMachine();

        StateMachine.AddState(PlayerEnum.Idle, new PlayerIdleState(this, StateMachine, "Idle"));
        StateMachine.AddState(PlayerEnum.Run, new PlayerRunState(this, StateMachine, "Run"));
        StateMachine.AddState(PlayerEnum.Axe, new PlayerAxeState(this, StateMachine, "Axe"));

        StateMachine.Initialize(PlayerEnum.Idle, this);
    }

    private void ChangeAxeState(InputAction.CallbackContext context)
    {
        Vector2 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        HandleSpriteFlip(mouse);
        StateMachine.ChangeState(PlayerEnum.Axe);
    }

    private void Update()
    {
        Vector2 input = playerInput.Input.Move.ReadValue<Vector2>();
        movementCompo.SetMovement(input * _speed);
        HandleSpriteFlip(input);
        StateMachine.CurrentState.UpdateState();
    }
    public override void HandleSpriteFlip(Vector2 dir)
    {
        if(isStop) return;
        bool isRight = IsFacingRight();
        if (dir.x < 0 && isRight)
        {
            transform.eulerAngles = new Vector3(0, -180f, 0);
        }
        else if (dir.x > 0 && !isRight)
        {
            transform.eulerAngles = Vector3.zero;
        }
    }
    public void OnDestroy()
    {
        playerInput.Input.Axe.performed -= ChangeAxeState;
    }

    public override void EndTriggerCall()
    {
        StateMachine.CurrentState.AnimationEndTrigger();
    }
}
