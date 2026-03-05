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
        PlayerStateMachine.Initialize(new PlayerRunState(this, PlayerStateMachine));
        PlayerAnimatorController.OnBackToRun += PlayerAnimatorController_OnBackToRun;
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
}

[System.Serializable]
public class ColliderHolder
{
    public CapsuleCollider RunCollider;
    public CapsuleCollider JumpCollinder;
    public CapsuleCollider SlideCollider;
}