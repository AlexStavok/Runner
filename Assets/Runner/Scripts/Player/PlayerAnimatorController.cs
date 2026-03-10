using System;
using UnityEngine;

public class PlayerAnimatorController : MonoBehaviour
{
    [SerializeField] private Animator _animatorController;

    public event EventHandler OnBackToRun;
    public event EventHandler OnJumpEnd;
    public event EventHandler OnSlideEnd;

    private const string RunTrigger = "Run";
    private const string JumpTrigger = "Jump";
    private const string SlideTrigger = "Slide";
    private const string IdleTrigger = "Idle";

    public void JumpEnd()
    {
        OnJumpEnd?.Invoke(this, EventArgs.Empty);
    }
    public void SlideEnd()
    {
        OnSlideEnd?.Invoke(this, EventArgs.Empty);
    }
    public void BackToRun()
    {
        OnBackToRun?.Invoke(this, EventArgs.Empty);
    }
    public void StartRun()
    {
        _animatorController.SetTrigger(RunTrigger);
    }
    public void StartJump()
    {
        _animatorController.SetTrigger(JumpTrigger);
    }
    public void StartSlide()
    {
        _animatorController.SetTrigger(SlideTrigger);
    }
    public void StartIdle()
    {
        _animatorController.SetTrigger(IdleTrigger);
    }
}
