using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CreateArmy : MonoBehaviour
{
    [SerializeField] GameObject CampSpawnPoint;
    [SerializeField] GameObject armyListCamp;
    
    CaptainOrder Captain;
    NavMeshAgent agent;
    soldierAnimatorScript soldierAnimator;

    bool create;

    private void Start()
    {
        Captain = GetComponent<CaptainOrder>();
    }
    private void OnEnable()
    {
        create = true;
    }
    public void createSoldier(int countOfMen)
    {
        if (create)
        {
            for (int i = 0; i < countOfMen; i++)
            {
                GameObject go = ObjectPool.Instance.GetFromPool("soldier");
                go.SetActive(true);
                agent = go.transform.GetComponent<NavMeshAgent>();
                soldierAnimator = go.transform.GetComponent<soldierAnimatorScript>();

                go.layer = transform.gameObject.layer;
                go.transform.parent = transform.parent;
                go.transform.position = CampSpawnPoint.transform.position;
                agent.enabled = true;
                //go.transform.position = Captain.transform.position + Captain.transform.forward * 5+Captain.transform.up*3;
                Captain.addListOfArmy(go, agent, soldierAnimator);

            }
            Captain.giveOrderFollowStop();
        }
        
    }
    public void dontCreate()
    {
        create = false;
    }
}
