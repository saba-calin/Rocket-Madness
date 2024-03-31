using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketParentPosition : MonoBehaviour
{
    Vector3 spawningPos;

    private void Awake()
    {
        spawningPos = gameObject.transform.position;
        int spawningRocket = PlayerPrefs.GetInt("SelectedRocketMvrhthufkad");
        gameObject.transform.position = AddOffsetToSpawningPos(spawningRocket);
        FindObjectOfType<RocketSpawner>().InstantiateRocket(spawningRocket);
    }

    private Vector3 AddOffsetToSpawningPos(int spawningRocket)
    {
        switch (spawningRocket)
        {
            case 0:
                spawningPos.z = spawningPos.z + 0.129f;
                break;
            case 1:
                spawningPos.x = spawningPos.x - 0.05f;
                spawningPos.z = spawningPos.z - 0.14f;
                break;
            case 2:
                spawningPos.x = spawningPos.x - 0.2f;
                spawningPos.z = spawningPos.z - 0.03f;
                break;
            case 3:
                spawningPos.x = spawningPos.x - 0.3f;
                spawningPos.z = spawningPos.z + 0.07f;
                break;
            case 4:
                spawningPos.x = spawningPos.x - 0.25f;
                spawningPos.z = spawningPos.z - 0.1f;
                break;
            case 5:
                spawningPos.x = spawningPos.x - 0.15f;
                spawningPos.z = spawningPos.z + 0.12f;
                break;
            case 6:
                spawningPos.x = spawningPos.x - 0.2f;
                spawningPos.z = spawningPos.z - 0.03f;
                break;
            case 7:
                spawningPos.x = spawningPos.x - 0.15f;
                spawningPos.z = spawningPos.z + 0.12f;
                break;
            case 8:
                spawningPos.x = spawningPos.x - 0.3f;
                spawningPos.z = spawningPos.z + 0.07f;
                break;
            case 9:
                spawningPos.z = spawningPos.z + 0.129f;
                break;
            case 10:
                spawningPos.x = spawningPos.x - 0.25f;
                spawningPos.z = spawningPos.z - 0.1f;
                break;
            case 11:
                spawningPos.x = spawningPos.x - 0.05f;
                spawningPos.z = spawningPos.z - 0.14f;
                break;
            case 12:
                spawningPos.z = spawningPos.z + 0.129f;
                break;
            case 13:
                spawningPos.x = spawningPos.x - 0.3f;
                spawningPos.z = spawningPos.z + 0.07f;
                break;
            case 14:
                spawningPos.x = spawningPos.x - 0.05f;
                spawningPos.z = spawningPos.z - 0.14f;
                break;
            case 15:
                spawningPos.x = spawningPos.x - 0.25f;
                spawningPos.z = spawningPos.z - 0.1f;
                break;
            case 16:
                spawningPos.x = spawningPos.x - 0.15f;
                spawningPos.z = spawningPos.z + 0.12f;
                break;
            case 17:
                spawningPos.x = spawningPos.x - 0.2f;
                spawningPos.z = spawningPos.z - 0.03f;
                break;
            case 18:
                spawningPos.x = spawningPos.x - 0.05f;
                spawningPos.z = spawningPos.z - 0.14f;
                break;
            case 19:
                spawningPos.x = spawningPos.x - 0.15f;
                spawningPos.z = spawningPos.z + 0.12f;
                break;
            case 20:
                spawningPos.z = spawningPos.z + 0.129f;
                break;
            case 21:
                spawningPos.x = spawningPos.x - 0.3f;
                spawningPos.z = spawningPos.z + 0.07f;
                break;
            case 22:
                spawningPos.x = spawningPos.x - 0.2f;
                spawningPos.z = spawningPos.z - 0.03f;
                break;
            case 23:
                spawningPos.x = spawningPos.x - 0.25f;
                spawningPos.z = spawningPos.z - 0.1f;
                break;
            case 24:
                spawningPos.x = spawningPos.x - 0.2f;
                spawningPos.z = spawningPos.z - 0.03f;
                break;
            case 25:
                spawningPos.z = spawningPos.z + 0.129f;
                break;
            case 26:
                spawningPos.x = spawningPos.x - 0.05f;
                spawningPos.z = spawningPos.z - 0.14f;
                break;
            case 27:
                spawningPos.x = spawningPos.x - 0.25f;
                spawningPos.z = spawningPos.z - 0.1f;
                break;
            case 28:
                spawningPos.x = spawningPos.x - 0.3f;
                spawningPos.z = spawningPos.z + 0.07f;
                break;
            case 29:
                spawningPos.x = spawningPos.x - 0.15f;
                spawningPos.z = spawningPos.z + 0.12f;
                break;
            case 30:
                spawningPos.x = spawningPos.x - 0.45f;
                spawningPos.z = spawningPos.z + 1f;
                break;
            case 31:
                spawningPos.x = spawningPos.x - 0.45f;
                spawningPos.z = spawningPos.z + 1f;
                break;
            case 32:
                spawningPos.x = spawningPos.x - 0.45f;
                spawningPos.z = spawningPos.z + 1f;
                break;
            case 33:
                spawningPos.x = spawningPos.x - 0.45f;
                spawningPos.z = spawningPos.z + 1f;
                break;
            case 34:
                spawningPos.x = spawningPos.x - 0.45f;
                spawningPos.z = spawningPos.z + 1f;
                break;
            case 35:
                spawningPos.x = spawningPos.x - 0.45f;
                spawningPos.z = spawningPos.z + 1f;
                break;
        }
        return spawningPos;
    }
}
