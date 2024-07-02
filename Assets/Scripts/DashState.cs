using UnityEngine;

public class DashState : PlayerState
{
    private float _dashTime;

    public DashState(PlayerController player, PlayerStateMachine stateMachine) : base(player, stateMachine) { }

    public override void Enter()
    {
        Debug.Log("Enter Dash State");
        _dashTime = Time.time;
        _player.Rigidbody.AddForce(Vector2.right * _player.MoveInput * _player.DashForce, ForceMode2D.Impulse);
        _player.IsDashing = true;
    }

    public override void LogicUpdate()
    {
        if (Time.time - _dashTime > _player.DashDuration)
        {
            _player.IsDashing = false;
            _player.LastDashTime = Time.time;
            _stateMachine.ChangeState(_player.IdleState);
        }
    }

    public override void PhysicsUpdate()
    {
        _player.transform.Translate(Vector2.right * _player.MoveInput * _player.MoveSpeed * Time.fixedDeltaTime);
    }

    public override void Exit()
    {
        _player.IsDashing = false;
    }
}
