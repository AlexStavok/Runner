using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private Button _exitButton;
    [SerializeField] private Button _logoutButton;
    [SerializeField] private Button _leaderboardButton;
    [SerializeField] private Button _playButton;

    [SerializeField] private LeaderboardUI _leaderboardUI;

    private void Start()
    {
        _exitButton.onClick.AddListener(() =>
        {
            Application.Quit();
        });
        _logoutButton.onClick.AddListener(() =>
        {
            FirebaseService.Instance.Logout();
            SceneManager.LoadScene("Scene_Auth");
        });
        _leaderboardButton.onClick.AddListener(() =>
        {
            _leaderboardUI.Show();
            Hide();
        });
        _playButton.onClick.AddListener(() =>
        {
            GameManager.Instance.StartGame(true);
        });
    }
    public void Show()
    {
        gameObject.SetActive(true);
    }
    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
