using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class startFuncScript : MonoBehaviour
{
    //CaptainOrder
    //    Create
    //    animator
    //    stackSpawn
    //    takeDamage ?
    //    takenstack ?
    //    enemyNavMesh
    //    joyStcik+
    CaptainOrder captainOrder;
    JoystickMovement joystickMovement;
    Death death;
    Collider colliderPlayer;
    TakenStackList takenStack;
    void Start()
    {
        joystickMovement = null;
        if (transform.CompareTag("Player"))
        {
            joystickMovement = GetComponent<JoystickMovement>();
        }
        takenStack = GetComponent<TakenStackList>();
        captainOrder = GetComponent<CaptainOrder>();
        death = GetComponent<Death>();
        colliderPlayer = GetComponent<Collider>();
        GameEventSystem.current.playerEvent += resetPlayer;
    }
    
    public void resetPlayer()
    {
        colliderPlayer.enabled = true;
        joystickMovement?.NewLevel();
        captainOrder.NewLevel();
        takenStack.FalseActiveStack();
        death.NewLevel();
    }

}
