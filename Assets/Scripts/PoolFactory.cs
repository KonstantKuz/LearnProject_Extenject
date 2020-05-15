using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PoolFactory<T> : MonoBehaviour, IObjectPool<T> where T : MonoBehaviour, IPoolObject<T>
 {
    [SerializeField] private GameObject prefab = null;
    [SerializeField] private Transform parent = null;
    public GameObject Prefab
    {
        get { return prefab; }
    }
    
    private Queue<T> pool = null;
    private Factory<T> factory = null;

    [Inject]
    public void Construct(Factory<T> factory)
    {
        this.factory = factory;
        pool = new Queue<T>();
        
    }
    public T Spawn()
    {
        T spawnObj;
        
        if(pool.Count > 0)
        {
            spawnObj = pool.Dequeue();
            spawnObj.gameObject.SetActive(true);
        }
        else
        {
            spawnObj = factory.Create();
            spawnObj.transform.parent = parent;
            spawnObj.gameObject.GetComponent<T>().pool = this;
        }
        
        return spawnObj;
    }
    
    public void Return(T item)
    {
        if(!pool.Contains(item))
        {
            item.gameObject.SetActive(false);
            pool.Enqueue(item);
        }
    }

    public void Return(T item, float delay)
    {
        StartCoroutine(DelayedReturn(item, delay));
    }

    private IEnumerator DelayedReturn(T item, float delay)
    {
        yield return new WaitForSeconds(delay);
        Return(item);
    }
    
    public class Factory<T> : PlaceholderFactory<T>
    {
    }
}
