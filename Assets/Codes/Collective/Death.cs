using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : MonoBehaviour
{
    Rigidbody rigidbodyOfPlayer;
    Collider colliderOfPlayer;
    PlayerAnimatorScript playerAnimator;
    TakenStackList takenStackList;
    CaptainOrder captainOrder;
    JoystickMovement joystickMovement;
    EnemyNavMeshScript enemyNavMesh;
    CreateArmy createArmy;

    soldierAnimatorScript soldierAnimator;
    public GameObject PooledObjectParent;

    bool iAmDead;
    private void Start()
    {
        rigidbodyOfPlayer = GetComponent<Rigidbody>();
        colliderOfPlayer = GetComponent<Collider>();
        takenStackList = null;
        if (transform.CompareTag("Player") || transform.CompareTag("enemyCaptain"))
        {
            if (transform.CompareTag("Player"))
                joystickMovement = GetComponent<JoystickMovement>();
            else
                enemyNavMesh = GetComponent<EnemyNavMeshScript>();

            createArmy = GetComponent<CreateArmy>();
            playerAnimator = GetComponent<PlayerAnimatorScript>();
            takenStackList = GetComponent<TakenStackList>();
        }
        if (transform.CompareTag("Soldier"))
        {
            soldierAnimator = transform.GetComponent<soldierAnimatorScript>();
        }
;
    }

    public void deathTrue()
    {
        iAmDead = true;
    }
    public void NewLevel()
    {
        iAmDead = false;
    }
    public bool DeathSituation()
    {
        return iAmDead;
    }
  
    public void deathPlayer()
    {
        if (takenStackList != null)
        {
            if (transform.CompareTag("Player"))
                joystickMovement.stopTheMovement();
            else
                enemyNavMesh.movementOff();

            leaderBoardScript.Instance.boardUpdate(transform.name,-1);
            rigidbodyOfPlayer.isKinematic = true;
            rigidbodyOfPlayer.useGravity = false;
            colliderOfPlayer.enabled=false;
            createArmy.dontCreate();
            playerAnimator.deadTrigger();
            takenStackList.dropStack();
            StartCoroutine(deadPlayer());
        }
        else
        {
            soldierAnimator.deadTrigger();
            captainOrder = PlayerComponents.Instance.getCaptain(transform.parent.name);
            captainOrder.soldierDead(transform.name);
            transform.name = "Soldier";
            transform.parent = null;
            colliderOfPlayer.enabled = false;
            StartCoroutine(deadSoldier());
        }
    }

    IEnumerator deadPlayer()
    {
        yield return new WaitForSeconds(10f);
        rigidbodyOfPlayer.isKinematic = false;
        rigidbodyOfPlayer.useGravity = true;
        colliderOfPlayer.enabled = true;
        transform.parent.gameObject.SetActive(false);
    }
    IEnumerator deadSoldier()
    {
        yield return new WaitForSeconds(10f);
        transform.gameObject.SetActive(false);
        colliderOfPlayer.enabled = true;
    }
}
