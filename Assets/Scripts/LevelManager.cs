using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI levelCompletedText;
    [SerializeField] TextMeshProUGUI levelFailedText;

    int currentSceneIndex;

    [SerializeField] bool levelLoaded = false;

    private void Awake()
    {
        SingletonPattern();
        ManageWinAndLoseImageTexts();
    }

    private void Update()
    {
        LoadSavedLevel();
    }

    private void LoadSavedLevel()
    {
        if (levelLoaded == false)
        {
            levelLoaded = true;
            int level = PlayerPrefs.GetInt("LevelMvrhthufkad");
            SceneManager.LoadScene(level);
        }
    }

    private void ManageWinAndLoseImageTexts()
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int timesGameCompleted = PlayerPrefs.GetInt("TimesGameCompletedMvrhthufkad");
        int sceneCount = SceneManager.sceneCountInBuildSettings - 2;
        int playingLevel = (timesGameCompleted * sceneCount) + (currentSceneIndex + 1);
        levelCompletedText.text = "Level " + playingLevel.ToString() + " completed";
        levelFailedText.text = "Level " + playingLevel.ToString() + " failed";
    }

    private void SingletonPattern()
    {
        int numberOfLevelManagers = FindObjectsOfType<LevelManager>().Length;
        if (numberOfLevelManagers > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}
