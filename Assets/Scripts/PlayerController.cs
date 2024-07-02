using UnityEngine;

// I tried referencing from this video: https://www.youtube.com/watch?v=OjreMoAG9Ec
// but I didn't like a lot of what he was doing there so I changed a some stuff
public class PlayerController : MonoBehaviour
{
    #region Player stats
    [SerializeField, Min(0)] float _moveSpeed = 5f;
    [SerializeField, Min(0)] float _jumpForce = 6f;
    [SerializeField, Min(0)] float _dashForce = 6f;
    [SerializeField, Min(0)] float _dashDuration = 0.2f;
    [SerializeField, Min(0)] float _dashCooldown = 1f;
    #endregion

    #region states
    public MoveState MoveState { get; private set; }
    public JumpState JumpState { get; private set; }
    public DashState DashState { get; private set; }
    public IdleState IdleState { get; private set; }
    public FallState FallState { get; private set; }
    #endregion

    private float _moveInput;
    private bool _jumpInput;
    private bool _dashInput;

    const string MOVE = "Horizontal";
    const string JUMP = "Jump";
    const string DASH = "Shift";

    public PlayerStateMachine StateMachine { get; private set; }
    public float MoveSpeed { get => _moveSpeed; private set => _moveSpeed = value; }
    public float JumpForce { get => _jumpForce; private set => _jumpForce = value; }
    public float DashForce { get => _dashForce; private set => _dashForce = value; }
    public float DashDuration { get => _dashDuration; private set => _dashDuration = value; }
    public float DashCooldown { get => _dashCooldown; private set => _dashCooldown = value; }
    public Rigidbody2D Rigidbody { get; private set; }
    public bool IsDashing { get; set; }
    public float LastDashTime { get; set; }

    public float MoveInput { get => _moveInput; set => _moveInput = value; }
    public bool JumpInput { get => _jumpInput; set => _jumpInput = value; }
    public bool DashInput { get => _dashInput; set => _dashInput = value; }

    private void Awake()
    {
        Rigidbody = GetComponent<Rigidbody2D>();

        StateMachine = new PlayerStateMachine();

        MoveState = new MoveState(this, StateMachine);
        JumpState = new JumpState(this, StateMachine);
        DashState = new DashState(this, StateMachine);
        IdleState = new IdleState(this, StateMachine);
        FallState = new FallState(this, StateMachine);
    }

    private void Start()
    {
        StateMachine.Initialize(IdleState);
    }

    private void Update()
    {
        MoveInput = Input.GetAxis(MOVE);
        JumpInput = Input.GetKeyDown(KeyCode.Space);
        DashInput = Input.GetKeyDown(KeyCode.LeftShift);

        StateMachine.CurrentState.HandleInput();
        StateMachine.CurrentState.LogicUpdate();
    }

    private void FixedUpdate()
    {
        StateMachine.CurrentState.PhysicsUpdate();
    }
}


public class FallState : PlayerState
{
    public FallState(PlayerController player, PlayerStateMachine stateMachine) : base(player, stateMachine) { }

    public override void LogicUpdate()
    {
        if (_player.Rigidbody.velocity.y == 0)
        {
            _stateMachine.ChangeState(_player.IdleState);
        }
    }
    public override void PhysicsUpdate()
    {
        _player.transform.Translate(Vector2.right * _player.MoveInput * _player.MoveSpeed * Time.fixedDeltaTime);
    }
}
