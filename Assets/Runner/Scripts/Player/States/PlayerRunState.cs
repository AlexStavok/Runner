using UnityEngine;

public class PlayerRunState : PlayerState
{
    public PlayerRunState(Player player, PlayerStateMachine stateMachine) : base(player, stateMachine) { }

    public override void Enter()
    {
        Debug.Log("Enter RunState");
        Player.ColliderHolder.RunCollider.enabled = true;
        Player.PlayerAnimatorController.StartRun();
    }

    public override void HandleInput(EInputCommand inputCommand)
    {
        switch (inputCommand)
        {
            case EInputCommand.Jump:
                StateMachine.ChangeState(new PlayerJumpState(Player, StateMachine));
                break;
            case EInputCommand.Slide:
                StateMachine.ChangeState(new PlayerSlideState(Player, StateMachine));
                break;
        }
    }
    public override void Exit()
    {
        Player.ColliderHolder.RunCollider.enabled = false;
        Debug.Log("Exit RunState");
    }
}
