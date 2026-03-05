using UnityEngine;

public class PlayerStateMachine
{
    public PlayerState CurrentState { get; private set; }

    public void Initialize(PlayerState startState)
    {
        CurrentState = startState;
        CurrentState.Enter();
    }
    public void ChangeState(PlayerState nextState)
    {
        CurrentState.Exit();
        CurrentState = nextState;
        CurrentState.Enter();
    }
}
