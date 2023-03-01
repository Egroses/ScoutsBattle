using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectToPool : MonoBehaviour
{
    public GameObject objectToPool;
    public string nameOfObject;
    public int poolCount;
    public int expandAmount;
    public List<GameObject> pooledObjects=new List<GameObject>();
}
