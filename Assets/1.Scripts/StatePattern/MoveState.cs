using UnityEngine;

public class MoveState : PlayerState 
{
    public MoveState(PlayerController player) : base(player)
    {
    }

    public override void Enter()
    {
        // 애니메이션 변경
        _player.anim.SetBool("isRun", true);
    }

    public override void Update()
    {
        // 플레이어 방향 설정
        flip();

        // 점프상태로 전환
        if (_player.inputJump) 
        {
            _player.playerStateMachine.TransitionTo(_player.playerStateMachine.jumpState);
            return;
        }
        
        // Idle 상태로 전환
        if (_player.inputH == 0f)
        {
            _player.playerStateMachine.TransitionTo(_player.playerStateMachine.idleState);
            return;
        }
        
        
    }

    public override void FixedUpdate()
    {   
        move();
    }

    public override void Exit()
    {
        _player.anim.SetBool("isRun", false);
    }
    
    
}
