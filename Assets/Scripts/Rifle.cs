using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Rifle : MonoBehaviour, IGun
{
    [SerializeField] private int bulletCount = 3;
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
        StartCoroutine(FireQueue());
    }

    private IEnumerator FireQueue()
    {
        for (int i = 0; i < bulletCount; i++)
        {
            GameObject bullet = Pool.GetObject("Bullet");
            bullet.transform.position = Barrel.position;
            bullet.transform.rotation = Barrel.rotation;
            bullet.GetComponent<PooledObject>().DelayedReturnToPool(5f);
            yield return new WaitForSeconds(0.2f);
        }
    }
}
