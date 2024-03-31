using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    [HideInInspector] public float xSpeed;
    [HideInInspector] public float ySpeed;

    [SerializeField] float ySpawningOffset = 0.1f;
    [SerializeField] float xSpawnginOffset = 0.1f;
    [SerializeField] ParticleSystem missileExplosionVFX;
    [SerializeField] AudioClip missileExplosionSFX;
    [SerializeField] float maxDistanceToRocket = 25f;

    float distanceToRocket;

    Transform particleSystemsParent;
    Vector3 spawningPos;
    GameObject rocket;
    Rigidbody myRigidbody;
    GameSession gameSession;
    UIManager uIManager;
    AudioSource missileExplosionSoundPlayer;

    private void Awake()
    {
        rocket = GameObject.FindGameObjectWithTag("Rocket");
        gameSession = FindObjectOfType<GameSession>();
        uIManager = FindObjectOfType<UIManager>();
        myRigidbody = GetComponent<Rigidbody>();
        particleSystemsParent = GameObject.Find("Particle Systems").transform;
        spawningPos = gameObject.transform.position;
        missileExplosionSoundPlayer = GameObject.FindGameObjectWithTag("Missile Explosion Sound Player").GetComponent<AudioSource>();
    }

    private void FixedUpdate()
    {
        if (ySpeed == 0)
        {
            myRigidbody.velocity = new Vector3(xSpeed * Time.deltaTime, 0f, 0f);
        }
        else
        {
            myRigidbody.velocity = new Vector3(0f, ySpeed * Time.deltaTime, 0f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        CalculateSpawnPosAndDistToRocket();
        switch (other.gameObject.tag)
        {
            case "Rocket":
                Instantiate(missileExplosionVFX, spawningPos, Quaternion.identity).transform.parent = particleSystemsParent.transform;
                if (gameSession.state != GameSession.State.Alive && distanceToRocket <= maxDistanceToRocket && uIManager.isAllowedToPlayMissileExplosionSound == true)
                {
                    missileExplosionSoundPlayer.PlayOneShot(missileExplosionSFX);
                }
                gameSession.StartDeathSequence();
                Destroy(gameObject);
                break;
            case "Obstacle":
                Instantiate(missileExplosionVFX, spawningPos, Quaternion.identity).transform.parent = particleSystemsParent.transform;
                if (uIManager.isAllowedToPlayMissileExplosionSound == true && distanceToRocket <= maxDistanceToRocket)
                {
                    missileExplosionSoundPlayer.PlayOneShot(missileExplosionSFX);
                }
                Destroy(gameObject);
                break;
            case "Missile Spawner":
                // Do nothing
                break;
            case "Start Platform":
                Instantiate(missileExplosionVFX, spawningPos, Quaternion.identity).transform.parent = particleSystemsParent.transform;
                if (uIManager.isAllowedToPlayMissileExplosionSound == true && distanceToRocket <= maxDistanceToRocket)
                {
                    missileExplosionSoundPlayer.PlayOneShot(missileExplosionSFX);
                }
                Destroy(gameObject);
                break;
            case "Win Platform":
                Instantiate(missileExplosionVFX, spawningPos, Quaternion.identity).transform.parent = particleSystemsParent.transform;
                if (uIManager.isAllowedToPlayMissileExplosionSound == true && distanceToRocket <= maxDistanceToRocket)
                {
                    missileExplosionSoundPlayer.PlayOneShot(missileExplosionSFX);
                }
                Destroy(gameObject);
                break;
            case "Refuel Platform":
                Instantiate(missileExplosionVFX, spawningPos, Quaternion.identity).transform.parent = particleSystemsParent.transform;
                if (uIManager.isAllowedToPlayMissileExplosionSound == true && distanceToRocket <= maxDistanceToRocket)
                {
                    missileExplosionSoundPlayer.PlayOneShot(missileExplosionSFX);
                }
                Destroy(gameObject);
                break;
        }
    }

    private void CalculateSpawnPosAndDistToRocket()
    {
        spawningPos = gameObject.transform.position;
        if (ySpeed == 0)
        {
            spawningPos.y += ySpawningOffset;
        }
        else
        {
            spawningPos.x += xSpawnginOffset;
        }
        distanceToRocket = Vector3.Distance(gameObject.transform.position, rocket.transform.position);
    }
}
