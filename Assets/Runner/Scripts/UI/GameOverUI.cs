using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private Button _buttonRespawn;
    [SerializeField] private Button _buttonMainMenu;

    [SerializeField] private TextMeshProUGUI _scoreText;

    [SerializeField] private MainMenuUI _mainMenuUI;
    [SerializeField] private InGameUI _inGameUI;
    private void Start()
    {
        _buttonRespawn.onClick.AddListener(() =>
        {
            GoogleMobileAdsService.Instance.ShowRewardedAd();
        });
        _buttonMainMenu.onClick.AddListener(() =>
        {
            GameManager.Instance.RestartedAlreadyReset();
            _inGameUI.Hide();
            _mainMenuUI.Show();
            Hide();
        });
        GoogleMobileAdsService.Instance.OnRewardAdEarned += GoogleMobileAdsService_OnRewardAdEarned;
    }

    private void GoogleMobileAdsService_OnRewardAdEarned(object sender, System.EventArgs e)
    {
        GameManager.Instance.StartGame(false);
        Hide();
    }

    public void Show(int score, bool hideAdButton)
    {
        if (!hideAdButton)
        {
            GoogleMobileAdsService.Instance.LoadRewardedAd();
            _buttonRespawn.gameObject.SetActive(true);
        }
        else
            _buttonRespawn.gameObject.SetActive(false);

        _scoreText.text = score.ToString();

        gameObject.SetActive(true);
    }
    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
