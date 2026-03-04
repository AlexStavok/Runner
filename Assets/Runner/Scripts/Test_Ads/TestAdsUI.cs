using UnityEngine;
using UnityEngine.UI;

public class TestAdsUI : MonoBehaviour
{
    [SerializeField] private Button _buttonLoadAd;
    [SerializeField] private Button _buttonShowAd;

    private void Start()
    {
        _buttonLoadAd.onClick.AddListener(() =>
        {
            GoogleMobileAdsService.Instance.LoadRewardedAd();
        });
        _buttonShowAd.onClick.AddListener(() =>
        {
            GoogleMobileAdsService.Instance.ShowRewardedAd();
        });
    }
}
