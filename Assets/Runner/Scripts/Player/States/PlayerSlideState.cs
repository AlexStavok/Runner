using UnityEngine;

public class PlayerSlideState : PlayerState
{
    public PlayerSlideState(Player player, PlayerStateMachine stateMachine) : base(player, stateMachine) { }
    public override void Enter()
    {
        Debug.Log("Enter SlideState");
        Player.ColliderHolder.SlideCollider.enabled = true;
        Player.PlayerAnimatorController.StartSlide();
    }

    public override void HandleInput(EInputCommand inputCommand)
    {
        if (inputCommand == EInputCommand.Jump)
            StateMachine.ChangeState(new PlayerRunState(Player, StateMachine));
    }
    public override void Exit()
    {

        Player.ColliderHolder.SlideCollider.enabled = false;
        Debug.Log("Exit SlideState");
    }
}
