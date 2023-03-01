using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner Instance;

    [SerializeField] int countOfEnemy;
    [SerializeField] GameObject SpawnObject;
    [SerializeField] string objectName;
    [SerializeField] List<string> nameList = new List<string>();
    [SerializeField] List<Color> colors = new List<Color>();

    List<GameObject> enemys = new List<GameObject>();

    private void Awake()
    {
        Instance = this;
    }

    public void NewLevel()
    {
        foreach (var item in enemys)
        {
            item.transform.GetChild(0).gameObject.SetActive(false);
            item.transform.GetChild(1).GetComponent<EnemyReset>().ResetAllEnemy();
            ObjectPool.Instance.DepositObject(item); 
        }
        enemys.Clear();
        if (countOfEnemy < 0)
            countOfEnemy = nameList.Count;
        spawnEnemy();
    }
    public void spawnEnemy()
    {
        for (int i = 0; i < countOfEnemy; i++)
        {
            GameObject go = ObjectPool.Instance.GetFromPool(objectName);
            Transform obj = go.transform.Find("enemyName");
            obj.name = nameList[i];
            obj.Find("mesh").GetComponent<SkinnedMeshRenderer>().material.color = colors[i];
            go.name = "Takým " + (i+1);
            go.transform.parent = transform;
            go.SetActive(true);
            go.transform.position = transform.position + Vector3.forward * (Random.Range(-30, 30)) + Vector3.right * Random.Range(-30, 30);
            enemys.Add(go);
        }
    }
}
