using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool Instance;

    public ObjectToPool[] objectsToPool;
    [SerializeField] GameObject pooledObjectsParent;

    private void Awake()
    {
        Instance = this;
        InitilaizePool();
    }
    void InitilaizePool()
    {
        foreach (ObjectToPool pool in objectsToPool)
        {
            for (int i = 0; i < pool.poolCount; i++)
            {
                GameObject _object = Instantiate(pool.objectToPool, Vector3.zero, Quaternion.identity);
                _object.name = pool.nameOfObject;
                _object.SetActive(false);
                _object.transform.parent = pooledObjectsParent.transform;
                pool.pooledObjects.Add(_object);
            }
        }
    }

    public GameObject GetFromPool(string name)
    {
        GameObject _objectToReturn=null;
        foreach (ObjectToPool pool in objectsToPool)
        {
            if (pool.nameOfObject.Equals(name))
            {
                for (int i = 0; i < pool.pooledObjects.Count; i++)
                {
                    if (!pool.pooledObjects[i].activeInHierarchy)
                    {
                        _objectToReturn = pool.pooledObjects[i];
                    }
                }
                if (_objectToReturn == null)
                {
                    for (int i = 0; i < pool.expandAmount; i++)
                    {
                        GameObject _objectPooling = Instantiate(pool.objectToPool, Vector3.zero, Quaternion.identity);
                        _objectPooling.SetActive(false);
                        _objectPooling.name = pool.nameOfObject;
                        pool.pooledObjects.Add(_objectPooling);
                        _objectToReturn = _objectPooling;
                    }
                }
            }
        }
        return _objectToReturn;
    }
    
    public void DepositObject(GameObject obj)
    {
        foreach (ObjectToPool item in objectsToPool)
        {
            foreach (GameObject go in item.pooledObjects)
            {
                if (obj == go)
                {
                    obj.SetActive(false);
                }
            }
        }
    }

    public void DepositAll()
    {
        foreach (ObjectToPool item in objectsToPool)
        {
            foreach(GameObject obj in item.pooledObjects)
            {
                obj.SetActive(false);
            }
        }
    }
}
