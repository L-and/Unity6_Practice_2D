using System;
using UnityEngine;

[Serializable]
public class StateMachine
{
    public IState CurrentState { get; private set; }
    
    public IdleState idleState;
    public MoveState moveState;
    public JumpState jumpState;

    public StateMachine(PlayerController player)
    {
        idleState = new IdleState(player);
        moveState = new MoveState(player);
        jumpState = new JumpState(player);
    }
    
    public void Initialize(IState startingState)
    {
        CurrentState = startingState;
        startingState.Enter();
    }

    public void TransitionTo(IState nextState)
    {
        Debug.Log($"[State 변경] {CurrentState.GetType()} > {nextState.GetType()}");
        CurrentState.Exit();
        CurrentState = nextState;
        nextState.Enter();
    }

    public void Update()
    {
        if (CurrentState != null)
        {
            CurrentState.Update();
        }
    }

    public void FixedUpdate()
    {
        if (CurrentState != null)
        {
            CurrentState.FixedUpdate();
        }
    }
}