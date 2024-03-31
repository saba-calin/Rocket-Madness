using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using TMPro;

public class ShopAds : MonoBehaviour
{
    [SerializeField] int shopAdsIndex;
    [SerializeField] int adsToWatch = 10;
    int updatedAdsToWatch;

    [Tooltip("Used for buttons and texts management")]
    [SerializeField] GameObject watchButton;
    [SerializeField] GameObject selectButton;
    [SerializeField] GameObject selectedImage;
    [SerializeField] GameObject adsText;
    [SerializeField] GameObject adsImage;

    [Tooltip("Used for saving")]
    [SerializeField] string uniqueSavingID = "fsdfasf"; // Mash buttons altogether
    string save = "Mvrhthufkad";

    [Tooltip("Used for buttons SFX")]
    [SerializeField] AudioClip watchAdForRocketSFX;
    [SerializeField] AudioClip selectRocketSFX;
    [SerializeField] AudioClip warningSFX;
    AudioSource buttonsAudioSource;

    ShopCoins[] coinsShops;
    ShopAds[] adsShops;
    RewardManager rewardManager;

    private void Awake() // Make sure that everything is disabled in the hierarchy
    {
        UpdateAdsToWatch();
        ManageButtonsAndTexts();
        coinsShops = FindObjectsOfType<ShopCoins>();
        adsShops = FindObjectsOfType<ShopAds>();
        rewardManager = FindObjectOfType<RewardManager>();
        buttonsAudioSource = GameObject.FindGameObjectWithTag("Buttons Audio Source").GetComponent<AudioSource>();
    }

    private void Start()
    {
        PlayerPrefs.SetInt("FirstTimeEnteringShopMvrhthufkad", 1);
    }

    private void UpdateAdsToWatch()
    {
        if (PlayerPrefs.GetInt("FirstTimeEnteringShopMvrhthufkad") == 0)
        {
            updatedAdsToWatch = adsToWatch;
            PlayerPrefs.SetInt(uniqueSavingID, updatedAdsToWatch);
        }
        else
        {
            updatedAdsToWatch = PlayerPrefs.GetInt(uniqueSavingID);
        }    
    }

    public void WatchAd()
    {
        rewardManager.shopAdsIndex = shopAdsIndex;
        if (updatedAdsToWatch == 1)
        {
            FindObjectOfType<AdManagerUnityAds>().option = 1;
            FindObjectOfType<AdManagerUnityAds>().ShowRewardedAd();
        }
        else
        {
            FindObjectOfType<AdManagerUnityAds>().option = 2;
            FindObjectOfType<AdManagerUnityAds>().ShowRewardedAd();
        }
    }

    public void SelectRocket()
    {
        buttonsAudioSource.PlayOneShot(selectRocketSFX);
        selectButton.SetActive(false);
        selectedImage.SetActive(true);
        PlayerPrefs.SetInt("SelectedRocketMvrhthufkad", Convert.ToInt32(gameObject.name));
        DisableSelectedImageAndManageButtonsAndTexts();
    }

    public void UnlockRocket() // This should be called from the AdManager script
    {
        buttonsAudioSource.PlayOneShot(watchAdForRocketSFX);
        watchButton.SetActive(false);
        adsText.SetActive(false);
        adsImage.SetActive(false);
        selectedImage.SetActive(true);
        PlayerPrefs.SetInt(gameObject.name + save, Convert.ToInt32(gameObject.name));
        PlayerPrefs.SetInt("SelectedRocketMvrhthufkad", Convert.ToInt32(gameObject.name));
        DisableSelectedImageAndManageButtonsAndTexts();
    }

    public void WatchAdToUnlockRocket() // This should be called from the AdManager script
    {
        buttonsAudioSource.PlayOneShot(watchAdForRocketSFX);
        updatedAdsToWatch--;
        PlayerPrefs.SetInt(uniqueSavingID, updatedAdsToWatch);
        adsText.GetComponent<TextMeshProUGUI>().text = updatedAdsToWatch.ToString();
    }

    private void DisableSelectedImageAndManageButtonsAndTexts()
    {
        foreach (ShopAds shop in adsShops)
        {
            shop.DisableSelectedImage();
            shop.ManageButtonsAndTexts();
        }
        foreach (ShopCoins shop in coinsShops)
        {
            shop.DisableSelectedImage();
            shop.ManageButtonsAndTexts();
        }
    }

    public void DisableSelectedImage()
    {
        selectedImage.SetActive(false);
    }

    public void ManageButtonsAndTexts()
    {
        if (PlayerPrefs.GetInt("SelectedRocketMvrhthufkad") == Convert.ToInt32(gameObject.name))
        {
            selectedImage.SetActive(true);
        }
        else if (PlayerPrefs.GetInt(gameObject.name + save) == Convert.ToInt32(gameObject.name))
        {
            selectButton.SetActive(true);
        }
        else
        {
            watchButton.SetActive(true);
            adsText.GetComponent<TextMeshProUGUI>().text = updatedAdsToWatch.ToString();
            adsText.SetActive(true);
            adsImage.SetActive(true);
        }
    }

    public void unlockRocket()
    {
        PlayerPrefs.SetInt(gameObject.name + save, Convert.ToInt32(gameObject.name));
    }

    public void DisableEverything()
    {
        watchButton.SetActive(false);
        selectButton.SetActive(false);
        selectedImage.SetActive(false);
        adsText.SetActive(false);
        adsImage.SetActive(false);
    }
}
