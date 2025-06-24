public interface IState
{
    /// <summary>
    /// 상태에 처음 진입할 때 실행되는 코드
    /// </summary>
    public void Enter();

    /// <summary>
    /// 프레임당 로직. 새로운 상태로 전환하는 조건 포함
    /// </summary>
    public void Update();
    
    /// <summary>
    /// 물리 업데이트 코드
    /// </summary>
    public void FixedUpdate();


    /// <summary>
    /// 상태에서 벗어날 때 실행되는 코드
    /// </summary>
    public  void Exit();
    
}