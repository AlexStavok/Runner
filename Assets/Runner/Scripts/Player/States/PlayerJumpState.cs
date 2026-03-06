using UnityEngine;

public class PlayerJumpState : PlayerState
{
    public PlayerJumpState(Player player, PlayerStateMachine stateMachine) : base(player, stateMachine) { }

    public override void Enter()
    {
        Debug.Log("Enter JumpState");
        Player.ColliderHolder.JumpCollinder.enabled = true;
        Player.PlayerAnimatorController.StartJump();
    }

    public override void HandleInput(EInputCommand inputCommand)
    {
        if (inputCommand == EInputCommand.Slide)
            StateMachine.ChangeState(new PlayerRunState(Player, StateMachine));
    }
    public override void Exit()
    {

        Player.ColliderHolder.JumpCollinder.enabled = false;
        Debug.Log("Exit JumpState");
    }
}
