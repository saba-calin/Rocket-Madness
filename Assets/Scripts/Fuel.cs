using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Fuel : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI fuelText;
    [SerializeField] int fuel = 50;
    [SerializeField] float fuelConsumptionTime = 0.2f;
    [SerializeField] float fuelRefuelTime = 0.1f;
    float fuelConsumptionTimer;
    float fuelRefuelTimer;
    int maxFuel;

    [HideInInspector] public bool playerHasFuel = true;
    [HideInInspector] public bool isAllowedToRefuel = false;

    GameSession gameSession;

    private void Awake()
    {
        CheckForSpecialAbilities();
        fuelText.text = fuel.ToString();
        fuelConsumptionTimer = fuelConsumptionTime;
        fuelRefuelTimer = fuelRefuelTime;
        maxFuel = fuel;
        gameSession = FindObjectOfType<GameSession>();
    }

    private void Update()
    {
        if (playerHasFuel == false)
        {
            gameSession.DisableThrustParticles();
            gameSession.DisableThrustSFX();
        }
        Refuel();
    }

    public void BurnFuel()
    {
        fuelConsumptionTimer = fuelConsumptionTimer - Time.deltaTime;
        if (fuelConsumptionTimer <= Mathf.Epsilon)
        {
            fuelConsumptionTimer = fuelConsumptionTime;
            if (fuel > 0)
            {
                fuel--;
                fuelText.text = fuel.ToString();
            }
            else
            {
                playerHasFuel = false;
            }
        }
    }

    private void Refuel()
    {
        if (isAllowedToRefuel == true)
        {
            fuelRefuelTimer = fuelRefuelTimer - Time.deltaTime;
            if (fuelRefuelTimer <= Mathf.Epsilon)
            {
                fuelRefuelTimer = fuelRefuelTime;
                if (fuel < maxFuel)
                {
                    fuel++;
                    fuelText.text = fuel.ToString();
                }
            }
        }
    }

    private void CheckForSpecialAbilities()
    {
        int selectedRocket = PlayerPrefs.GetInt("SelectedRocketMvrhthufkad");
        switch (selectedRocket)
        {
            case 1:
                fuel = fuel + 1;
                break;
            case 5:
                fuel = fuel + 2;
                break;
            case 6:
                fuelConsumptionTime = fuelConsumptionTime + ((2f / 100f) * fuelConsumptionTime);
                break;
            case 8:
                fuel = fuel + 3;
                break;
            case 11:
                fuelConsumptionTime = fuelConsumptionTime + ((5f / 100f) * fuelConsumptionTime);
                break;
            case 12:
                fuel = fuel + 2;
                fuelConsumptionTime = fuelConsumptionTime + ((5f / 100f) * fuelConsumptionTime);
                break;
            case 18:
                fuel = fuel + 5;
                break;
            case 20:
                fuelConsumptionTime = fuelConsumptionTime + ((10f / 100f) * fuelConsumptionTime);
                break;
            case 22:
                fuel = fuel + 5;
                fuelConsumptionTime = fuelConsumptionTime + ((10f / 100f) * fuelConsumptionTime);
                break;
            case 26:
                fuelConsumptionTime = fuelConsumptionTime + ((15f / 100f) * fuelConsumptionTime);
                break;
            case 27:
                fuel = fuel + 7;
                break;
            case 30:
                fuelConsumptionTime = fuelConsumptionTime + ((20f / 100f) * fuelConsumptionTime);
                break;
            case 32:
                fuel = fuel + 7;
                fuelConsumptionTime = fuelConsumptionTime + ((20f / 100f) * fuelConsumptionTime);
                break;
            case 33:
                fuel = fuel + 12;
                break;
            case 35:
                fuel = fuel + 10;
                fuelConsumptionTime = fuelConsumptionTime + ((20f / 100f) * fuelConsumptionTime);
                break;
        }
    }
}
