using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class CharFillAmountScript : MonoBehaviour
{
    StackSpawnScript spawnScript;
    TakenStackList takenStackList;

    [SerializeField] Image charStackImage;
    void Start()
    {
        spawnScript = GetComponent<StackSpawnScript>();
        takenStackList = GetComponent<TakenStackList>();
    }

    
    public void charFillAmountUpdate()
    {
        float startFill = takenStackList.getCountOfList();
        float endFill = spawnScript.needMenCount;
        if (startFill <= endFill)
        {
            charStackImage.DOFillAmount(startFill / endFill,1f);
        }
        else
        {
            charStackImage.DOFillAmount(1, 1f);
        }
    }
}
