using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.InputSystem.LowLevel.InputStateHistory;

public class LeaderboardUI : MonoBehaviour
{
    [SerializeField] private LeaderboardSingleRecordUI _goldenRecordPrefab;
    [SerializeField] private LeaderboardSingleRecordUI _silverRecordPrefab;
    [SerializeField] private LeaderboardSingleRecordUI _copperRecordPrefab;
    [SerializeField] private LeaderboardSingleRecordUI _regularRecordPrefab;

    [SerializeField] private Transform _scrollContent;

    [SerializeField] private TextMeshProUGUI _textPlayerPosition;
    [SerializeField] private TextMeshProUGUI _textPlayerNickname;
    [SerializeField] private TextMeshProUGUI _textPlayerRecord;

    [SerializeField] private Button _buttonMainMenu;
    [SerializeField] private MainMenuUI _mainMenuUI;

    [SerializeField] private int numberOfTopRecords;

    private List<LeaderboardSingleRecordUI> _records;
    private void Awake()
    {
        _records = new List<LeaderboardSingleRecordUI>();
    }
    private void Start()
    {
        _buttonMainMenu.onClick.AddListener(() =>
        {
            _mainMenuUI.Show();
            Hide();
        });

        FirebaseService.Instance.OnLeaderboardLoaded += FirebaseService_OnLeaderboardLoaded;
        FirebaseService.Instance.OnPlayerDataLoaded += FirebaseService_OnPlayerDataLoaded;
        FirebaseService.Instance.OnPlayerRankLoaded += FirebaseService_OnPlayerRankLoaded;
    }

    private void FirebaseService_OnPlayerRankLoaded(object sender, int e)
    {
        _textPlayerPosition.text = $"{e}.";
    }

    private void FirebaseService_OnPlayerDataLoaded(object sender, PlayerData e)
    {
        _textPlayerNickname.text = e.Nickname;
        _textPlayerRecord.text = e.RecordScore.ToString();
    }

    private void FirebaseService_OnLeaderboardLoaded(object sender, System.Collections.Generic.List<PlayerData> e)
    {
        UpdateRecords(e);
    }
    private void UpdateRecords(List<PlayerData> newRecords)
    {
        if(_records.Count > 0)
        {
            foreach(var record in _records)
            {
                Destroy(record.gameObject);
            }
            _records.Clear();
        }

        for (int i = 0; i < newRecords.Count; i++)
        {
            switch (i)
            {
                case 0:
                    LeaderboardSingleRecordUI goldenRecord = Instantiate(_goldenRecordPrefab, _scrollContent);
                    _records.Add(goldenRecord);
                    goldenRecord.UpdateSingleRecord(i + 1, newRecords[i].Nickname, newRecords[i].RecordScore);
                    break;
                case 1:
                    LeaderboardSingleRecordUI silverRecord = Instantiate(_silverRecordPrefab, _scrollContent);
                    _records.Add(silverRecord);
                    silverRecord.UpdateSingleRecord(i + 1, newRecords[i].Nickname, newRecords[i].RecordScore);
                    break;
                case 2:
                    LeaderboardSingleRecordUI copperRecord = Instantiate(_copperRecordPrefab, _scrollContent);
                    _records.Add(copperRecord);
                    copperRecord.UpdateSingleRecord(i + 1, newRecords[i].Nickname, newRecords[i].RecordScore);
                    break;
                case > 2:
                    LeaderboardSingleRecordUI regularRecord = Instantiate(_regularRecordPrefab, _scrollContent);
                    _records.Add(regularRecord);
                    regularRecord.UpdateSingleRecord(i + 1, newRecords[i].Nickname, newRecords[i].RecordScore);
                    break;
            }
        }
    }

    public void Show()
    {
        FirebaseService.Instance.RequestLeaderboard(numberOfTopRecords);
        FirebaseService.Instance.RequestPlayerData();
        FirebaseService.Instance.RequestPlayerPosition();
        gameObject.SetActive(true);
    }
    public void Hide()
    {
        gameObject.SetActive(false);
    }
    private void OnDestroy()
    {
        FirebaseService.Instance.OnLeaderboardLoaded -= FirebaseService_OnLeaderboardLoaded;
        FirebaseService.Instance.OnPlayerDataLoaded -= FirebaseService_OnPlayerDataLoaded;
        FirebaseService.Instance.OnPlayerRankLoaded -= FirebaseService_OnPlayerRankLoaded;
    }
}
