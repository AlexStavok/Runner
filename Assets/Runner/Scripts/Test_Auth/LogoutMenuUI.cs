using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LogoutMenuUI : MonoBehaviour
{
    [SerializeField] private Button _buttonLogout;
    [SerializeField] private Button _buttonUpdateScore;

    [SerializeField] private AuthMenuUI _authMenuUI;

    [SerializeField] private TextMeshProUGUI _textNickname;
    [SerializeField] private TextMeshProUGUI _textRecordScore;
    [SerializeField] private TextMeshProUGUI _textLeaderboard;

    [SerializeField] private TMP_InputField _inputNewScore;
    private void Start()
    {
        _buttonLogout.onClick.AddListener(() =>
        {
            FirebaseService.Instance.Logout();
            _authMenuUI.Show();
            Hide();
        });
        _buttonUpdateScore.onClick.AddListener(() =>
        {
            FirebaseService.Instance.UpdatePlayerRecordScore(int.Parse(_inputNewScore.text));
            RequestPlayerData();
        });

        FirebaseService.Instance.OnPlayerDataLoaded += FirebaseService_OnPlayerDataLoaded;
        FirebaseService.Instance.OnPlayerDataUpdated += FirebaseService_OnPlayerDataUpdated;
        FirebaseService.Instance.OnLeaderboardLoaded += FirebaseService_OnLeaderboardLoaded;
    }

    private void FirebaseService_OnLeaderboardLoaded(object sender, System.Collections.Generic.List<PlayerData> e)
    {
        _textLeaderboard.text = "";

        for (int i = 0; i < e.Count; i++)
        {
            var player = e[i];
            _textLeaderboard.text += $"{i + 1} {player.Nickname}: {player.RecordScore}\n";
        }
    }

    private void FirebaseService_OnPlayerDataUpdated(object sender, EventArgs e)
    {
        RequestPlayerData();
        RequestLeaderBoard();
    }

    private void FirebaseService_OnPlayerDataLoaded(object sender, PlayerData e)
    {
        _textNickname.text = e.Nickname;
        _textRecordScore.text = e.RecordScore.ToString();
    }

    private void RequestPlayerData()
    {
        FirebaseService.Instance.RequestPlayerData();
    }
    private void RequestLeaderBoard()
    {
        FirebaseService.Instance.RequestLeaderboard(5);
    }
    public void Show()
    {
        RequestPlayerData();
        RequestLeaderBoard();
        gameObject.SetActive(true);
    }
    public void Hide()
    {
        gameObject.SetActive(false);
    }
    private void OnDestroy()
    {
        FirebaseService.Instance.OnPlayerDataLoaded -= FirebaseService_OnPlayerDataLoaded;
        FirebaseService.Instance.OnPlayerDataUpdated -= FirebaseService_OnPlayerDataUpdated;
    }
}
