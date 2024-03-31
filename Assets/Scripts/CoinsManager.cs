using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinsManager : MonoBehaviour
{
    [HideInInspector] public int coins;

    public int levelCompletedReward = 250;
    public int levelFailedReward = 50;

    [SerializeField] TextMeshProUGUI coinsText;
    [SerializeField] AudioClip buttonsSFX;
    AudioSource buttonsAudioSource;

    private void Awake()
    {
        coins = PlayerPrefs.GetInt("CoinsMvrhthufkad");
        coinsText.text = "Coins: " + coins.ToString();
        buttonsAudioSource = GameObject.FindGameObjectWithTag("Buttons Audio Source").GetComponent<AudioSource>();
    }

    public void SpendCoins(int itemCost)
    {
        coins = coins - itemCost;
        coinsText.text = "Coins: " + coins.ToString();
        PlayerPrefs.SetInt("CoinsMvrhthufkad", coins);
    }

    public void RewardPlayerAfterCompletingLevel(int reward)
    {
        coins = coins + reward;
        coinsText.text = "Coins: " + coins.ToString();
        PlayerPrefs.SetInt("CoinsMvrhthufkad", coins);
    }

    public void RewardPlayerForWatchingRewardedAd()
    {
        buttonsAudioSource.PlayOneShot(buttonsSFX);
        coins = coins + 30;
        coinsText.text = "Coins: " + coins.ToString();
        PlayerPrefs.SetInt("CoinsMvrhthufkad", coins);
    }

    public void RewardPlayerForSkippingInterstitialAd()
    {
        buttonsAudioSource.PlayOneShot(buttonsSFX);
        coins = coins + 5;
        coinsText.text = "Coins: " + coins.ToString();
        PlayerPrefs.SetInt("CoinsMvrhthufkad", coins);
    }

    public void RewardPlayerForWatchingInterstitialAd()
    {
        buttonsAudioSource.PlayOneShot(buttonsSFX);
        coins = coins + 10;
        coinsText.text = "Coins: " + coins.ToString();
        PlayerPrefs.SetInt("CoinsMvrhthufkad", coins);
    }
}
