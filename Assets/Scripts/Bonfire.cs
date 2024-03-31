using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonfire : MonoBehaviour
{
    [SerializeField] float burnTime = 10f;
    [SerializeField] float pauseTime = 5f;
    [SerializeField] bool isBurning = true;
    float timer;

    ParticleSystem fireParticleSystem;
    MeshCollider bonfireMeshCollider;

    private void Awake()
    {
        fireParticleSystem = GetComponent<ParticleSystem>();
        bonfireMeshCollider = GetComponentInParent<MeshCollider>();
        bonfireMeshCollider.isTrigger = true;
        timer = burnTime;
    }

    private void Update()
    {
        if (isBurning == true)
        {
            timer = timer - Time.deltaTime;
            if (timer <= Mathf.Epsilon)
            {
                isBurning = false;
                timer = pauseTime;
                bonfireMeshCollider.isTrigger = false;
                var emissionModule = fireParticleSystem.emission;
                emissionModule.enabled = false;
            }
        }
        else
        {
            timer = timer - Time.deltaTime;
            if (timer <= Mathf.Epsilon)
            {
                isBurning = true;
                timer = burnTime;
                bonfireMeshCollider.isTrigger = true;
                var emissionModule = fireParticleSystem.emission;
                emissionModule.enabled = true;
            }
        }
    }
}
