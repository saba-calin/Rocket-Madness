using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSession : MonoBehaviour
{
    [Header("Particle related stuff")]
    [SerializeField] ParticleSystem deathParticleSystem;
    [SerializeField] ParticleSystem winParticleSystem;
    ParticleSystem[] thrustParticleSystems;
    Transform particleSystmesParent;

    [Header("Sound related stuff")]
    [SerializeField] AudioClip deathSFX;
    [SerializeField] AudioClip winSFX;
    AudioSource myAudioSource;
    AudioSource thrustAudioSource;

    [Header("Used for the end of the game")]
    [SerializeField] float timeBetweenDisplayingUI = 0.5f;
    UIManager uIManager;
    int currentSceneIndex;

    [HideInInspector] public enum State { Dead, Alive, Winning };
    [HideInInspector] public State state = State.Alive;

    GameObject rocket;

    private void Awake()
    {
        var directionalLight = GameObject.FindGameObjectWithTag("Directional Light").GetComponent<Light>();
        switch (PlayerPrefs.GetInt("QualitySettingsMvrhthufkad"))
        {
            case 0: // Low
                QualitySettings.antiAliasing = 2;
                QualitySettings.shadowProjection = ShadowProjection.StableFit;
                directionalLight.shadows = LightShadows.Hard;
                QualitySettings.shadowDistance = 70;
                QualitySettings.shadowResolution = ShadowResolution.Low;
                break;
            case 1: // Medium
                QualitySettings.antiAliasing = 4;
                QualitySettings.shadowProjection = ShadowProjection.StableFit;
                directionalLight.shadows = LightShadows.Hard;
                QualitySettings.shadowDistance = 70;
                QualitySettings.shadowResolution = ShadowResolution.Medium;
                break;
            case 2: // High
                QualitySettings.antiAliasing = 8;
                QualitySettings.shadowProjection = ShadowProjection.CloseFit;
                directionalLight.shadows = LightShadows.Soft;
                QualitySettings.shadowDistance = 100;
                QualitySettings.shadowResolution = ShadowResolution.High;
                break;
            case 3: // Disabled
                QualitySettings.antiAliasing = 0;
                QualitySettings.shadowProjection = ShadowProjection.StableFit;
                directionalLight.shadows = LightShadows.None;
                QualitySettings.shadowDistance = 0;
                QualitySettings.shadowResolution = ShadowResolution.Low;
                break;
        }
    }

    private void Start() // Because the rocket needs to be instantiated first, then get the thrust particles
    {
        rocket = GameObject.FindGameObjectWithTag("Rocket");
        particleSystmesParent = GameObject.Find("Particle Systems").transform;
        thrustParticleSystems = FindObjectOfType<RocketMovement>().GetComponentsInChildren<ParticleSystem>();
        myAudioSource = GetComponent<AudioSource>();
        thrustAudioSource = FindObjectOfType<ThrustSFX>().GetComponent<AudioSource>();
        uIManager = FindObjectOfType<UIManager>();
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
    }

    private void Update()
    {
        if (state != State.Alive)
        {
            DisableThrustParticles();
            DisableThrustSFX();
        }
    }

    public void EnableThrustParticles()
    {
        foreach (ParticleSystem thrustParticleSystem in thrustParticleSystems)
        {
            var emissionModule = thrustParticleSystem.emission;
            emissionModule.enabled = true;
        }
    }

    public void DisableThrustParticles()
    {
        foreach (ParticleSystem thrustParticleSystem in thrustParticleSystems)
        {
            var emissionModule = thrustParticleSystem.emission;
            emissionModule.enabled = false;
        }
    }

    public void EnableThrustSFX()
    {
        thrustAudioSource.volume = 1f;
    }

    public void DisableThrustSFX()
    {
        thrustAudioSource.volume = 0f;
    }

    public void StartDeathSequence()
    {
        if (state == State.Alive)
        {
            state = State.Dead;
            InstantiateDeathOrWinParticleSystem(deathParticleSystem);
            myAudioSource.PlayOneShot(deathSFX);
            StartCoroutine(DeathSequence());
        }
    }

    public void StartWinSequence()
    {
        if (state == State.Alive)
        {
            state = State.Winning;
            InstantiateDeathOrWinParticleSystem(winParticleSystem);
            myAudioSource.PlayOneShot(winSFX);
            CheckIfLastLevelIsCompleted();
            StartCoroutine(WinSequence());
        }
    }

    private void InstantiateDeathOrWinParticleSystem(ParticleSystem deathOrWinParticleSystem)
    {
        Vector3 deathOrWinParticleSystemSpawnPos = rocket.transform.position;
        if (PlayerPrefs.GetInt("SelectedRocketMvrhthufkad") >= 30)
        {
            deathOrWinParticleSystemSpawnPos.x = deathOrWinParticleSystemSpawnPos.x + 0.35f;
            deathOrWinParticleSystemSpawnPos.z = deathOrWinParticleSystemSpawnPos.z - 1.1f;
        }
        Instantiate(deathOrWinParticleSystem, deathOrWinParticleSystemSpawnPos, Quaternion.identity).transform.parent = particleSystmesParent.transform;
    }

    IEnumerator DeathSequence()
    {
        yield return new WaitForSeconds(timeBetweenDisplayingUI);
        uIManager.DisplayLoseUI();
    }

    IEnumerator WinSequence()
    {
        yield return new WaitForSeconds(timeBetweenDisplayingUI);
        uIManager.DisplayWinUI();
    }

    private void CheckIfLastLevelIsCompleted()
    {
        int lastLevelIndex = SceneManager.sceneCountInBuildSettings - 3;
        if (currentSceneIndex != lastLevelIndex)
        {
            PlayerPrefs.SetInt("LevelMvrhthufkad", currentSceneIndex + 1);
        }
        else
        {
            PlayerPrefs.SetInt("LevelMvrhthufkad", 0);
            int timesGameCompleted = PlayerPrefs.GetInt("TimesGameCompletedMvrhthufkad");
            timesGameCompleted++;
            PlayerPrefs.SetInt("TimesGameCompletedMvrhthufkad", timesGameCompleted);
        }
    }
}
