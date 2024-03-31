using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketSpawner : MonoBehaviour
{
    [SerializeField] GameObject[] rocketPrefabs;
    [SerializeField] GameObject rocketParent;

    public void InstantiateRocket(int spawningRocket)
    {
        Instantiate(rocketPrefabs[spawningRocket], rocketParent.transform.position, rocketPrefabs[spawningRocket].transform.rotation, rocketParent.transform);
    }
}
