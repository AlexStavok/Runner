using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class InGameUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textScore;
    [SerializeField] private Button _buttonPause;

    [SerializeField] private PauseUI _pauseUI;
    private void Start()
    {
        _buttonPause.onClick.AddListener(() =>
        {
            _pauseUI.Show();
        });
    }
    private void Update()
    {
        _textScore.text = ((int)GameManager.Instance.CurrentScore).ToString();
    }
    public void Show()
    {
        _textScore.text = "0";
        gameObject.SetActive(true);
    }
    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
