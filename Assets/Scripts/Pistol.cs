using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Pistol : MonoBehaviour, IGun
{
    [SerializeField] private Transform barrel = null;
    public Transform Barrel { get { return barrel; } }
    
    public BulletFactoryPool bulletPoolFactory;
    
    [Inject]
    public void Construct(BulletFactoryPool factoryPool)
    {
        bulletPoolFactory = factoryPool;
    }

    public void Fire()
    {
        Transform bullet = bulletPoolFactory.Spawn().transform;
        bullet.position = Barrel.position;
        bullet.rotation = Barrel.rotation;
    }
}
