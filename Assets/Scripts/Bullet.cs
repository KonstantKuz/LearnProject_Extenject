using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Bullet : MonoBehaviour, IPooledObject<Bullet>
{
    [SerializeField] private float moveSpeed = 5f;
    public IObjectPool<Bullet> pool { get; set; }
    private IAudioManager audioManager = null;

    [Inject]
    public void Construct(IAudioManager manager)
    {
        audioManager = manager;
    }

    private void Start()
    {
        pool.Return(this, 5f);
    }

    private void OnTriggerEnter(Collider other)
    {
        audioManager.PlaySound("bullet impact");
        pool.Return(this);
    }
    
    void Update()
    {
        transform.position += transform.forward * moveSpeed;
    }
}