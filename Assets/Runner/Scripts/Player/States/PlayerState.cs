using UnityEngine;

public abstract class PlayerState
{
    protected Player Player;
    protected PlayerStateMachine StateMachine;

    public PlayerState(Player player, PlayerStateMachine stateMachine)
    {
        Player = player; StateMachine = stateMachine;
    }

    public abstract void Enter();
    public abstract void HandleInput(EInputCommand inputCommand);
    public abstract void Exit();
}
