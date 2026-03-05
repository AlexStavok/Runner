using UnityEngine;

public class PlayerInputSystem : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private PlayerDashes _playerDashes;

    private IInputStrategy _inputStrategy;

    private void Awake()
    {
#if UNITY_EDITOR
        _inputStrategy = new KeyboardInputStrategy();
#else
        -inputStrategy = new SwipeInputStrategy();
#endif
        _inputStrategy.Enable();
        _inputStrategy.OnInputCommand += InputStrategy_OnInputCommand;
    }
    private void InputStrategy_OnInputCommand(object sender, EInputCommand e)
    {
        if(e == EInputCommand.Jump || e == EInputCommand.Slide)
        {
            if (_player != null)
            {
                _player.ProcessInput(e);
            }
        }
        else
        {
            if (_playerDashes != null)
            {
                _playerDashes.ProcessInput(e);
            }
        }
    }
    private void OnDestroy()
    {
        _inputStrategy.Enable();
        _inputStrategy.OnInputCommand -= InputStrategy_OnInputCommand;
    }
}
