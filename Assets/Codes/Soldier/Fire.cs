using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Fire : MonoBehaviour
{
    [SerializeField] float maxDistance;
    [SerializeField] float sphereRadius;
    [SerializeField] LayerMask layerMask;
    [SerializeField] string bulletName;
    [SerializeField] GameObject sight;

    bool coroutineBool;
    bool fireBool;
    soldierAnimatorScript soldierAnimator;

    Vector3 origin;
   

    private void Start()
    {
        soldierAnimator = transform.GetComponent<soldierAnimatorScript>();
    }
    private void OnEnable()
    {
        coroutineBool = true;
    }
    private void Update()
    {
        fireBool = true;
        origin = transform.position;

        Collider[] hits = Physics.OverlapSphere(origin + transform.forward * maxDistance, sphereRadius,layerMask);
        
        if (hits.Length > 0)
        {
            foreach (var items in hits)
            {
                if (items.transform != null)
                {
                    if (items.transform.CompareTag("Soldier"))
                    {
                        
                        if (items.transform.parent.name != transform.parent.name)
                        {
                            fireOnCoro(items.gameObject);
                            fireBool = false;
                            break;
                        }
                    }
                }

                if (items.CompareTag("Player") || items.CompareTag("enemyCaptain"))
                {
                    if (items.transform.parent.name != transform.parent.name)
                    {
                        if (items.gameObject.transform.parent.childCount < 4)
                        {
                            fireOnCoro(items.gameObject);
                            fireBool = false;
                            break;
                        }
                    }
                }
            }
        }
        if(fireBool)
        {
            soldierAnimator.FireFalse();
        }
    }

    void fireOnCoro(GameObject other)
    {
        if (coroutineBool)
        {
            soldierAnimator.FireTrue();
            coroutineBool = false;
            StartCoroutine(fireOn(other.gameObject));
            transform.LookAt(other.transform);
        }
        
    }

    IEnumerator fireOn(GameObject target)
    {
        yield return new WaitForSeconds(0.5f);
        GameObject obj = ObjectPool.Instance.GetFromPool(bulletName);
        obj.transform.position = sight.transform.position;
        obj.SetActive(true);
        StartCoroutine(bulletGone(obj,target));
        coroutineBool = true;
    }

    IEnumerator bulletGone(GameObject obj,GameObject target)
    {

        while (true)
        {
            if (Vector3.Distance(obj.transform.position, target.transform.position) < 3f && target.activeInHierarchy)
                break;
            obj.transform.position = Vector3.Lerp(obj.transform.position, target.transform.position+Vector3.up*2,Time.deltaTime*20);
            yield return null;
        }
        if (target.activeInHierarchy)
            obj.transform.position = target.transform.position + Vector3.up * 2;
        
        obj.SetActive(false);
        target.GetComponent<TakeDamage>().takeDamageFunc();
    }
}
