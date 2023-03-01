using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.AI;
public class CaptainOrder : MonoBehaviour
{
    [SerializeField] List<GameObject> armyList=new List<GameObject>();
    [SerializeField] List<NavMeshAgent> armyAgent = new List<NavMeshAgent>();
    [SerializeField] List<soldierAnimatorScript> soldierAnimList = new List<soldierAnimatorScript>();
    [SerializeField] List<Vector3> armyPos = new List<Vector3>();
    [SerializeField] float radiusCircle;
    [SerializeField] float noise;
    [SerializeField] float angle=0;
    [SerializeField] float distance=1;

    List<Vector3> targetPoints = new List<Vector3>();

    public void NewLevel()
    {
        for (int i = 0; i < armyList.Count; i++)
        {
            armyList[i].SetActive(false);
        }
        armyList.Clear();
        armyPos.Clear();
        armyAgent.Clear();

        PlayerComponents.Instance.joinPlayer(transform.gameObject);
        leaderBoardScript.Instance.boardUpdate(transform.name, armyList.Count);
    }

    private void FixedUpdate()
    {
        if (GameManagerScript.Instance.gameSituation())
        {
            if (armyList.Count > 0 && armyPos.Count > 0 && armyAgent.Count > 0)
            {
                for (int i = 0; i < armyList.Count; i++)
                {
                    float distanceVector = Vector3.Distance(targetPoints[i], transform.position + armyPos[i] - transform.forward * distance * radiusCircle);
                    if (distanceVector > 5)
                    {
                        targetPoints[i] = transform.position + armyPos[i] - transform.forward * distance * radiusCircle;

                        if (armyAgent[i].transform.gameObject.activeInHierarchy)
                        {
                            try
                            {
                                armyAgent[i].SetDestination(targetPoints[i]);
                            }
                            catch
                            {

                            }
                            soldierAnimList[i].runTrue();
                        }
                    }
                    if (armyAgent[i].remainingDistance<10)
                    {
                        soldierAnimList[i].runFalse();
                    }
                }
            }
        }   
    }

    public void addListOfArmy(GameObject soldierObject,NavMeshAgent agent,soldierAnimatorScript soldierAnimator)
    {
        soldierObject.name = armyList.Count+"";
        armyList.Add(soldierObject);
        armyAgent.Add(agent);
        soldierAnimList.Add(soldierAnimator);
        leaderBoardScript.Instance.boardUpdate(transform.name,armyList.Count);
    }

    public void soldierDead(string nameOfSoldier)
    {
        for(int i=0;i<armyList.Count;i++)
        {
            if (armyList[i].name.Equals(nameOfSoldier))
            {
                armyList.RemoveAt(i);
                armyAgent[i].enabled = false;
                armyAgent.RemoveAt(i);
            }
        }
        leaderBoardScript.Instance.boardUpdate(transform.name, armyList.Count);
    }
    

    public void giveOrderFollowStop()
    {
        armyPos.Clear();
        targetPoints.Clear();
        angle = 0;
        float menCount = armyList.Count;
        float xPointCircle = 0;
        float zPointCircle = 0;
        float _radiusCircle;
        if (menCount > 20)
        {
            radiusCircle += (menCount-20)/10;
        }
        for (int i = 0; i < menCount; i++)
        {
            _radiusCircle = Random.Range(0, radiusCircle);
            xPointCircle = Mathf.Cos(angle*Mathf.Deg2Rad) * _radiusCircle;
            zPointCircle = Mathf.Sin(angle*Mathf.Deg2Rad) * _radiusCircle;
            angle += 360f / menCount;

            Vector3 positionSoldier = new Vector3(xPointCircle, 2,zPointCircle);
            positionSoldier += getNoise(positionSoldier);
            //armyList[i].transform.position = positionSoldier;
            armyPos.Add(positionSoldier);
            targetPoints.Add(Vector3.zero);
        }
    }
    

    Vector3 getNoise(Vector3 pos)
    {
        float noisePos = Mathf.PerlinNoise(pos.x*Time.deltaTime, pos.z*Time.deltaTime)*noise;
        return new Vector3(noisePos, 0, noisePos);
    }

}
