using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDistructor : MonoBehaviour
{
    [SerializeField] float timeUntilSelfDistruction = 5f;

    private void Awake()
    {
        Destroy(gameObject, timeUntilSelfDistruction);
    }
}
