using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemyNavMeshScript : MonoBehaviour
{
    
    SpawnStackPoint spawnStackPoint;
    PlayerAnimatorScript playerAnimator;
    NavMeshAgent navAgent;
    Collider colliderEnemy;
    Rigidbody rigidbodyEnemy;
    List<GameObject> spawnPoints;
    Vector3 targetPoint;
    bool Camping=true;

    private void Awake()
    {
        rigidbodyEnemy = GetComponent<Rigidbody>();
        colliderEnemy = GetComponent<Collider>();
        playerAnimator = GetComponent<PlayerAnimatorScript>();
        navAgent = GetComponent<NavMeshAgent>();
    }
    private void Start()
    {
        GameEventSystem.current.enemysEvent += startFunc;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("StackSpawnPoint"))
        {
            if (Vector3.Distance(transform.position, targetPoint) < 5)
            {
                goToTheDestination();
            }
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("StackSpawnPoint"))
        {
            if (Vector3.Distance(transform.position, targetPoint) < 5)
            {
                goToTheDestination();
            }
        }
    }

    void goToTheDestination()
    {
        if (!Camping)
        {
            playerAnimator.RunTrue();
            int randomNumber;

            if (spawnPoints.Count >= 1)
            {
                randomNumber = Random.Range(0, spawnPoints.Count - 1);
                targetPoint = spawnPoints[randomNumber].transform.position;
                navAgent.SetDestination(targetPoint);
            }
        }
    }

    public void startFunc()
    {
        spawnStackPoint = GameObject.Find("ObjectToPoolObject").transform.Find("StackSpawnPointPool").GetComponent<SpawnStackPoint>();
        
        spawnPoints = spawnStackPoint.StackSpawnPoints;
        colliderEnemy.enabled = true;

        navAgent.enabled = true;
        movementOn();
    }

    public void movementOn()
    {
        Camping = false;
        goToTheDestination();
    }
    public void movementOff()
    {
        rigidbodyEnemy.velocity = Vector3.zero;
        playerAnimator.RunFalse();
        if (navAgent.enabled)
            navAgent.SetDestination(transform.position);
        Camping = true;
    }
}
