using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// TODO: Remove this script before building
public class ROCKETKEYBOARDMOVENETDELETE : MonoBehaviour
{
    GameSession gameSession;
    RocketMovement rocketMovement;
    Rigidbody myRigidbody;

    int currentSceneIndex;

    private void Awake()
    {
        gameSession = FindObjectOfType<GameSession>();
        rocketMovement = GetComponent<RocketMovement>();
        myRigidbody = GetComponent<Rigidbody>();
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
    }

    private void FixedUpdate()
    {
        RotateLeft();
        RotateRight();
        if (FindObjectOfType<Fuel>().playerHasFuel == true)
        {
            Thrust();
        }
    }

    private void Update()
    {
        ManageLevels();
        RestartLevel();
        DeletePlayerPrefs();
    }

    private void RotateLeft()
    {
        myRigidbody.angularVelocity = Vector3.zero;
        if (Input.GetKey(KeyCode.A))
        {
            float rotationSpeed = rocketMovement.rotationSpeed;
            transform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime);
        }
    }

    private void RotateRight()
    {
        myRigidbody.angularVelocity = Vector3.zero;
        if (Input.GetKey(KeyCode.D))
        {
            float rotationSpeed = rocketMovement.rotationSpeed;
            transform.Rotate(0f, 0f, -rotationSpeed * Time.deltaTime);
        }
    }

    private void Thrust()
    {
        if (Input.GetKey(KeyCode.W))
        {
            float thrustForce = rocketMovement.thrustForce;
            myRigidbody.AddRelativeForce(0f, thrustForce * Time.deltaTime, 0f);
            gameSession.EnableThrustParticles();
            gameSession.EnableThrustSFX();
            FindObjectOfType<Fuel>().BurnFuel();
        }
        else
        {
            gameSession.DisableThrustParticles();
            gameSession.DisableThrustSFX();
        }
    }

    private void RestartLevel()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(currentSceneIndex);
        }
    }

    private void ManageLevels()
    {
        int level;
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            if (SceneManager.GetActiveScene().buildIndex == 0)
            {
                SceneManager.LoadScene("Level 70 Refurbished");
                PlayerPrefs.SetInt("LevelMvrhthufkad", 69);
            }
            else
            {
                PlayerPrefs.SetInt("LevelMvrhthufkad", currentSceneIndex - 1);
                level = PlayerPrefs.GetInt("LevelMvrhthufkad");
                SceneManager.LoadScene(level);
            }
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (SceneManager.GetActiveScene().buildIndex == 69)
            {
                SceneManager.LoadScene("Level 1 Refurbished");
                PlayerPrefs.SetInt("LevelMvrhthufkad", 0);
            }
            else
            {
                PlayerPrefs.SetInt("LevelMvrhthufkad", currentSceneIndex + 1);
                level = PlayerPrefs.GetInt("LevelMvrhthufkad");
                SceneManager.LoadScene(level);
            }
        }
    }

    private void DeletePlayerPrefs()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PlayerPrefs.DeleteAll();
        }
    }
}
