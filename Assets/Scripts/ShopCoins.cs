using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using TMPro;

public class ShopCoins : MonoBehaviour
{
    [SerializeField] int itemCost = 0;

    [Tooltip("Used for buttons and texts management")]
    [SerializeField] GameObject buyButton;
    [SerializeField] GameObject selectButton;
    [SerializeField] GameObject selectedImage;
    [SerializeField] GameObject coinsText;
    [SerializeField] GameObject coinsImage;

    [Tooltip("Used for saving")]
    string save = "Mvrhthufkad";

    [Tooltip("Used for buttons SFX")]
    [SerializeField] AudioClip buyRocketSFX;
    [SerializeField] AudioClip selectRocketSFX;
    [SerializeField] AudioClip notEnoughCoinsSFX;
    AudioSource buttonsAudioSource;

    ShopCoins[] coinsShops;
    ShopAds[] adsShops;

    UIManager uIManager;

    private void Awake() // Make sure that everything is disabled in the hierarchy
    {
        ManageButtonsAndTexts();
        coinsShops = FindObjectsOfType<ShopCoins>();
        adsShops = FindObjectsOfType<ShopAds>();
        buttonsAudioSource = GameObject.FindGameObjectWithTag("Buttons Audio Source").GetComponent<AudioSource>();
        uIManager = FindObjectOfType<UIManager>();
    }
    
    public void BuyButton()
    {
        if (FindObjectOfType<CoinsManager>().coins >= itemCost)
        {
            buttonsAudioSource.PlayOneShot(buyRocketSFX);
            FindObjectOfType<CoinsManager>().SpendCoins(itemCost);
            buyButton.SetActive(false);
            coinsText.SetActive(false);
            coinsImage.SetActive(false);
            selectedImage.SetActive(true);
            PlayerPrefs.SetInt(gameObject.name + save, Convert.ToInt32(gameObject.name));
            PlayerPrefs.SetInt("SelectedRocketMvrhthufkad", Convert.ToInt32(gameObject.name));
            DisableSelectedImageAndManageButtonsAndTexts();
        }
        else
        {
            buttonsAudioSource.PlayOneShot(notEnoughCoinsSFX);
            uIManager.EnableNotEnoughCoinsImage();
        }
    }

    public void SelectButton()
    {
        buttonsAudioSource.PlayOneShot(selectRocketSFX);
        selectButton.SetActive(false);
        selectedImage.SetActive(true);
        PlayerPrefs.SetInt("SelectedRocketMvrhthufkad", Convert.ToInt32(gameObject.name));
        DisableSelectedImageAndManageButtonsAndTexts();
    }

    private void DisableSelectedImageAndManageButtonsAndTexts()
    {
        foreach (ShopCoins shop in coinsShops)
        {
            shop.DisableSelectedImage();
            shop.ManageButtonsAndTexts();
        }
        foreach (ShopAds shop in adsShops)
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
            buyButton.SetActive(true);
            coinsText.GetComponent<TextMeshProUGUI>().text = itemCost.ToString();
            coinsText.SetActive(true);
            coinsImage.SetActive(true);
        }
    }

    public void UnlockRocket()
    {
        PlayerPrefs.SetInt(gameObject.name + save, Convert.ToInt32(gameObject.name));
    }

    public void DisableEverything()
    {
        buyButton.SetActive(false);
        selectButton.SetActive(false);
        selectedImage.SetActive(false);
        coinsText.SetActive(false);
        coinsImage.SetActive(false);
    }
}
