using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Bullet : MonoBehaviour, IPoolObject<Bullet>
{
    public IObjectPool<Bullet> pool { get; set; }
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
        pool.Return(this);
    }
    
    void Update()
    {
        transform.position += transform.forward * moveSpeed;
    }

}