using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    [SerializeField] List<GameObject> Levels = new List<GameObject>();


    [SerializeField] bool TestLevel = false;

    [ShowIf("TestLevel")][SerializeField] GameObject TestLevelPrefab;
    
    public int levelNumber;

    GameObject go;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        if (TestLevel)
        {
            go = Instantiate(TestLevelPrefab, Vector3.zero , Quaternion.identity);
        }
        else
        {
            go = Instantiate(Levels[levelNumber], Vector3.zero , Quaternion.identity);
        }
    }
    public void NewLevel()
    {
        Destroy(go);
        levelNumber = levelNumber+1>=5 ? 0 : levelNumber+1;
        go = Instantiate(Levels[levelNumber], Vector3.zero, Quaternion.identity);
    }
    public void LevelAgain()
    {
        Destroy(go);
        go = Instantiate(Levels[levelNumber], Vector3.zero , Quaternion.identity);
    }
}
