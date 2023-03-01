using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TakenStackList : MonoBehaviour
{
    [SerializeField] float height = 0;
    [SerializeField] float lerpAmountPos=10;
    [SerializeField] float lerpAmountRos = 10;
    [SerializeField] float heightCount=0;
    [SerializeField] List<GameObject> ListOfObjects;
    [SerializeField] List<StackObjectScript> ListOfObjectScript;

    PlayerAnimatorScript playerAnimator;
    GameObject ReferenceStack;
    CharFillAmountScript charFillAmountScript;
    CinemachineScript cinemachineScript;

    private void Start()
    {
        playerAnimator = GetComponent<PlayerAnimatorScript>();
        ReferenceStack = transform.Find("ReferencePoint").gameObject;

        charFillAmountScript = null;
        cinemachineScript = null;
        if (transform.CompareTag("Player"))
        {
            cinemachineScript = GetComponent<CinemachineScript>();
            charFillAmountScript = GetComponent<CharFillAmountScript>();
        }
    }

    private void Update()
    {
        if (ListOfObjects.Count > 0)
        {
            for (int i = 0; i < ListOfObjects.Count ; i++)
            {
                GameObject obj = ListOfObjects[i];

                Vector3 targetPosition = i==0 ? ReferenceStack.transform.position + Vector3.up * heightCount : ListOfObjects[i-1].transform.position+Vector3.up*heightCount;

                Quaternion targetRotation = i == 0 ?  Quaternion.Euler(0,90+ transform.rotation.eulerAngles.y,0) : ListOfObjects[i - 1].transform.rotation;
                obj.transform.position = Vector3.Lerp(obj.transform.position, targetPosition, Time.deltaTime * lerpAmountPos);
                obj.transform.rotation = Quaternion.Lerp(obj.transform.rotation, targetRotation,Time.deltaTime * lerpAmountRos);
            }
        }
    }
   
    public void addObjectToList(GameObject addableObject)
    {
        height = ListOfObjects.Count * heightCount;
        ListOfObjectScript.Add(addableObject.transform.GetComponent<StackObjectScript>());
        ListOfObjectScript[ListOfObjectScript.Count-1].ComponentOff();
        StartCoroutine(setPositionTrue(addableObject,height));
        playerAnimator.CaryTrue();
    }
    IEnumerator setPositionTrue(GameObject addableObject,float upHeight)
    {
        addableObject.transform.DOMove(ReferenceStack.transform.position + Vector3.up * upHeight, 0.1f);
        yield return new WaitForSeconds(0.1f);
        addableObject.transform.position= ReferenceStack.transform.position + Vector3.up * upHeight;
        ListOfObjects.Add(addableObject);
        addableObject.transform.parent = null;

        if (transform.CompareTag("Player"))
        {
            charFillAmountScript.charFillAmountUpdate();
            StartCoroutine(cinemachineScript.ZoomOutCamera());
        }
    }
    public void freeAllStack()
    {
        if (ListOfObjects.Count > 0)
        {
            GameObject obj = ListOfObjects[ListOfObjects.Count - 1];
            ListOfObjectScript[ListOfObjects.Count - 1].ComponentOn();
            obj.SetActive(false);
            ListOfObjectScript.RemoveAt(ListOfObjects.Count - 1);
            ListOfObjects.RemoveAt(ListOfObjects.Count - 1);

            if (ListOfObjects.Count == 0)
            {
                playerAnimator.CaryFalse();
            }
            if (transform.CompareTag("Player"))
            {
                cinemachineScript.normalCameraSet();
            }
        }
    }

    public int getCountOfList()
    {
        return ListOfObjects.Count;
    }

    public void dropStack()
    {
        for (int i = 0; i < ListOfObjects.Count; i++)
        {
            if(i<ListOfObjectScript.Count)
                ListOfObjectScript[i]?.ComponentOn();
        }
        ListOfObjects.Clear();
        ListOfObjectScript.Clear();
    }

    public void FalseActiveStack()
    {
        int i = 0;
        while (i < ListOfObjects.Count)
        {
            if (i < ListOfObjectScript.Count)
                ListOfObjectScript[i]?.ComponentOn();
            ListOfObjects[i].SetActive(false);
            i++;
        }
        ListOfObjects.Clear();
        ListOfObjectScript.Clear();
    }
}
