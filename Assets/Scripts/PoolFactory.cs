using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public interface IPooledObject<T>
{
    IObjectPool<T> pool { get; set; }
}

public interface IObjectPool<T>
{
    void Return(T obj);
    void Return(T obj, float delay);
}

public class PoolFactory<T> : MonoBehaviour, IObjectPool<T> where T : MonoBehaviour, IPooledObject<T>
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
        T item;
        
        if(pool.Count > 0)
        {
            item = pool.Dequeue();
            item.gameObject.SetActive(true);
        }
        else
        {
            item = factory.Create();
            item.transform.parent = parent;
            item.pool = this;
        }
        
        return item;
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
