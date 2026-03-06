using UnityEngine;

public class Player : MonoBehaviour
{
    public ColliderHolder ColliderHolder;
    public PlayerAnimatorController PlayerAnimatorController;

    private PlayerStateMachine PlayerStateMachine;

    private void Awake()
    {
        PlayerStateMachine = new PlayerStateMachine();
    }
    private void Start()
    {
        PlayerStateMachine.Initialize(new PlayerIdleState(this, PlayerStateMachine));
    }
    private void PlayerAnimatorController_OnSlideEnd(object sender, System.EventArgs e)
    {
        ColliderHolder.SlideCollider.enabled = false;
        ColliderHolder.RunCollider.enabled = true;
    }

    private void PlayerAnimatorController_OnJumpEnd(object sender, System.EventArgs e)
    {
        ColliderHolder.JumpCollinder.enabled = false;
        ColliderHolder.RunCollider.enabled = true;
    }

    private void PlayerAnimatorController_OnBackToRun(object sender, System.EventArgs e)
    {
        BackToRunState();
    }

    public void ProcessInput(EInputCommand inputCommand)
    {
        PlayerStateMachine.CurrentState.HandleInput(inputCommand);
    }
    public void BackToRunState()
    {
        PlayerStateMachine.ChangeState(new PlayerRunState(this, PlayerStateMachine));
    }
    public void BackToIdleState()
    {
        PlayerStateMachine.ChangeState(new PlayerIdleState(this, PlayerStateMachine));
    }
    public void StartGame()
    {
        PlayerAnimatorController.OnBackToRun += PlayerAnimatorController_OnBackToRun;
        PlayerAnimatorController.OnJumpEnd += PlayerAnimatorController_OnJumpEnd;
        PlayerAnimatorController.OnSlideEnd += PlayerAnimatorController_OnSlideEnd;
        BackToRunState();
    }
    public void EndGame()
    {
        PlayerAnimatorController.OnBackToRun -= PlayerAnimatorController_OnBackToRun;
        PlayerAnimatorController.OnJumpEnd -= PlayerAnimatorController_OnJumpEnd;
        PlayerAnimatorController.OnSlideEnd -= PlayerAnimatorController_OnSlideEnd;
        BackToIdleState();
    }
}

[System.Serializable]
public class ColliderHolder
{
    public CapsuleCollider RunCollider;
    public CapsuleCollider JumpCollinder;
    public CapsuleCollider SlideCollider;
}