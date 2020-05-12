using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class BulletFactoryPool : MonoBehaviour, IObjectPool<Bullet>
{
    [SerializeField] private GameObject prefab = null;
    [SerializeField] private Transform parent = null;
    public GameObject Prefab
    {
        get { return prefab; }
    }
    
    private Queue<Bullet> pool = null;
    private Factory<Bullet> factory = null;

    [Inject]
    public void Construct(Factory<Bullet> factory)
    {
        this.factory = factory;
        pool = new Queue<Bullet>();
        
    }
    public Bullet Spawn()
    {
        Bullet spawnObj;
        
        if(pool.Count > 0)
        {
            spawnObj = pool.Dequeue();
            spawnObj.gameObject.SetActive(true);
        }
        else
        {
            spawnObj = factory.Create();
            spawnObj.transform.parent = parent;
            spawnObj.gameObject.GetComponent<Bullet>().pool = this;
        }
        
        //pool.Enqueue(objToReturn);
        
        return spawnObj;
    }
    
    public void Return(Bullet item)
    {
        if(!pool.Contains(item))
        {
            item.gameObject.SetActive(false);
            pool.Enqueue(item);
        }
    }
    
    public class Factory<T> : PlaceholderFactory<T>
    {
    }
}

// public class BulletPoolComponent : MonoBehaviour, IPoolObject<Bullet>
// {
//     public IObjectPool<Bullet> pool { get; set; }
//
//     private Bullet componentToReturn;
//
//     private void Awake()
//     {
//         componentToReturn = GetComponent<Bullet>();
//     }
//
//     public void ReturnToPool()
//     {
//         pool.Return(componentToReturn);
//     }
// }
