using System;

public interface IInputStrategy
{
    public event EventHandler<EInputCommand> OnInputCommand;

    public void Enable();
    public void Disable();
}