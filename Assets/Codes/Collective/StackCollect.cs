using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackCollect : MonoBehaviour
{
    TakenStackList takenStacksList;

    private void Start()
    {
        takenStacksList = transform.GetComponent<TakenStackList>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("StackableObject"))
        {
            takenStacksList.addObjectToList(other.gameObject);
        }
    }
}
