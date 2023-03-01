using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelStartScript : MonoBehaviour
{
    void Start()// plane spawnlandýktan sonra pointleri yerleþtirmesi için
    {
        GameObject.Find("ObjectToPoolObject").transform.Find("StackSpawnPointPool").GetComponent<SpawnStackPoint>().NewLevel();
    }
}
