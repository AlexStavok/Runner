using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private Button _exitButton;
    [SerializeField] private Button _logoutButton;
    [SerializeField] private Button _leaderboardButton;
    [SerializeField] private Button _playButton;

    [SerializeField] private TextMeshProUGUI _recordText;

    [SerializeField] private LeaderboardUI _leaderboardUI;
    [SerializeField] private InGameUI _inGameUI;

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
            _inGameUI.Show();
        });

        GameManager.Instance.OnRecordChanged += GameManager_OnRecordChanged;
    }

    private void GameManager_OnRecordChanged(object sender, System.EventArgs e)
    {
        _recordText.text = $"Record: {GameManager.Instance.ScoreRecord}";
    }

    public void Show()
    {
        gameObject.SetActive(true);
        _recordText.text = $"Record: {GameManager.Instance.ScoreRecord.ToString()}";
    }
    public void Hide()
    {
        gameObject.SetActive(false);
    }
    private void OnDestroy()
    {
        GameManager.Instance.OnRecordChanged -= GameManager_OnRecordChanged;
    }
}
