using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyReset : MonoBehaviour
{
    EnemyNavMeshScript enemyNavMesh;
    CampImageSpawn campImage;
    CaptainOrder captainOrder;
    CreateArmy createArmy;
    Death death;
    PlayerAnimatorScript playerAnimator;
    StackCollect stackCollect;
    StackSpawnScript stackSpawnScript;
    TakeDamage takeDamage;
    TakenStackList takenStack;

    private void Start()
    {
        //enemyNavMesh = GetComponent<EnemyNavMeshScript>();
        //campImage = GetComponent<CampImageSpawn>();
        captainOrder = GetComponent<CaptainOrder>();
        //createArmy = GetComponent<CreateArmy>();
        //death = GetComponent<Death>();
        //playerAnimator = GetComponent<PlayerAnimatorScript>();
        //stackCollect = GetComponent<StackCollect>();
        stackSpawnScript = GetComponent<StackSpawnScript>();
        //takeDamage = GetComponent<TakeDamage>();
        takenStack = GetComponent<TakenStackList>();
    }

    public void ResetAllEnemy()
    {
        leaderBoardScript.Instance.boardUpdate(transform.name, -1);
        PlayerComponents.Instance.removePlayer(transform.name);
        captainOrder.NewLevel();
        stackSpawnScript.NewLevel();
        takenStack.dropStack();
        transform.name = "enemyName";
    }
}
