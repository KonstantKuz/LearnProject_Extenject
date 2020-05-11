using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;

    private void OnTriggerEnter(Collider other)
    {
        GetComponent<PooledObject>().DelayedReturnToPool(0);
    }

    void Update()
    {
        transform.position += transform.forward * moveSpeed;
    }
}
