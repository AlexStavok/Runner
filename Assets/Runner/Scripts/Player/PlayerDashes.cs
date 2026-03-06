using UnityEngine;

public class PlayerDashes : MonoBehaviour
{
    [SerializeField] private float _linesDistance;
    [SerializeField] private float _lineChangeSpeed;

    private int _currentLine = 0;
    private Vector3 _targetPos;

    private bool _isStarted = false;
    private void Awake()
    {
        _targetPos = transform.position;
    }
    public void ProcessInput(EInputCommand inputCommand)
    {
        switch (inputCommand)
        {
            case EInputCommand.MoveLeft:
                ChangeLine(-1);
                break;
            case EInputCommand.MoveRight:
                ChangeLine(1);
                break;
        }
    }
    private void ChangeLine(int direction)
    {
        if (!_isStarted)
            return;

        _currentLine += direction;

        _currentLine = Mathf.Clamp(_currentLine, -1, 1);
        
        _targetPos = new Vector3(_currentLine * _linesDistance, transform.position.y, transform.position.z);
    }
    private void Update()
    {
        Vector3 currentPos = transform.position;

        Vector3 newPos = Vector3.Lerp(currentPos, _targetPos, _lineChangeSpeed * Time.deltaTime);

        transform.position = newPos;
    }
    public void StartGame()
    {
        _isStarted = true;
    }
    public void EndGame()
    {
        _currentLine = -1;
        ChangeLine(1);
        _isStarted = false;
    }
}
