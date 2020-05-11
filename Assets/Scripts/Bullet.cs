using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;

    private AudioManager audioManager = null;

    [Inject]
    public void Construct(AudioManager manager)
    {
        audioManager = manager;
    }
    private void OnTriggerEnter(Collider other)
    {
        audioManager.PlaySound("Play hit sound!");
        GetComponent<PooledObject>().DelayedReturnToPool(0);
    }
    
    void Update()
    {
        transform.position += transform.forward * moveSpeed;
    }
}
