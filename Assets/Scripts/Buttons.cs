using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Buttons : MonoBehaviour
{
    [SerializeField] GameObject winImage;
    [SerializeField] GameObject loseImage;
    [SerializeField] GameObject shopImage;
    [SerializeField] GameObject startEndImage;
    [SerializeField] GameObject unlockAllButton;

    int currentSceneIndex;

    UIManager uIManager;

    AudioSource buttonsAudioSource;
    [SerializeField] AudioClip buttonsSFX;
    [SerializeField] AudioClip warningSFX;
    [SerializeField] AudioClip unlockedRocketSFX;
    [HideInInspector] public bool isAllowedToPlayButtonsSFX = true;
    bool dropdownIsAllowedToPlaySFX = false;

    [SerializeField] string TermsOfServiceURL = "https://policies.google.com/terms?hl=en-US";
    [SerializeField] string PrivacyPolicyURL = "https://policies.google.com/privacy?hl=en-US";

    [SerializeField] TMP_Dropdown qualitySettingsDropdown;

    private void Awake()
    {
        if (SceneManager.GetActiveScene().name == "Settings")
        {
            qualitySettingsDropdown.value = PlayerPrefs.GetInt("QualitySettingsMvrhthufkad");
        }
    }

    private void Start()
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        uIManager = FindObjectOfType<UIManager>();
        buttonsAudioSource = GameObject.FindGameObjectWithTag("Buttons Audio Source").GetComponent<AudioSource>();
    }

    public void NextLevel()
    {
        PlayButtonsSFX(buttonsSFX);
        uIManager.DisableWinImage();
        StartCoroutine(LoadNextLevel());
    }

    IEnumerator LoadNextLevel()
    {
        float timeUntilLoadingNextScene = winImage.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length;
        yield return new WaitForSeconds(timeUntilLoadingNextScene);
        CheckIfLastLevelIsCompletedWhenCompleted();
    }

    public void PlayAgain()
    {
        PlayButtonsSFX(buttonsSFX);
        uIManager.DisableWinImage();
        StartCoroutine(playAgain());
    }

    IEnumerator playAgain()
    {
        float timeUntilReloadingLevel = winImage.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length;
        yield return new WaitForSeconds(timeUntilReloadingLevel);
        SceneManager.LoadScene(currentSceneIndex);
    }

    public void RetryLevel()
    {
       if (isAllowedToPlayButtonsSFX == true) // IDK why I have to do this to make it work
       {                                      // It would play the SFX whenever you press on it without the if statement
            PlayButtonsSFX(buttonsSFX);       // And this seem to be the only fix
            uIManager.DisableLoseImage();     // It's not my fault for the first time!
            StartCoroutine(retryLevel());
       }
    }

    IEnumerator retryLevel()
    {
        float timeUntilReloadingLevel = loseImage.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length;
        yield return new WaitForSeconds(timeUntilReloadingLevel);
        SceneManager.LoadScene(currentSceneIndex);
    }

    public void SkipLevel()
    {
        FindObjectOfType<AdManagerUnityAds>().option = 4;
        FindObjectOfType<AdManagerUnityAds>().ShowRewardedAd();
    }

    public IEnumerator skipLevel() // This is called from the AdManager
    {
        float timeUntilLoadingNextLevel = loseImage.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length;
        yield return new WaitForSeconds(timeUntilLoadingNextLevel);
        CheckIfLastLevelIsCompletedWhenSkipped();
    }

    public void Shop()
    {
        PlayButtonsSFX(buttonsSFX);
        uIManager.EnableShopImage();
        StartCoroutine(LoadShop());
    }

    IEnumerator LoadShop()
    {
        float timeUntilLoadingShop = shopImage.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length;
        yield return new WaitForSeconds(timeUntilLoadingShop);
        SceneManager.LoadScene("Shop");
    }

    public void Back()
    {
        PlayButtonsSFX(buttonsSFX);
        uIManager.EnableStartEndImage();
        StartCoroutine(ExitShop());
    }

    IEnumerator ExitShop()
    {
        float timeUntilExiting = startEndImage.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length;
        yield return new WaitForSeconds(timeUntilExiting);
        int sceneToLoad = PlayerPrefs.GetInt("LevelMvrhthufkad");
        SceneManager.LoadScene(sceneToLoad);
    }

    private void CheckIfLastLevelIsCompletedWhenCompleted()
    {
        int lastLevelIndex = SceneManager.sceneCountInBuildSettings - 3;
        if (currentSceneIndex == lastLevelIndex)
        {
            SceneManager.LoadScene(0); // Saving how many times the game has been completed is done in the GameSession script
        }
        else
        {
            SceneManager.LoadScene(currentSceneIndex + 1);
        }
    }

    private void CheckIfLastLevelIsCompletedWhenSkipped()
    {
        int lastLevelIndex = SceneManager.sceneCountInBuildSettings - 3;
        if (currentSceneIndex != lastLevelIndex)
        {
            PlayerPrefs.SetInt("LevelMvrhthufkad", currentSceneIndex + 1);
            SceneManager.LoadScene(currentSceneIndex + 1);
        }
        else
        {
            PlayerPrefs.SetInt("LevelMvrhthufkad", 0);
            int timesGameCompleted = PlayerPrefs.GetInt("TimesGameCompletedMvrhthufkad");
            timesGameCompleted++;
            PlayerPrefs.SetInt("TimesGameCompletedMvrhthufkad", timesGameCompleted);
            SceneManager.LoadScene(0);
        }
    }

    private void PlayButtonsSFX(AudioClip audioClip)
    {
        if (isAllowedToPlayButtonsSFX == true)
        {
            isAllowedToPlayButtonsSFX = false;
            buttonsAudioSource.PlayOneShot(audioClip);
        }
    }

    public void NotEnoughCoinsCloseButton()
    {
        PlayButtonsSFX(buttonsSFX);
        uIManager.DisableNotEnoughCoinsImage();
    }

    public void GetCoins()
    {
        FindObjectOfType<AdManagerUnityAds>().option = 3;
        FindObjectOfType<AdManagerUnityAds>().ShowRewardedAd();
    }

    public void UnlockAll()
    {
        buttonsAudioSource.PlayOneShot(unlockedRocketSFX);
        ShopCoins[] shopCoins = FindObjectsOfType<ShopCoins>();
        ShopAds[] shopAds = FindObjectsOfType<ShopAds>();
        foreach (ShopCoins shopcoin in shopCoins)
        {
            shopcoin.UnlockRocket();
            shopcoin.DisableEverything();
            shopcoin.ManageButtonsAndTexts();
        }
        foreach (ShopAds shopAd in shopAds)
        {
            shopAd.unlockRocket();
            shopAd.DisableEverything();
            shopAd.ManageButtonsAndTexts();
        }
        PlayerPrefs.SetInt("AlreadyUnlockedAllMvrhthufkad", 1);
    }

    public void TermsOfService()
    {
        Application.OpenURL(TermsOfServiceURL);
    }

    public void PrivacyPolicy()
    {
        Application.OpenURL(PrivacyPolicyURL);
    }

    public void IAccept()
    {
        PlayerPrefs.SetInt("AcceptedGDPRConsentMvrhthufkad", 1);
        uIManager.DisableGDPRConsentImage();
        Time.timeScale = 1;
    }

    public void SettingsButton()
    {
        PlayButtonsSFX(buttonsSFX);
        uIManager.EnableShopImage();
        StartCoroutine(settingsButton());
    }

    IEnumerator settingsButton()
    {
        float timeUntilLoadingScene = shopImage.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length;
        yield return new WaitForSeconds(timeUntilLoadingScene);
        SceneManager.LoadScene("Settings");
    }

    public void QualitySettingDropdown(int value)
    {
        if (dropdownIsAllowedToPlaySFX == true)         // Having to do this in order to avoid SFX being played on awake
        {                                               // because the value gets changed by default on awake
            buttonsAudioSource.PlayOneShot(buttonsSFX); 
        }
        else
        {
            StartCoroutine(EnableDropdownToPlaySFX());
        }
        PlayerPrefs.SetInt("QualitySettingsMvrhthufkad", qualitySettingsDropdown.value);
        // 0 --> Low
        // 1 --> Medium
        // 2 --> High
        // 3 --> Disabled
    }

    IEnumerator EnableDropdownToPlaySFX()
    {
        yield return new WaitForSeconds(0.1f);
        dropdownIsAllowedToPlaySFX = true;
    }
}
