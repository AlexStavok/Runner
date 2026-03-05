using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class KeyboardInputStrategy : IInputStrategy
{
    public event EventHandler<EInputCommand> OnInputCommand;

    private InputActions _inputActions;
    public KeyboardInputStrategy()
    {
        _inputActions = new InputActions();
    }

    private void MoveRight_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnInputCommand?.Invoke(this, EInputCommand.MoveRight);
    }

    private void MoveLeft_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnInputCommand?.Invoke(this, EInputCommand.MoveLeft);
    }

    private void Slide_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnInputCommand?.Invoke(this, EInputCommand.Slide);
    }

    private void Jump_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnInputCommand?.Invoke(this, EInputCommand.Jump);
    }
    public void Enable()
    {
        _inputActions.PlayerInput.Enable();

        _inputActions.PlayerInput.Jump.performed += Jump_performed;
        _inputActions.PlayerInput.Slide.performed += Slide_performed;
        _inputActions.PlayerInput.MoveLeft.performed += MoveLeft_performed;
        _inputActions.PlayerInput.MoveRight.performed += MoveRight_performed;
    }

    public void Disable()
    {
        _inputActions.PlayerInput.Disable();

        _inputActions.PlayerInput.Jump.performed -= Jump_performed;
        _inputActions.PlayerInput.Slide.performed -= Slide_performed;
        _inputActions.PlayerInput.MoveLeft.performed -= MoveLeft_performed;
        _inputActions.PlayerInput.MoveRight.performed -= MoveRight_performed;
    }
}
