using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelStartScript : MonoBehaviour
{
    void Start()// plane spawnland�ktan sonra pointleri yerle�tirmesi i�in
    {
        GameObject.Find("ObjectToPoolObject").transform.Find("StackSpawnPointPool").GetComponent<SpawnStackPoint>().NewLevel();
    }
}
