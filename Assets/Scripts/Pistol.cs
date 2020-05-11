using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Pistol : MonoBehaviour, IGun
{
    [SerializeField] private Transform barrel = null;
    public Transform Barrel { get { return barrel; } }
    public ObjectPool Pool { get; private set; }

    [Inject]
    public void Construct(ObjectPool pool)
    {
        Pool = pool;
    }

    public void Fire()
    {
        GameObject bullet = Pool.GetObject("Bullet");
        bullet.transform.position = Barrel.position;
        bullet.transform.rotation = Barrel.rotation;
        bullet.GetComponent<PooledObject>().DelayedReturnToPool(5f);
    }
}
