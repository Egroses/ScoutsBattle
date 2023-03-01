using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnStackPoint : MonoBehaviour
{
    [SerializeField] int spawnPointCount;
    [SerializeField] float noise;
    [SerializeField] string stackSpawnName;
    [SerializeField] EnemySpawner spawner;
    public List<GameObject> StackSpawnPoints = new List<GameObject>();

    GameObject right;
    GameObject left;
    GameObject forward;
    GameObject backward;

    int squareX;
    int squareY;
    float verticalDistance;
    float horizontalDistance;
    public void NewLevel()
    {
        foreach (var points in StackSpawnPoints)
        {
            ObjectPool.Instance.DepositObject(points);
        }
        Transform obj = GameObject.Find("Level(Clone)").transform.Find("Plane");
        right = obj.Find("right").gameObject;
        left = obj.Find("left").gameObject;
        forward = obj.Find("forward").gameObject;
        backward = obj.Find("backward").gameObject;
        spawnPoints();
    }

    void spawnPoints()
    {
        if (spawnPointCount < 0)
        {
            spawnPointCount = 0;
        }

        squareX = (int)Mathf.Sqrt(spawnPointCount) - 1;
        squareY = (int)Mathf.Sqrt(spawnPointCount) - 1;
        if (squareX < Mathf.Sqrt(spawnPointCount) - 1) squareY = squareX + 1;
        if (squareX * squareY < spawnPointCount) squareX++;
        
        if (spawnPointCount==1)
        {
            squareX = 1;
            squareY = 1;
        }

        verticalDistance = (forward.transform.position.z - backward.transform.position.z) / squareY;
        horizontalDistance = (right.transform.position.x - left.transform.position.x) / squareX;

        for (int i = 0; i <= squareY; i++)
        {
            for (int j = 0; j <= squareX; j++)
            {
                int instantAmount = i * squareX + j + i;

                if (instantAmount < spawnPointCount)
                {
                    GameObject obj = ObjectPool.Instance.GetFromPool(stackSpawnName);
                    obj.SetActive(true);
                    Vector3 posPoint = Vector3.up * (right.transform.position.y + 1) + right.transform.position.x * Vector3.right + forward.transform.position.z * Vector3.forward - horizontalDistance * j * Vector3.right - verticalDistance * i * Vector3.forward;
                    obj.transform.position = posPoint;
                    posPoint = getNoise(posPoint);


                    if (posPoint.x < right.transform.position.x && posPoint.x > left.transform.position.x && posPoint.z < forward.transform.position.z && posPoint.z > backward.transform.position.z)
                    {
                        obj.transform.position = posPoint;
                    }

                    StackSpawnPoints.Add(obj);
                    obj.transform.parent = transform;
                }
            }
        }
        EnemySpawner.Instance.NewLevel();
    }
    
    Vector3 getNoise(Vector3 pos)
    {
        float xPos = Random.Range(-noise, noise);
        float yPos = Random.Range(-noise, noise);

        return pos + xPos * transform.right + yPos * transform.forward;
    }
}

