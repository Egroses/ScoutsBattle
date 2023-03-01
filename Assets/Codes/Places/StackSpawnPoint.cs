using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackSpawnPoint : MonoBehaviour
{
    StackableObjectPooling stackableObjectPooling;
    void Start()
    {
        stackableObjectPooling = GetComponent<StackableObjectPooling>();
    }

    private void OnTriggerExit(Collider other)
    {
        if ((other.CompareTag("Player") || other.CompareTag("enemyCaptain")) && transform.childCount < 3)
        {
            stackableObjectPooling.spawnStackable(transform.position);
        }
    }
}
