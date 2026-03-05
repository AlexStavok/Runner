using UnityEngine;

public class PlayerDashes : MonoBehaviour
{
    [SerializeField] private float _linesDistance;
    [SerializeField] private float _lineChangeSpeed;

    private int currentLine = 0;
    private Vector3 targetPos;
    private void Awake()
    {
        targetPos = transform.position;
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
        currentLine += direction;

        currentLine = Mathf.Clamp(currentLine, -1, 1);
        
        targetPos = new Vector3(currentLine * _linesDistance, transform.position.y, transform.position.z);
    }
    private void Update()
    {
        Vector3 currentPos = transform.position;

        Vector3 newPos = Vector3.Lerp(currentPos, targetPos, _lineChangeSpeed * Time.deltaTime);

        transform.position = newPos;
    }
}
