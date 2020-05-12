using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Rifle : MonoBehaviour, IGun
{
    [SerializeField] private int bulletCount = 3;
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
        StartCoroutine(FireQueue());
    }

    private IEnumerator FireQueue()
    {
        for (int i = 0; i < bulletCount; i++)
        {
            GameObject bullet = bulletFactPool.Spawn().gameObject;
            bullet.transform.position = Barrel.position;
            bullet.transform.rotation = Barrel.rotation;
            yield return new WaitForSeconds(0.2f);
        }
    }
}
