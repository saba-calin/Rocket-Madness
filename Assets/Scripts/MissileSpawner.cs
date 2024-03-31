using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileSpawner : MonoBehaviour
{
    [SerializeField] float delayBetweenSpawns = 1f;
    [SerializeField] float ySpawningOffset = 0.5f; // Used for horizontal instatiation
    [SerializeField] float xSpawningOffset = 0.5f; // Used for vertical instantiation
    [SerializeField] float xMissileSpeed = 5f;
    [SerializeField] float yMissileSpeed = 5f;
    [SerializeField] GameObject missilePrefab;
    [SerializeField] Transform missileParent;

    Coroutine spawnMissilesCoroutine;

    Vector3 spawningPos;

    private void Awake()
    {
        spawnMissilesCoroutine = StartCoroutine(SpawnMissiles());
        spawningPos = gameObject.transform.position;
        if (yMissileSpeed == 0)
        {
            spawningPos.y += ySpawningOffset;
        }
        else
        {
            spawningPos.x += xSpawningOffset;
        }
    }

    IEnumerator SpawnMissiles()
    {
        while (true)
        {
            yield return new WaitForSeconds(delayBetweenSpawns);
            GameObject missileInstance = Instantiate(missilePrefab, spawningPos, missilePrefab.transform.rotation);
            missileInstance.transform.parent = missileParent.transform;
            missileInstance.GetComponent<Missile>().xSpeed = xMissileSpeed;
            missileInstance.GetComponent<Missile>().ySpeed = yMissileSpeed;
            if (FindObjectOfType<GameSession>().state != GameSession.State.Alive)
            {
                StopCoroutine(spawnMissilesCoroutine);
            }
        }
    }
}
