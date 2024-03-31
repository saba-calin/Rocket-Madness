using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RewardManager : MonoBehaviour
{
    [SerializeField] ShopAds[] shopAds;
    [HideInInspector] public int shopAdsIndex;

    public void RewardPlayer(int option)
    {
        StartCoroutine(rewardPlayer(option)); // Delaying it because the app keeps crashing when rewarding using AdMob
    }                                         

    IEnumerator rewardPlayer(int option)
    {
        yield return new WaitForSeconds(1);
        switch (option) // Rewards after watching the video
        {
            case 1: // Watch the last ad to unlock the rocket
                shopAds[shopAdsIndex].UnlockRocket();
                break;

            case 2: // Watch an ad to reduce the number of ads left to watch before unlocking the rocket
                shopAds[shopAdsIndex].WatchAdToUnlockRocket();
                break;

            case 3: // Get coins
                FindObjectOfType<CoinsManager>().RewardPlayerForWatchingRewardedAd();
                if (SceneManager.GetActiveScene().name == "Shop")
                {
                    FindObjectOfType<UIManager>().DisableNotEnoughCoinsImage();
                }
                break;

            case 4: // Skip level
                FindObjectOfType<UIManager>().DisableLoseImage();
                StartCoroutine(FindObjectOfType<Buttons>().skipLevel());
                break;

            case 5: // Skipped interstitial ad
                FindObjectOfType<CoinsManager>().RewardPlayerForSkippingInterstitialAd();
                break;

            case 6: // Watched the whole intestitial ad
                FindObjectOfType<CoinsManager>().RewardPlayerForWatchingInterstitialAd();
                break;
        }
    }
}
