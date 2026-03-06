using UnityEngine;

public class PlayerIdleState : PlayerState
{
    public PlayerIdleState(Player player, PlayerStateMachine stateMachine) : base(player, stateMachine) { }

    public override void Enter()
    {
        Debug.Log("Enter IdleState");
        Player.ColliderHolder.RunCollider.enabled = false;
        Player.ColliderHolder.RunCollider.enabled = false;
        Player.ColliderHolder.RunCollider.enabled = false;
        Player.PlayerAnimatorController.StartIdle();
    }

    public override void HandleInput(EInputCommand inputCommand)
    {

    }
    public override void Exit()
    {
        Debug.Log("Exit IdleState");
    }
}
