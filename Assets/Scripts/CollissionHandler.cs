using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollissionHandler : MonoBehaviour
{
    GameSession gameSession;
    Fuel fuel;

    private void Awake()
    {
        gameSession = FindObjectOfType<GameSession>();
        fuel = FindObjectOfType<Fuel>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Obstacle":
                gameSession.StartDeathSequence();
                fuel.isAllowedToRefuel = false;
                break;
            case "Missile Spawner":
                gameSession.StartDeathSequence();
                fuel.isAllowedToRefuel = false;
                break;
            case "Start Platform":
                if (fuel.playerHasFuel == false)
                {
                    gameSession.StartDeathSequence();
                }
                fuel.isAllowedToRefuel = false;
                break;
            case "Win Platform":
                gameSession.StartWinSequence();
                fuel.isAllowedToRefuel = false;
                break;
            case "Refuel Platform":
                if (gameSession.state == GameSession.State.Alive)
                {
                    fuel.isAllowedToRefuel = true;
                    fuel.playerHasFuel = true;
                }
                break;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Refuel Platform":
                fuel.isAllowedToRefuel = false;
                break;
        }
    }

    private void OnParticleCollision(GameObject other)
    {
        gameSession.StartDeathSequence();
        fuel.isAllowedToRefuel = false;
    }
}
