using UnityEngine;

public abstract class PlayerState : IState
{
    protected PlayerController _player;
    
    // 플레이어 방향 스케일값
    private static Vector3 _rightScale = new Vector3(1,1,1);
    private static Vector3 _leftScale = new Vector3(-1,1,1);
    public PlayerState(PlayerController player)
    {
        _player = player;
    }

    public abstract void Enter();
    public abstract void Update();
    public abstract void FixedUpdate();
    public abstract void Exit();

    /// <summary>
    /// Transform.Scale.x 로 플립 설정
    /// </summary>
    protected void flip()
    {
        if (_player.inputH != 0f)
        {
            _player.transform.localScale = _player.inputH > 0 ? _rightScale : _leftScale;
        }
    }
    
    /// <summary>
    /// 입력값에 따른 움직임 업데이트
    /// </summary>
    protected void move()
    {
        _player.rb.linearVelocity = new Vector2(_player.inputH * _player.speed, _player.rb.linearVelocity.y);
    }
}