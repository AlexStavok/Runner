using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class SwipeInputStrategy : IInputStrategy
{
    public event EventHandler<EInputCommand> OnInputCommand;

    private Vector2 _startPos;
    private float _minSwipeDistance = 50f;

    private void InputSystem_onAfterUpdate()
    {
        SwipeDetect();
    }

    private void SwipeDetect()
    {
        if (Touchscreen.current == null)
            return;

        var touch = Touchscreen.current.primaryTouch;

        if (touch.press.wasPressedThisFrame)
        {
            _startPos = touch.position.ReadValue();
        }

        if (touch.press.wasReleasedThisFrame)
        {
            Vector2 endPos = touch.position.ReadValue();
            Vector2 delta = endPos - _startPos;

            if (delta.magnitude < _minSwipeDistance)
                return;

            EInputCommand eInputCommand;

            if (Mathf.Abs(delta.x) > Mathf.Abs(delta.y))
                eInputCommand = delta.x > 0 ? EInputCommand.MoveRight : EInputCommand.MoveLeft;
            else
                eInputCommand = delta.y > 0 ? EInputCommand.Jump : EInputCommand.Slide;

            OnInputCommand?.Invoke(this, eInputCommand);
        }
    }
    public void Enable()
    {
        InputSystem.onAfterUpdate += InputSystem_onAfterUpdate;
    }

    public void Disable()
    {
        InputSystem.onAfterUpdate -= InputSystem_onAfterUpdate;
    }
}
