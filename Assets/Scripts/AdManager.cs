using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using GoogleMobileAds.Api;

public class AdManager : MonoBehaviour
{
    [SerializeField] int attemptsBeforeShowingInterstitialAd = 5;
    int attemps = 0;

    [SerializeField] string interstitialAdId = "ca-app-pub-3104610384084371/8285706982";
    InterstitialAd interstitialAd1;
    InterstitialAd interstitialAd2;
    bool showingInterstitialAd1 = true;

    [SerializeField] string rewardedAdId = "ca-app-pub-3104610384084371/7874358036";
    RewardedAd rewardedAd1;
    RewardedAd rewardedAd2;
    bool showingRewardedAd1 = true;
    [HideInInspector] public int option;

    bool isAllowedToInitialize = true;

    private void Awake()
    {
        SingletonPattern();
        if (isAllowedToInitialize == true)
        {
            MobileAds.Initialize(initStatus => { });
            LoadInterstitialAd1();
            LoadRewardedAd1();
            StartCoroutine(CheckToSeeIfAdsAreLoaded());
        }
    }

    public void LevelHasEnded()
    {
        attemps++;
    }

    public void ShowInterstitialAd()
    {
        if (attemps >= attemptsBeforeShowingInterstitialAd && PlayerPrefs.GetInt("AlreadyUnlockedAllMvrhthufkad") == 0)
        {
            attemps = 0;
            DisplayInterstitialAd();
        }
    }

    private void LoadInterstitialAd1()
    {
        interstitialAd1 = new InterstitialAd(interstitialAdId);
        AdRequest adRequest = new AdRequest.Builder().Build();
        interstitialAd1.LoadAd(adRequest);

        interstitialAd1.OnAdOpening += InterstitialAd1_OnAdOpening;
        interstitialAd1.OnAdClosed += InterstitialAd1_OnAdClosed;
    }

    private void InterstitialAd1_OnAdOpening(object sender, System.EventArgs e)
    {
        FindObjectOfType<UIManager>().EnableShowingAdImage();
        LoadInterstitialAd2();
    }

    private void InterstitialAd1_OnAdClosed(object sender, System.EventArgs e)
    {
        FindObjectOfType<UIManager>().DisableShowingAdImage();
    }

    private void LoadInterstitialAd2()
    {
        interstitialAd2 = new InterstitialAd(interstitialAdId);
        AdRequest adRequest = new AdRequest.Builder().Build();
        interstitialAd2.LoadAd(adRequest);

        interstitialAd2.OnAdOpening += InterstitialAd2_OnAdOpening;
        interstitialAd2.OnAdClosed += InterstitialAd2_OnAdClosed;
    }

    private void InterstitialAd2_OnAdOpening(object sender, System.EventArgs e)
    {
        FindObjectOfType<UIManager>().EnableShowingAdImage();
        LoadInterstitialAd1();
    }

    private void InterstitialAd2_OnAdClosed(object sender, System.EventArgs e)
    {
        FindObjectOfType<UIManager>().DisableShowingAdImage();
    }

    private void DisplayInterstitialAd()
    {
        if (showingInterstitialAd1 == true)
        {
            if (interstitialAd1.IsLoaded() == true)
            {
                interstitialAd1.Show();
                showingInterstitialAd1 = false;
            }
        }
        else
        {
            if (interstitialAd2.IsLoaded() == true)
            {
                interstitialAd2.Show();
                showingInterstitialAd1 = true;
            }
        }
    }

    private void LoadRewardedAd1()
    {
        rewardedAd1 = new RewardedAd(rewardedAdId);
        AdRequest adRequest = new AdRequest.Builder().Build();
        rewardedAd1.LoadAd(adRequest);

        rewardedAd1.OnAdOpening += RewardedAd1_OnAdOpening;
        rewardedAd1.OnAdClosed += RewardedAd1_OnAdClosed;
        rewardedAd1.OnUserEarnedReward += RewardedAd1_OnUserEarnedReward;
    }

    private void RewardedAd1_OnAdOpening(object sender, System.EventArgs e)
    {
        FindObjectOfType<UIManager>().EnableShowingAdImage();
        LoadRewardedAd2();
    }

    private void RewardedAd1_OnAdClosed(object sender, System.EventArgs e)
    {
        FindObjectOfType<UIManager>().DisableShowingAdImage();
    }

    private void RewardedAd1_OnUserEarnedReward(object sender, Reward e)
    {
        FindObjectOfType<RewardManager>().RewardPlayer(option);
    }

    private void LoadRewardedAd2()
    {
        rewardedAd2 = new RewardedAd(rewardedAdId);
        AdRequest adRequest = new AdRequest.Builder().Build();
        rewardedAd2.LoadAd(adRequest);

        rewardedAd2.OnAdOpening += RewardedAd2_OnAdOpening;
        rewardedAd2.OnAdClosed += RewardedAd2_OnAdClosed;
        rewardedAd2.OnUserEarnedReward += RewardedAd2_OnUserEarnedReward;
    }

    private void RewardedAd2_OnAdOpening(object sender, System.EventArgs e)
    {
        FindObjectOfType<UIManager>().EnableShowingAdImage();
        LoadRewardedAd1();
    }

    private void RewardedAd2_OnAdClosed(object sender, System.EventArgs e)
    {
        FindObjectOfType<UIManager>().DisableShowingAdImage();
    }

    private void RewardedAd2_OnUserEarnedReward(object sender, Reward e)
    {
        FindObjectOfType<RewardManager>().RewardPlayer(option);
    }

    public void ShowRewardedAd()
    {
        if (showingRewardedAd1 == true)
        {
            if (rewardedAd1.IsLoaded() == true)
            {
                rewardedAd1.Show();
                showingRewardedAd1 = false;
            }
        }
        else
        {
            if (rewardedAd2.IsLoaded() == true)
            {
                rewardedAd2.Show();
                showingRewardedAd1 = true;
            }
        }
    }

    private void SingletonPattern()
    {
        int numberOfAdManagers = FindObjectsOfType<AdManager>().Length;
        if (numberOfAdManagers > 1)
        {
            isAllowedToInitialize = false;
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    IEnumerator CheckToSeeIfAdsAreLoaded()
    {
        while (true)
        {
            if (showingInterstitialAd1 == true)
            {
                if (interstitialAd1.IsLoaded() == false)
                {
                    yield return new WaitForSeconds(30);
                    if (interstitialAd1.IsLoaded() == false)
                    {
                        LoadInterstitialAd1();
                    }
                }
            }
            else
            {
                if (interstitialAd2.IsLoaded() == false)
                {
                    yield return new WaitForSeconds(30);
                    if (interstitialAd2.IsLoaded() == false)
                    {
                        LoadInterstitialAd2();
                    }
                }
            }

            if (showingRewardedAd1 == true)
            {
                if (rewardedAd1.IsLoaded() == false)
                {
                    yield return new WaitForSeconds(30);
                    if (rewardedAd1.IsLoaded() == false)
                    {
                        LoadRewardedAd1();
                    }
                }
            }
            else
            {
                if (rewardedAd2.IsLoaded() == false)
                {
                    yield return new WaitForSeconds(30);
                    if (rewardedAd2.IsLoaded() == false)
                    {
                        LoadRewardedAd2();
                    }
                }
            }
            yield return new WaitForSeconds(30);
        }
    }
}
