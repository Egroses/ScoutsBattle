using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackSpawnScript : MonoBehaviour
{
    int collectSoon;
    float spawnCountDown;
    public int needMenCount;

    [SerializeField] float spawnTimeCountDown;
    [SerializeField] float sphereRadius;
    [SerializeField] float maxDistance;
    [SerializeField] int factorAmount=1;
    [SerializeField] LayerMask layerMask;

    Vector3 origin;
    Vector3 direction;
    Coroutine controlCoroutine;

    CreateArmy createArmy;
    CampImageSpawn campImage;
    TakenStackList takenStackList;
    JoystickMovement joystickMovement;
    EnemyNavMeshScript enemyNavMeshScript;
    CharFillAmountScript charFillAmountScript;


    void Start()
    {
        joystickMovement = null;
        charFillAmountScript = null;
        enemyNavMeshScript = null;
        if (transform.CompareTag("enemyCaptain"))
        {
            enemyNavMeshScript = GetComponent<EnemyNavMeshScript>();
        }
        if (transform.CompareTag("Player"))
        {
            joystickMovement = GetComponent<JoystickMovement>();
            charFillAmountScript = GetComponent<CharFillAmountScript>();
        }
        

        campImage = GetComponent<CampImageSpawn>();
        createArmy = GetComponent<CreateArmy>();
        takenStackList = GetComponent<TakenStackList>();
    }

    public void NewLevel()//yeni levelde 
    {
        spawnCountDown = spawnTimeCountDown;
        collectSoon = 0;
        controlCoroutine = null;
    }

    void Update()
    {
        origin = transform.position;
        direction = transform.forward;
        if (collectSoon == takenStackList.getCountOfList())
        {
            if (spawnCountDown >= 0)
                spawnCountDown -= Time.deltaTime;
        }
        else
        {
            collectSoon = takenStackList.getCountOfList();
            spawnCountDown = spawnTimeCountDown;
        }

        Collider[] hits = Physics.OverlapSphere(origin + transform.forward * maxDistance, sphereRadius, layerMask);
        
        if (hits.Length ==0)
        {
            if (takenStackList.getCountOfList() >= needMenCount && spawnCountDown <= 0 && controlCoroutine==null)
            {
                controlCoroutine = StartCoroutine(spawnCampStart());

                spawnCountDown = spawnTimeCountDown;
            }
        }
        
    }
    IEnumerator spawnCampStart()
    {

        int menCount = takenStackList.getCountOfList();
        int stackCount = takenStackList.getCountOfList();
        float CountDown = 0.1f;

        campImage.campValues(maxDistance,stackCount);

      
        joystickMovement?.stopTheMovement();
        enemyNavMeshScript?.movementOff();

        for (int i = stackCount; i > 0; i--)
        {
            takenStackList.freeAllStack();
            campImage.campFillAmountUpdate();
            yield return new WaitForSeconds(CountDown);
            CountDown -= CountDown / 10;
        }

        
        joystickMovement?.continueTheMovement();
        charFillAmountScript?.charFillAmountUpdate();
        enemyNavMeshScript?.movementOn();

        createArmy.createSoldier(menCount/factorAmount);
        controlCoroutine = null;

    }
}
