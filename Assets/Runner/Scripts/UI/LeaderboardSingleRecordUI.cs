using TMPro;
using UnityEngine;

public class LeaderboardSingleRecordUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _nickname;
    [SerializeField] private TextMeshProUGUI _record;
    public void UpdateSingleRecord(int position, string nickname, int record)
    {
        _nickname.text = $"{position}. {nickname}";
        _record.text = record.ToString();
    }
}
