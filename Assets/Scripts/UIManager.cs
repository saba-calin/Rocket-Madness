using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject startEndImage;
    [SerializeField] GameObject winImage;
    [SerializeField] GameObject loseImage;
    [SerializeField] GameObject shopImage;
    [SerializeField] GameObject shopButton;
    [SerializeField] GameObject coinsImage;
    [SerializeField] GameObject getCoinsButton;
    [SerializeField] GameObject levelEndedCoins;
    [SerializeField] GameObject settingsImage;
    [SerializeField] GameObject notEnoughCoinsImage;
    [SerializeField] GameObject showingAdImage;
    [SerializeField] GameObject GDPRConsentImage;

    [HideInInspector] public bool isAllowedToPlayMissileExplosionSound = true;

    GameSession gameSession;

    [SerializeField] AudioClip buttonsSFX;
    AudioSource buttonsAudioSource;

    private void Awake()
    {
        if (SceneManager.GetActiveScene().name == "Shop" || SceneManager.GetActiveScene().name == "Settings")
        {
            shopImage.SetActive(true);
            shopImage.GetComponent<Animator>().SetTrigger("Shop");
        }
        else
        {
            startEndImage.SetActive(true);
            winImage.SetActive(false);
            loseImage.SetActive(false);
            shopImage.SetActive(false);
            StartCoroutine(DisableStartEndImage());
        }
        gameSession = FindObjectOfType<GameSession>();
        buttonsAudioSource = GameObject.FindGameObjectWithTag("Buttons Audio Source").GetComponent<AudioSource>();
        //CheckIfAcceptedGDPR();
    }

    private void CheckIfAcceptedGDPR()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)      // The GDPR consent needs to accepted only once
        {                                                       // Displaying it only at the first level because any PlayerPrefs key is 0 by default
            int acceptedPrivacyPolicy = PlayerPrefs.GetInt("AcceptedGDPRConsentMvrhthufkad");
            if (acceptedPrivacyPolicy == 0)
            {
                Time.timeScale = 0;
                GDPRConsentImage.SetActive(true);
            }
        }
    }

    public void DisableGDPRConsentImage()
    {
        GDPRConsentImage.SetActive(false);
    }

    IEnumerator DisableStartEndImage()
    {
        float timeUntilDisabling = startEndImage.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length;
        yield return new WaitForSeconds(timeUntilDisabling);
        startEndImage.SetActive(false);
    }

    public void DisplayWinUI()
    {
        EnableStartEndImage();
        StartCoroutine(EnableWinImage());
    }

    public void DisplayLoseUI()
    {
        EnableStartEndImage();
        StartCoroutine(EnableLoseImage());
    }

    public void EnableStartEndImage()
    {
        startEndImage.SetActive(true);
        startEndImage.GetComponent<Animator>().SetTrigger("GameHasEnded");
        StartCoroutine(EnableShopButton());
        StartCoroutine(EnableCoinsImage());
        StartCoroutine(EnableGetCoinsButton());
        StartCoroutine(EnableSettingsImage());
    }

    IEnumerator EnableShopButton()
    {
        float timeUntilEnabling = startEndImage.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length;
        yield return new WaitForSeconds(timeUntilEnabling + 0.2f);
        shopButton.SetActive(true);
    }

    IEnumerator EnableCoinsImage()
    {
        float timeUntilEnabling = startEndImage.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length;
        yield return new WaitForSeconds(timeUntilEnabling + 0.4f);
        coinsImage.SetActive(true);
        StartCoroutine(EnableLevelEndedCoins());
    }

    IEnumerator EnableLevelEndedCoins()
    {
        float timeUntilEnabling = coinsImage.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length;
        yield return new WaitForSeconds(timeUntilEnabling);
        int coins = 0;
        if (gameSession.state == GameSession.State.Dead)
        {
            coins = FindObjectOfType<CoinsManager>().levelFailedReward;
        }
        else if (gameSession.state == GameSession.State.Winning)
        {
            coins = FindObjectOfType<CoinsManager>().levelCompletedReward;
        }
        levelEndedCoins.GetComponentInChildren<TextMeshProUGUI>().text = "Coins: +" + coins.ToString();
        levelEndedCoins.SetActive(true);
        float timeUntilRewardingPlayerForCompletingLevel = levelEndedCoins.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length;
        yield return new WaitForSeconds(timeUntilRewardingPlayerForCompletingLevel);
        FindObjectOfType<CoinsManager>().RewardPlayerAfterCompletingLevel(coins);
    }

    IEnumerator EnableGetCoinsButton()
    {
        float timeUntilEnabling = startEndImage.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length;
        yield return new WaitForSeconds(timeUntilEnabling + 0.6f);
        getCoinsButton.SetActive(true);
    }

    IEnumerator EnableSettingsImage()
    {
        float timeUntilEnabling = startEndImage.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length;
        yield return new WaitForSeconds(timeUntilEnabling + 0.2f);
        settingsImage.SetActive(true);
    }
    
    IEnumerator EnableWinImage()
    {
        float timeUntilEnabling = startEndImage.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length;
        yield return new WaitForSeconds(timeUntilEnabling);
        FindObjectOfType<AdManagerUnityAds>().LevelHasEnded();
        FindObjectOfType<AdManagerUnityAds>().ShowInterstitialAd();
        winImage.SetActive(true);
        isAllowedToPlayMissileExplosionSound = false;
    }

    IEnumerator EnableLoseImage()
    {
        float timeUntilEnabling = startEndImage.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length;
        yield return new WaitForSeconds(timeUntilEnabling);
        FindObjectOfType<AdManagerUnityAds>().LevelHasEnded();
        FindObjectOfType<AdManagerUnityAds>().ShowInterstitialAd();
        loseImage.SetActive(true);
        isAllowedToPlayMissileExplosionSound = false;
    }

    public void DisableWinImage()
    {
        winImage.GetComponent<Animator>().SetTrigger("WinImageDisappear");
        StartCoroutine(DisableShopButton());
        StartCoroutine(DisableCoinsImage());
        StartCoroutine(DisableGetCoinsButton());
        StartCoroutine(DisableSettingsImage());
    }

    public void DisableLoseImage()
    {
        buttonsAudioSource.PlayOneShot(buttonsSFX);
        loseImage.GetComponent<Animator>().SetTrigger("LoseImageDisappear");
        StartCoroutine(DisableShopButton());
        StartCoroutine(DisableCoinsImage());
        StartCoroutine(DisableGetCoinsButton());
        StartCoroutine(DisableSettingsImage());
    }

    IEnumerator DisableShopButton()
    {
        float timeUntilDisabling;
        if (winImage.activeInHierarchy == true)
        {
            timeUntilDisabling = winImage.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length;
        }
        else
        {
            timeUntilDisabling = loseImage.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length;
        }
        yield return new WaitForSeconds(timeUntilDisabling - 0.8f);
        shopButton.GetComponent<Animator>().SetTrigger("ShopButtonDisappear");
    }

    IEnumerator DisableCoinsImage()
    {
        float timeUntilDisabling;
        if (winImage.activeInHierarchy == true)
        {
            timeUntilDisabling = winImage.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length;
        }
        else
        {
            timeUntilDisabling = loseImage.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length;
        }
        yield return new WaitForSeconds(timeUntilDisabling - 0.6f);
        coinsImage.GetComponent<Animator>().SetTrigger("CoinsImageDisappear");
    }

    IEnumerator DisableGetCoinsButton()
    {
        float timeUntilDisabling;
        if (winImage.activeInHierarchy == true)
        {
            timeUntilDisabling = winImage.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length;
        }
        else
        {
            timeUntilDisabling = loseImage.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length;
        }
        yield return new WaitForSeconds(timeUntilDisabling - 0.4f);
        getCoinsButton.GetComponent<Animator>().SetTrigger("GetCoinsButtonDisappear");
    }

    IEnumerator DisableSettingsImage()
    {
        float timeUntilDisabling;
        if (winImage.activeInHierarchy == true)
        {
            timeUntilDisabling = winImage.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length;
        }
        else
        {
            timeUntilDisabling = loseImage.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length;
        }
        yield return new WaitForSeconds(timeUntilDisabling - 0.8f);
        settingsImage.GetComponent<Animator>().SetTrigger("SettingsImageDisappear");
    }

    public void EnableShopImage()
    {
        shopImage.SetActive(true);
    }

    public void EnableNotEnoughCoinsImage()
    {
        notEnoughCoinsImage.SetActive(true);
    }

    public void DisableNotEnoughCoinsImage()
    {
        notEnoughCoinsImage.GetComponent<Animator>().SetTrigger("NotEnoughCoinsImageDisappear");
        StartCoroutine(disableNotEnoughCoinsImage());
    }

    IEnumerator disableNotEnoughCoinsImage()
    {
        float timeUntilDisabling = notEnoughCoinsImage.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length;
        yield return new WaitForSeconds(timeUntilDisabling);
        notEnoughCoinsImage.SetActive(false);
        FindObjectOfType<Buttons>().isAllowedToPlayButtonsSFX = true;
    }

    public void EnableShowingAdImage()
    {
        showingAdImage.SetActive(true);
    }

    public void DisableShowingAdImage()
    {
        showingAdImage.SetActive(false);
    }
}
