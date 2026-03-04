using UnityEngine;
using GoogleMobileAds;
using GoogleMobileAds.Api;
using System;

public class GoogleMobileAdsService : MonoBehaviour
{
    public static GoogleMobileAdsService Instance;

    private readonly string _adUnitID = "ca-app-pub-3940256099942544/5224354917";

    private RewardedAd _rewardedAd;

    public event EventHandler OnRewardAdEarned;

    private void Awake()
    {
        if(Instance == null) 
            Instance = this;

        MobileAds.Initialize((InitializationStatus initStatus) =>
        {
            Debug.Log("MobileAds initialised");
        });
    }
    public void LoadRewardedAd()
    {
        var adRequest = new AdRequest();

        RewardedAd.Load(_adUnitID, adRequest, (RewardedAd ad, LoadAdError error) =>
        {
            if (error != null)
            {
                Debug.LogWarning($"Error with rewarded ad load: {error}");
                return;
            }

            _rewardedAd = ad;
            Debug.Log($"Rewarded ad loaded");
        });
    }
    public void ShowRewardedAd()
    {
        if (_rewardedAd != null && _rewardedAd.CanShowAd())
        {
            _rewardedAd.Show((Reward reward) =>
            {
                OnRewardAdEarned?.Invoke(this, EventArgs.Empty);
                Debug.Log("Rewarded ad earned");
            });
        }
    }
}
