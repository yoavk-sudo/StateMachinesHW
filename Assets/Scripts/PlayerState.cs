using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public abstract class PlayerState : MonoBehaviour
{
    protected PlayerController _player;
    protected PlayerStateMachine _stateMachine;

    protected PlayerState(PlayerController player, PlayerStateMachine stateMachine)
    {
        _player = player;
        _stateMachine = stateMachine;
    }

    public virtual void Enter() { }
    public virtual void Exit() { }
    public virtual void HandleInput() { }
    public virtual void LogicUpdate() { }
    public virtual void PhysicsUpdate() { }
}


