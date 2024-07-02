using UnityEngine;

public class MoveState : PlayerState
{
    private const string AxisName = "Horizontal";
    private const string JumpKey = "Jump";

    public MoveState(PlayerController player, PlayerStateMachine stateMachine) : base(player, stateMachine) { }

    public override void Enter()
    {
        Debug.Log("Enter Move State");
    }

    public override void Exit()
    {
        Debug.Log("Exit Move State");
    }

    public override void HandleInput()
    {
        _player.MoveInput = Input.GetAxis(AxisName);
        _player.JumpInput = Input.GetButtonDown(JumpKey);
        _player.DashInput = Input.GetKeyDown(KeyCode.LeftShift);
    }

    public override void LogicUpdate()
    {
        if (_player.JumpInput)
        {
            _stateMachine.ChangeState(_player.JumpState);
        }
        else if (_player.DashInput && Time.time - _player.LastDashTime > _player.DashCooldown)
        {
            _stateMachine.ChangeState(_player.DashState);
        }
        else if (_player.MoveInput == 0)
        {
            _stateMachine.ChangeState(_player.IdleState);
        }
    }

    public override void PhysicsUpdate()
    {
        _player.transform.Translate(Vector2.right * _player.MoveInput * _player.MoveSpeed * Time.fixedDeltaTime);
    }
}