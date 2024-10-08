using UnityEngine;
using UnityEngine.InputSystem;

public class Player : Agent
{
    public Hungry hungryCompo;
    public Water waterCompo;
    private PlayerStateMachine StateMachine;
    private DamageCaster _damageCasterCompo;

    public PlayerInput playerInput;

    [SerializeField] private float _speed;
    [SerializeField] private int _damage;
    protected override void Awake()
    {
        base.Awake();
        _damageCasterCompo = GetComponentInChildren<DamageCaster>();
        hungryCompo = GetComponent<Hungry>();
        hungryCompo.Initialize(this);

        waterCompo = GetComponent<Water>();
        waterCompo.Initialize(this);

        playerInput = new();
        playerInput.Enable();

        playerInput.Input.Axe.performed += ChangeAxeState;

        StateMachine = new PlayerStateMachine();

        StateMachine.AddState(PlayerEnum.Idle, new PlayerIdleState(this, StateMachine, "Idle"));
        StateMachine.AddState(PlayerEnum.Run, new PlayerRunState(this, StateMachine, "Run"));
        StateMachine.AddState(PlayerEnum.Axe, new PlayerAxeState(this, StateMachine, "Axe"));
        StateMachine.AddState(PlayerEnum.Dead, new PlayerDeadState(this, StateMachine, "Dead"));

        StateMachine.Initialize(PlayerEnum.Idle, this);
    }
    public void SetDeadState()
    {
        StateMachine.ChangeState(PlayerEnum.Dead);
    }   
    public void SetDead()
    {
        isStop = true;
        isDead = true;
    }
    private void ChangeAxeState(InputAction.CallbackContext context)
    {
        if (GameManager.instance.isUI || GameManager.instance.isInteractionUI) return;
        Vector2 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (mouse.x > transform.position.x)
            HandleSpriteFlip(Vector2.one);
        else
            HandleSpriteFlip(-Vector2.one);
        StateMachine.ChangeState(PlayerEnum.Axe);
    }

    private void Update()
    {
        Move();
        StateMachine.CurrentState.UpdateState();
    }

    private void Move()
    {
        if (GameManager.instance.isInteractionUI)
        {
            StateMachine.ChangeState(PlayerEnum.Idle);
            return;
        }
        Vector2 input = playerInput.Input.Move.ReadValue<Vector2>();
        movementCompo.SetMovement(input * _speed);
        HandleSpriteFlip(input);
    }

    public override void HandleSpriteFlip(Vector2 dir)
    {
        if (isStop) return;
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
    public override void Attack()
    {
        _damageCasterCompo.CastDamage(_damage);
    }
}
