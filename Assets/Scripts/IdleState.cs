using UnityEngine;

public class IdleState : PlayerState
{
    private const string AxisName = "Horizontal";
    private const string JumpKey = "Jump";

    public IdleState(PlayerController player, PlayerStateMachine stateMachine) : base(player, stateMachine) { }

    public override void HandleInput()
    {
        _player.MoveInput = Input.GetAxis(AxisName);
        _player.JumpInput = Input.GetButtonDown(JumpKey);
        _player.DashInput = Input.GetKeyDown(KeyCode.LeftShift);
    }

    public override void LogicUpdate()
    {
        if (Mathf.Abs(_player.MoveInput) > 0.01f)
        {
            _stateMachine.ChangeState(_player.MoveState);
        }
        else if (_player.JumpInput)
        {
            _stateMachine.ChangeState(_player.JumpState);
        }
        else if (_player.DashInput)
        {
            _stateMachine.ChangeState(_player.DashState);
        }
    }
}

