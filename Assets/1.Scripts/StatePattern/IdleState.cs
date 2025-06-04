using UnityEngine;

public class IdleState : PlayerState
{
    public IdleState(PlayerController player) : base(player)
    {
    }

    public override void Enter()
    {
    }

    public override void Update()
    {
        // 점프상태로 전환
        if (_player.inputJump) 
        {
            _player.playerStateMachine.TransitionTo(_player.playerStateMachine.jumpState);
            return;
        }
    
        // 이동상태로 전환
        if (_player.inputH != 0f)
        {
            _player.playerStateMachine.TransitionTo(_player.playerStateMachine.moveState);
            return;
        }
    }

    public override void FixedUpdate()
    {
    }

    public override void Exit()
    {
    }
}