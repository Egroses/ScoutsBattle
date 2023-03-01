using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierColor : MonoBehaviour
{
    Renderer rendererMat;
    [SerializeField] List<Renderer> colorableObject = new List<Renderer>();
    
    void Start()
    {
        rendererMat = transform.GetComponent<Renderer>();
    }
    private void OnEnable()
    {
        StartCoroutine(getCaptainColor());   
    }
    IEnumerator getCaptainColor()
    {
        yield return new WaitForSeconds(0.2f);
        for (int i = 0; i < colorableObject.Count; i++)
        {
            colorableObject[i].material.color = PlayerComponents.Instance.getColor(transform.parent.parent.name);
        }
    }

}
