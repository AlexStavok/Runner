using System;
using UnityEditor.Animations;
using UnityEngine;

public class PlayerAnimatorController : MonoBehaviour
{
    [SerializeField] private Animator _animatorController;

    public event EventHandler OnBackToRun;

    private const string RunTrigger = "Run";
    private const string JumpTrigger = "Jump";
    private const string SlideTrigger = "Slide";

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
}
