using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackableObjectPooling : MonoBehaviour
{
    [SerializeField] string stackableObjectName;
    [SerializeField] List<GameObject> StackableObjects = new List<GameObject>();
    Coroutine controlSpawn;
    private void OnEnable()
    {
        controlSpawn = null;
        spawnStartGame(transform.position);
    }
    public void spawnStackable(Vector3 location)
    {
        if (controlSpawn == null)
        {
            controlSpawn = StartCoroutine(spawnStackableObject(location));
        }
    }
    void spawnStartGame(Vector3 location)
    {
        for (int i = 0; i < transform.childCount + 2; i++)
        {
            if (transform.childCount != 0)
            {
                GameObject childObject = transform.GetChild(0).gameObject;
                childObject.SetActive(false);
                childObject.transform.parent = null;
            }
        }
        StackableObjects.Clear();

        for (int i = 0; i < 3; i++)
        {
            StackableObjects.Add(ObjectPool.Instance.GetFromPool(stackableObjectName));
            StackableObjects[i].SetActive(true);

            if (i == 0)
            {
                StackableObjects[i].transform.position = location + transform.right;
            }
            else if (i == 1)
            {
                StackableObjects[i].transform.position = location - transform.right;
            }
            else if (i == 2)
            {
                StackableObjects[i].transform.position = location + transform.up;
            }

            StackableObjects[i].transform.parent = transform;

            StackableObjects[i].transform.rotation = transform.rotation;
        }
    }
    IEnumerator spawnStackableObject(Vector3 location)
    {
        yield return new WaitForSeconds(5f);
        for (int i = 0; i < transform.childCount+2; i++)
        {
            if (transform.childCount != 0)
            {
                GameObject childObject = transform.GetChild(0).gameObject;
                childObject.SetActive(false);
                childObject.transform.parent = null;
            }
        }
        StackableObjects.Clear();

        for (int i = 0; i < 3; i++)
        {
            StackableObjects.Add(ObjectPool.Instance.GetFromPool(stackableObjectName));
            StackableObjects[i].SetActive(true);

            if (i == 0)
            {
                StackableObjects[i].transform.position = location + transform.right;
            }
            else if (i == 1)
            {
                StackableObjects[i].transform.position = location - transform.right;
            }
            else if (i == 2)
            {
                StackableObjects[i].transform.position = location + transform.up;
            }

            StackableObjects[i].transform.parent = transform;

            StackableObjects[i].transform.rotation = transform.rotation;
        }
        
    
        
        controlSpawn = null;
    }
}
