using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketMovement : MonoBehaviour
{
    [SerializeField] public float rotationSpeed = 5f; // TODO: Delete public before building
    [SerializeField] public float thrustForce = 10f;  // TODO: Delete public before building

    bool isAllowedToRotateLeft = false;
    bool isAllowedToRotateRight = false;
    bool isAllowedToThrust = false;

    Rigidbody myRigidbody;
    [HideInInspector] public GameSession gameSession;  // set to public so that the keyboard controller script can access it

    private void Awake()
    {
        myRigidbody = GetComponent<Rigidbody>();
        gameSession = FindObjectOfType<GameSession>();
    }

    private void FixedUpdate()
    {
        myRigidbody.angularVelocity = Vector3.zero;
        if (gameSession.state == GameSession.State.Alive)
        {
            if (GetComponent<Fuel>().playerHasFuel == true)
            {
                Thrust(isAllowedToThrust);
            }
            RotateLeft(isAllowedToRotateLeft);
            RotateRight(isAllowedToRotateRight);
        }
        else
        {
            myRigidbody.constraints = RigidbodyConstraints.None;
        }
    }

    private void RotateLeft(bool left)
    {
        if (left == true)
        {
            transform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime);
        }
    }

    private void RotateRight(bool right)
    {
        if (right == true)
        {
            transform.Rotate(0f, 0f, -rotationSpeed * Time.deltaTime);
        }
    }

    private void Thrust(bool up)
    {
        if (up == true)
        {
            myRigidbody.AddRelativeForce(0f, thrustForce * Time.deltaTime, 0f);
            gameSession.EnableThrustParticles();
            gameSession.EnableThrustSFX();
            GetComponent<Fuel>().BurnFuel();
        }
        else
        {
            // TODO uncomment the following 2 lines when reenabling the the Buttons Canvas

            //gameSession.DisableThrustParticles();
            //gameSession.DisableThrustSFX();
        }
    }

    public void Left(bool move)
    {
        if (move == true)
        {
            isAllowedToRotateLeft = true;
        }
        else
        {
            isAllowedToRotateLeft = false;
        }
    }

    public void Right(bool move)
    {
        if (move == true)
        {
            isAllowedToRotateRight = true;
        }
        else
        {
            isAllowedToRotateRight = false;
        }
    }

    public void Up(bool move)
    {
        if (move == true)
        {
            isAllowedToThrust = true;
        }
        else
        {
            isAllowedToThrust = false;
        }
    }
}
