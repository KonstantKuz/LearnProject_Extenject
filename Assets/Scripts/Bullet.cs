using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Bullet : MonoBehaviour, IPoolObject<Bullet>
{
    public IObjectPool<Bullet> pool { get; set; }
    [SerializeField] private float moveSpeed = 5f;

    private IAudioManager audioManager = null;

    [Inject]
    public void Construct(IAudioManager manager)
    {
        audioManager = manager;
    }

    private void Start()
    {
        pool.Return(this, 10f);
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