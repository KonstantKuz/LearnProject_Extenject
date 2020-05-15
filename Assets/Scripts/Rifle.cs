using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Rifle : MonoBehaviour, IGun
{
    [SerializeField] private int bulletCount = 3;
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
        StartCoroutine(FireQueue());
    }

    private IEnumerator FireQueue()
    {
        for (int i = 0; i < bulletCount; i++)
        {
            Transform bullet = bulletPoolFactory.Spawn().transform;
            bullet.position = Barrel.position;
            bullet.rotation = Barrel.rotation;
            yield return new WaitForSeconds(0.2f);
        }
    }
}
