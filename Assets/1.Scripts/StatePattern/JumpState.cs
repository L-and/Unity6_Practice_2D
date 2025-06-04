using Platformer;
using UnityEngine;

public class JumpState : PlayerState
{
    public JumpState(PlayerController player) : base(player)
    {
    }

    public override void Enter()
    {
        // 애니메이션 점프상태로 설정
        _player.anim.SetBool("isGround", false);

        // 점프키를 입력할 때 점프실행, 사운드 출력
        if (_player.inputJump)
        {
            // 사운드 출력
            SoundManager.Instance.OnJumpSound();
        
            // 점프 물리 적용
            _player.rb.AddForceY(_player.jumpForce, ForceMode2D.Impulse);
        }
        
        
        // grounded 상태 변경
        _player.isGrounded = false;
    }

    public override void Update()
    {
        // 플레이어 방향 설정
        flip();
       
        
        // Idle 상태로 전환
        if (_player.isGrounded)
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
        // 애니메이션 점프상태 해제
        _player.anim.SetBool("isGround", true);
    }
}
