using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class CampImageSpawn : MonoBehaviour
{
    [SerializeField] Image spawnCampImage;
    [SerializeField] GameObject campObject;


    float startFill;
    float endFill;

    public void campValues(float distance,int stackCount)
    {
        startFill = 1;
        endFill = stackCount;

        campObject.SetActive(true);
        spawnCampImage.fillAmount = 0;
        campObject.transform.position = transform.position + transform.forward * distance;
    }
    public void campFillAmountUpdate()
    {
        if (spawnCampImage.fillAmount < 1)
        {
         
            spawnCampImage.DOFillAmount(startFill/endFill,0.1f);
            startFill += 1;
        }
        if(startFill==endFill)
        {
            StartCoroutine(dissapearCamp());
        }
    }

    IEnumerator dissapearCamp()
    {
        yield return new WaitForSeconds(2);
        campObject.SetActive(false);
    }
    
}
