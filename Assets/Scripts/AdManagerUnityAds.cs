using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdManagerUnityAds : MonoBehaviour, IUnityAdsListener
{
    [SerializeField] string googlePlayStoreID = "4079003";
    [SerializeField] string interstitialAdID = "InterstitialAd";
    [SerializeField] string rewardedAdID = "RewardedAd";
    [SerializeField] bool testMode = true;

    [SerializeField] int attemptsBeforeShowingInterstitialAd = 5;
    int attempts = 0;

    [HideInInspector] public int option;

    bool isAllowedToInitialize = true;

    private void Awake()
    { 
        SingletonPattern();
        if (isAllowedToInitialize == true)
        {
            Advertisement.AddListener(this);
            Advertisement.Initialize(googlePlayStoreID, testMode);
        }
    }

    public void LevelHasEnded()
    {
        attempts++;
    }

    public void ShowInterstitialAd()
    {
        if (attempts >= attemptsBeforeShowingInterstitialAd && PlayerPrefs.GetInt("AlreadyUnlockedAllMvrhthufkad") == 0)
        {
            attempts = 0;
            DisplayInterstitialAd();
        }
    }

    private void DisplayInterstitialAd()
    {
        if (Advertisement.IsReady(interstitialAdID))
        {
            Advertisement.Show(interstitialAdID);
        }
    }

    public void ShowRewardedAd()
    {
        if (Advertisement.IsReady(rewardedAdID))
        {
            Advertisement.Show(rewardedAdID);
        }
    }

    public void OnUnityAdsDidStart(string placementId)
    {
        if (placementId == interstitialAdID)
        {
            Debug.Log("Started intestitial ad");
        }
        else if (placementId == rewardedAdID)
        {
            Debug.Log("Started rewarded ad");
        }
    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        switch (showResult)
        {
            case ShowResult.Skipped: // For rewarded interstitial
                if (placementId == interstitialAdID)
                {
                    FindObjectOfType<RewardManager>().RewardPlayer(5);
                }
                break;
            case ShowResult.Finished: // For rewarded interstitial and rewarded ad
                if (placementId == interstitialAdID)
                {
                    FindObjectOfType<RewardManager>().RewardPlayer(6);
                }
                else if (placementId == rewardedAdID)
                {
                    FindObjectOfType<RewardManager>().RewardPlayer(option);
                }
                break;
        }
    }

    // USELESS
    public void OnUnityAdsReady(string placementId)
    {
        // Having to use this method, otherwise the IUnityAdsListener throws an error
    }


    // USELESS
    public void OnUnityAdsDidError(string message)
    {
        // Having to use this method, otherwise the IUnityAdsListener throws an error
    }

    private void SingletonPattern()
    {
        int numberOfAdManagersUnityAds = FindObjectsOfType<AdManagerUnityAds>().Length;
        if (numberOfAdManagersUnityAds > 1)
        {
            isAllowedToInitialize = false;
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}
