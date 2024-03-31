using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Vector3 offset;
    [SerializeField] float smoothSpeed = 0.125f;

    GameObject rocket;

    private void Start() // Because the rocket needs to be instantiated first
    {
        rocket = GameObject.FindGameObjectWithTag("Rocket");
        gameObject.transform.position = rocket.transform.position;
    }

    private void FixedUpdate()
    {
        Vector3 desiredPos = rocket.transform.position + offset;
        Vector3 smoothedPos = Vector3.Lerp(gameObject.transform.position, desiredPos, smoothSpeed * Time.deltaTime);
        gameObject.transform.position = smoothedPos;
    }
}
