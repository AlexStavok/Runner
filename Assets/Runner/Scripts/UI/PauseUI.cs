using UnityEngine;
using UnityEngine.UI;

public class PauseUI : MonoBehaviour
{
    [SerializeField] private Button _buttonMainMenu;
    [SerializeField] private Button _buttonClose;

    [SerializeField] private MainMenuUI _mainMenuUI;
    [SerializeField] private InGameUI _inGameUI;
    private void Start()
    {
        _buttonMainMenu.onClick.AddListener(() =>
        {
            GameManager.Instance.ResetGameObjects();
            GameManager.Instance.RestartedAlreadyReset();
            _mainMenuUI.Show();
            _inGameUI.Hide();
            Hide();
        });
        _buttonClose.onClick.AddListener(() =>
        {
            Hide();
        });
    }
    public void Show()
    {
        Time.timeScale = 0f;
        gameObject.SetActive(true);
    }
    public void Hide()
    {
        Time.timeScale = 1.0f;
        gameObject.SetActive(false);
    }
}
