using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Pool
{
    public Transform parent;
    public GameObject prefab;
    public string poolTag;
    public Queue<GameObject> pool;
}

public class ObjectPool : MonoBehaviour
{
    public List<Pool> pools;

    private Dictionary<string, Pool> poolDictionary;

    private void Awake()
    {
        InitializePooler();
    }

    private void InitializePooler()
    {
        poolDictionary = new Dictionary<string, Pool>();

        for (int i = 0; i < pools.Count; i++)
        {
            pools[i].pool = new Queue<GameObject>();
            poolDictionary.Add(pools[i].poolTag, pools[i]);
        }
    }

    public GameObject GetObject(string name)
    {
        GameObject objToReturn;

        if(!poolDictionary.ContainsKey(name))
        {
            Debug.LogError($"Threse is no pools with name {name}");
        }
        Debug.Log($"Pool count of {name} pool == {poolDictionary[name].pool.Count}");

        if(poolDictionary[name].pool.Count > 0)
        {
            objToReturn = poolDictionary[name].pool.Dequeue();
            objToReturn.SetActive(true);
        }
        else
        {
            objToReturn = Instantiate(poolDictionary[name].prefab, poolDictionary[name].parent);
            PooledObject poolComponent = objToReturn.AddComponent<PooledObject>();
            poolComponent.pooler = this;
            poolComponent.poolTag = poolDictionary[name].poolTag;
        }

        return objToReturn;
    }
    
    public void ReturnObject(GameObject toReturn)
    {
        PooledObject pooledObject = toReturn.GetComponent<PooledObject>();

        if(!poolDictionary.ContainsKey(pooledObject.poolTag))
        {
            Debug.LogError($"Threse is no pools with tag {pooledObject.poolTag}");
        }
        if(!poolDictionary[pooledObject.poolTag].pool.Contains(toReturn))
        {
            toReturn.SetActive(false);
            poolDictionary[pooledObject.poolTag].pool.Enqueue(toReturn);
        }
    }

}

public class PooledObject : MonoBehaviour
{
    public ObjectPool pooler;
    public string poolTag;

    public void DelayedReturnToPool(float delay)
    {
        StartCoroutine(DelayedReturn(delay));
    }

    private IEnumerator DelayedReturn(float delay)
    {
        yield return new WaitForSeconds(delay);
        pooler.ReturnObject(gameObject);
    }
}