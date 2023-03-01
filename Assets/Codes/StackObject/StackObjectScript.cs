using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackObjectScript : MonoBehaviour
{
    Rigidbody objectRigidbody;
    BoxCollider boxCollider;
    SphereCollider sphereCollider;
    private void Start()
    {
        objectRigidbody = transform.GetComponent<Rigidbody>();
        boxCollider = transform.GetComponent<BoxCollider>();
        sphereCollider = transform.GetComponent<SphereCollider>();
    }

    public void ComponentOn()
    {
        objectRigidbody.isKinematic = false;
        objectRigidbody.useGravity = true;
        boxCollider.enabled = true;
        objectRigidbody.AddForce(Vector3.up*5);
        Invoke("addForceInvoke", 1f);
    }
    void addForceInvoke()
    {
        sphereCollider.enabled = true;
    }
    public void ComponentOff()
    {
        objectRigidbody.isKinematic = true;
        objectRigidbody.useGravity = false;
        boxCollider.enabled = false;
        sphereCollider.enabled = false;
    }
}
