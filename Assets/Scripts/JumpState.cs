using UnityEngine;

public class JumpState : PlayerState
{
    public JumpState(PlayerController player, PlayerStateMachine stateMachine) : base(player, stateMachine) { }

    public override void Enter()
    {
        Debug.Log("Enter Jump State");
        _player.Rigidbody.AddForce(Vector2.up * _player.JumpForce, ForceMode2D.Impulse);
    }

    public override void LogicUpdate()
    {
        if (_player.Rigidbody.velocity.y <= 0)
        {
            _stateMachine.ChangeState(_player.FallState);
        }
    }

    public override void PhysicsUpdate()
    {
        _player.transform.Translate(Vector2.right * _player.MoveInput * _player.MoveSpeed * Time.fixedDeltaTime);
    }
}