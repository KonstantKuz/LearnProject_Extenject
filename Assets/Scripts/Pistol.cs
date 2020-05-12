using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Pistol : MonoBehaviour, IGun
{
    [SerializeField] private Transform barrel = null;
    public Transform Barrel { get { return barrel; } }
    
    public BulletFactoryPool bulletFactPool;
    
    [Inject]
    public void Construct(BulletFactoryPool factoryPool)
    {
        bulletFactPool = factoryPool;
    }

    public void Fire()
    {
        GameObject bullet = bulletFactPool.Spawn().gameObject;
        bullet.transform.position = Barrel.position;
        bullet.transform.rotation = Barrel.rotation;
    }
}
